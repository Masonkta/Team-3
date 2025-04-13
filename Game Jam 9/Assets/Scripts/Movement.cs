using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;

    [Header("Light Tracking")]
    public Light2D light2d;
    //public float eyeRotationSpeed = 5f;

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

            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            light2d.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // adjust offset if needed

        }

        if (moveInput.x < 0)
        {
            // goin left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x > 0)
        {
            // goin right
            transform.localScale = new Vector3(1, 1, 1);
        }

        light2d.transform.localScale = new Vector3(1, 1, 1);

    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
    /*void UpdateEyeDirection()
    {
        float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        eyeTransform.rotation = Quaternion.Lerp(eyeTransform.rotation, targetRotation, Time.deltaTime * eyeRotationSpeed);
    }*/
}
