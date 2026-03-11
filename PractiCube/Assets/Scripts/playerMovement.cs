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
    private takeObject currentObject;
    private pressButton currentButton;
    private fase1Fase2 currentFase1Fase2;
    private fasesnivel9 currentFaseNivel9;

    public int totalJumps = 1;
    int leftJumps;
    public int totalDashes = 1;
    int leftDashes;
    bool inButtonFase1Fase2;
    bool inDoor;
    bool inWall;
    bool inGround;
    bool isGrounded;

    Renderer rend;

    static bool fasePrimeraVez = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        leftDashes = totalDashes;
        leftJumps = totalJumps;
        rend = GetComponent<Renderer>();

        if (fasePrimeraVez == false)
        {
            fases.fase = 1;
            fasePrimeraVez = true;
        }
    }

    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();

        if (leftJumps <= 0)
        {
            rend.materials[3].color = new Color(0.3294f, 0.1647f, 0f);
        }
        else if (leftJumps == 1)
        {
            rend.materials[3].color = new Color(0.5216f, 0.2588f, 0f);
        }
        else if (leftJumps == 2)
        {
            rend.materials[3].color = new Color(0.7216f, 0.3608f, 0f);
        }
        else if (leftJumps >= 3)
        {
            rend.materials[3].color = new Color(1f, 0.502f, 0f);
        }

        if (leftDashes <= 0)
        {
            rend.materials[1].color = new Color(0.102f, 0.102f, 0.102f, 1f);
        }
        else if (leftDashes == 1)
        {
            rend.materials[1].color = new Color(0f, 0.0039f, 0.3725f, 1f);
        }
        else if (leftDashes >= 2)
        {
            rend.materials[1].color = new Color(0f, 0.0118f, 0.7608f);
        }

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

        if (currentButton != null)
        {
            currentButton.PressButton();
        }

        if (inDoor == true)
        {
            fases.fase = 1;
        }

        if (inButtonFase1Fase2 == true)
        {
            currentFase1Fase2.CambioFase();
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
            if (leftJumps != 3)
            {
                leftJumps = totalJumps;
            }

            if (leftDashes != 2)
            {
                leftDashes = totalDashes;
            }

            inWall = true;
        }

        if (other.gameObject.CompareTag("Suelo"))
        {
            if (leftJumps != 3)
            {
                leftJumps = totalJumps;
            }

            if (leftDashes != 2)
            {
                leftDashes = totalDashes;
            }

            inGround = true;
        }

        Tp tp = other.GetComponent<Tp>();
        if (tp != null && other.gameObject.CompareTag("Puerta"))
        {
            currentTp = tp;
            inDoor = true;
        }

        if (tp != null && other.gameObject.CompareTag("Laser"))
        {
            currentTp = tp;
            currentTp.onInteract();
        }

        fase1Fase2 fase1AFase2 = other.GetComponent<fase1Fase2>();
        if (fase1AFase2 != null && other.gameObject.CompareTag("LaserFase1Fase2"))
        {
            currentTp = tp;
            currentTp.onInteract();
            
            currentFase1Fase2 = fase1AFase2;
            currentFase1Fase2.CambioFase();
        }

        fasesnivel9 faseNivel9 = other.GetComponent<fasesnivel9>();
        if (faseNivel9 != null && other.gameObject.CompareTag("LaserNivel9"))
        {
            currentTp = tp;
            currentTp.onInteract();
            
            currentFaseNivel9 = faseNivel9;
            currentFaseNivel9.CambioFases();
        }

        takeObject objJump = other.GetComponent<takeObject>();
        if (objJump != null && other.gameObject.CompareTag("Jump"))
        {
            currentObject = objJump;
            leftJumps ++;
            currentObject.TakeObject();
        }

        takeObject objDash = other.GetComponent<takeObject>();
        if (objDash != null && other.gameObject.CompareTag("Dash"))
        {
            currentObject = objDash;
            leftDashes ++;
            currentObject.TakeObject();
        }

        pressButton button = other.GetComponent<pressButton>();
        if (button != null && other.gameObject.CompareTag("Button"))
        {
            currentButton = button;
        }

        if (fase1AFase2 != null && other.gameObject.CompareTag("ButtonFase1Fase2"))
        {
            currentButton = button;
            currentFase1Fase2 = fase1AFase2;
            inButtonFase1Fase2 = true;
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
            inDoor = false;
        }

        fase1Fase2 fase1AFase2 = other.GetComponent<fase1Fase2>();
        if (fase1AFase2 != null && other.gameObject.CompareTag("ButtonFase1Fase2"))
        {
            currentButton = null;
            currentFase1Fase2 = null;
            inButtonFase1Fase2 = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Suelo")) {
            isGrounded = true;
            
            if (leftDashes != 2)
            {
                leftDashes = totalDashes;
            }
            
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

