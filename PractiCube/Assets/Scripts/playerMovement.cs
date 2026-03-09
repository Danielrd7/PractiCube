using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float jumpForce;
    public float dashForce;

    private Vector2 moveDirection;

    public InputActionReference move;
    public InputActionReference saltar;
    public InputActionReference dash;
    public InputActionReference interact;

    private Tp currentTp;

    public int totalJumps = 1;
    int leftJumps;
    public int totalDashes = 1;
    int leftDashes;
    bool inWall;
    bool inGround;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        leftDashes = totalDashes;
        leftJumps = totalJumps;
    }

    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * speed, rb.linearVelocity.y);
    }

    private void Saltar(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (leftJumps > 0)
            {
                if (inWall)
                {
                    rb.AddForce(new Vector2(-moveDirection.x * 5 * speed, jumpForce), ForceMode.Impulse);
                    leftJumps--;
                }
                else
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                    leftJumps--;
                }
            }
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (context.started && inWall != true && inGround != true && leftDashes > 0 || context.started && isGrounded == true && leftDashes > 0)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(new Vector2(moveDirection.x * 3 * dashForce, moveDirection.y * dashForce), ForceMode.Impulse);
            leftDashes--;
            StartCoroutine(StopDash(.1f));
        }
    }

    void Interact(InputAction.CallbackContext context)
    {
        if (currentTp != null)
        {
            currentTp.onInteract();
        }
    }

    private void OnEnable()
    {
        saltar.action.started += Saltar;
        dash.action.started += Dash;
        interact.action.started += Interact;
    }

    private void OnDisable()
    {
        saltar.action.started -= Saltar;
        dash.action.started -= Dash;
        interact.action.started -= Interact;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pared"))
        {
            leftJumps = totalJumps;
            leftDashes = totalDashes;
            inWall = true;
        }

        if (other.gameObject.CompareTag("Suelo"))
        {
            leftJumps = totalJumps;
            leftDashes = totalDashes;
            inGround = true;
        }

        Tp tp = other.GetComponent<Tp>();
        if (tp != null && other.gameObject.CompareTag("Puerta"))
        {
            currentTp = tp;
        }

        if (tp != null && other.gameObject.CompareTag("Lazer"))
        {
            currentTp = tp;
            currentTp.onInteract();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pared"))
        {
            inWall = false;
        }

        if (other.gameObject.CompareTag("Suelo"))
        {
            inGround = false;
        }

        Tp tp = other.GetComponent<Tp>();
        if (tp != null && tp == currentTp && other.gameObject.CompareTag("Puerta"))
        {
            currentTp = null;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Suelo"))        {
            isGrounded = true;
            leftDashes = totalDashes;
        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator StopDash(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        rb.linearVelocity = Vector3.zero;
        rb.Sleep();
    }
}

