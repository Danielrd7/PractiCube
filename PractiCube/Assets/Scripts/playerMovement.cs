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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
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
}
