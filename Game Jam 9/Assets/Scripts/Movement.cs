using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;

    [Header("Eye Tracking")]
    public Transform eyeTransform;
    public float eyeRotationSpeed = 5f;

    [Header("Noise Settings")]
    public float noisePerSecondWhileMoving = .1f;
    private PlayerStats playerStats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
            if (playerStats != null)
            {
                if (playerStats.noiseLevel < 3)
                {
                    playerStats.AddNoiseWalk(noisePerSecondWhileMoving * Time.deltaTime);
                }
            }
        }
        UpdateEyeDirection();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
    void UpdateEyeDirection()
    {
        float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        eyeTransform.rotation = Quaternion.Lerp(eyeTransform.rotation, targetRotation, Time.deltaTime * eyeRotationSpeed);
    }
}
