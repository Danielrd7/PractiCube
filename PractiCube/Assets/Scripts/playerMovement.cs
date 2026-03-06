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
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                leftJumps--;
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

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Estructura"))
        {
            leftJumps = totalJumps;
        }
    }
}

