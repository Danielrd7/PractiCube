using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float jumpForce;

    private Vector2 moveDirection;

    public InputActionReference move;
    public InputActionReference saltar;

    public int totalJumps = 1;
    int leftJumps;
    bool inWall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    private void OnEnable()
    {
        saltar.action.started += Saltar;
    }

    private void OnDisable()
    {
        saltar.action.started -= Saltar;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pared"))
        {
            leftJumps = totalJumps;
            inWall = true;
        }

        if (other.gameObject.CompareTag("Suelo"))
        {
            leftJumps = totalJumps;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pared"))
        {
            inWall = false;
        }
    }
}

