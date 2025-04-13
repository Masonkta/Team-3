using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float sprintSpeed = 4f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection = Vector2.down;

    [Header("Sprinting")]
    public PlayerBodyChanges playerBody;
    public bool isSprinting = false;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;
    public float sprintCooldownTime = 2f; 
    private float sprintCooldownTimer = 0f;
    private bool canSprint = true; 

    [Header("Light Tracking")]
    public Light2D light2d;

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

        if (playerStats.Stamina >= 0f && canSprint)
        {
            isSprinting = Input.GetKey(KeyCode.LeftShift) && moveInput != Vector2.zero && playerBody.hasLegs;
        }
        else
        {
            isSprinting = false;
        }


        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        if (moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;

            if (playerStats != null)
            {
                if (isSprinting)
                {
                    playerStats.ReduceStamina(staminaDrainRate * Time.deltaTime);
                    playerStats.AddNoise(1.5f * Time.deltaTime);
                }
                else
                {
                    playerStats.AddNoiseWalk(noisePerSecondWhileMoving * Time.deltaTime);

                    if (playerStats.Stamina < 100)
                    {
                        playerStats.RegenStamina(staminaRegenRate * Time.deltaTime);
                    }
                }
            }

            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            light2d.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            if (!isSprinting && playerStats != null && playerStats.Stamina < 100)
            {
                playerStats.RegenStamina(staminaRegenRate * Time.deltaTime);
            }
        }

        if (playerStats.Stamina <= 0 && canSprint)
        {
            canSprint = false;
            sprintCooldownTimer = sprintCooldownTime; 
        }


        if (!canSprint)
        {
            sprintCooldownTimer -= Time.deltaTime;
            if (sprintCooldownTimer <= 0f && playerStats.Stamina >= 10f)
            {
                canSprint = true; 
            }
        }

        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        light2d.transform.localScale = new Vector3(1, 1, 1);
    }
    void FixedUpdate()
    {
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        rb.linearVelocity = moveInput * currentSpeed;
    }

}
