using UnityEngine;

public class Simple2DMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;
    public GameObject eyeUp;
    public GameObject eyeDown;
    public GameObject eyeLeft;
    public GameObject eyeRight;
    public GameObject eyeUpRight;
    public GameObject eyeUpLeft;
    public GameObject eyeDownRight;
    public GameObject eyeDownLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetEyeDirection(lastMoveDirection);
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput.normalized;
        }
        UpdateEyeDirection();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void UpdateEyeDirection()
    {
        SetEyeDirection(lastMoveDirection);
    }

    void SetEyeDirection(Vector2 direction)
    {
        eyeUp.SetActive(false);
        eyeDown.SetActive(false);
        eyeLeft.SetActive(false);
        eyeRight.SetActive(false);
        eyeUpRight.SetActive(false);
        eyeUpLeft.SetActive(false);
        eyeDownRight.SetActive(false);
        eyeDownLeft.SetActive(false);

        float threshold = 0.1f;

        if (direction.y > threshold)
        {
            if (direction.x > threshold)
                eyeUpRight.SetActive(true);
            else if (direction.x < -threshold)
                eyeUpLeft.SetActive(true);
            else
                eyeUp.SetActive(true);
        }
        else if (direction.y < -threshold)
        {
            if (direction.x > threshold)
                eyeDownRight.SetActive(true);
            else if (direction.x < -threshold)
                eyeDownLeft.SetActive(true);
            else
                eyeDown.SetActive(true);
        }
        else
        {
            if (direction.x > threshold)
                eyeRight.SetActive(true);
            else if (direction.x < -threshold)
                eyeLeft.SetActive(true);
        }
    }
}
