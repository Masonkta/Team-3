using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class BossScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject targetPlayer;
    public PlayerStats playerStats;
    public bool isChasing = false;

    [Header("Chase Speeds")]
    public float lowNoiseSpeed = 1f;
    public float mediumNoiseSpeed = 2.5f;
    public float highNoiseSpeed = 4f;
    public float speedSmoothness = 5f;
    public float damageCooldown = 1f;
    private float nextDamageTime = 0f;

    [Header("Roaming Settings")]
    public float roamSpeed = .5f;
    public float roamRadius = 10f;
    public float roamDelay = 3f;
    private Vector3 roamTarget;
    private float roamTimer;

    [Header("Light Settings")]
    public Light2D spotlight;
    public float noNoiseIntensity = 0.01f;
    public float lowNoiseIntensity = 0.3f;
    public float mediumNoiseIntensity = 0.6f;
    public float highNoiseIntensity = 1f;
    public float lightSmoothness = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.areaMask = NavMesh.AllAreas;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        transform.position = new Vector3(transform.position.x, 0f, 0f);
        roamTimer = roamDelay;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, currentPosition.y, 0f);
        if (isChasing && targetPlayer != null)
        {
            Vector3 targetPosition = targetPlayer.transform.position;
            targetPosition.z = transform.position.z;
            agent.SetDestination(targetPosition);
            if (spotlight != null)
            {
                float targetIntensity = spotlight.intensity;

                switch (playerStats.CurrentNoiseCategory)
                {
                    case PlayerStats.NoiseLevelCategory.Low:
                        targetIntensity = lowNoiseIntensity;
                        break;
                    case PlayerStats.NoiseLevelCategory.Medium:
                        targetIntensity = mediumNoiseIntensity;
                        break;
                    case PlayerStats.NoiseLevelCategory.High:
                        targetIntensity = highNoiseIntensity;
                        break;
                    case PlayerStats.NoiseLevelCategory.None:
                        targetIntensity = noNoiseIntensity;
                        break;
                }

                spotlight.intensity = Mathf.Lerp(spotlight.intensity, targetIntensity, Time.deltaTime * lightSmoothness);
            }
            if (playerStats != null)
            {
                float targetSpeed = agent.speed;
                switch (playerStats.CurrentNoiseCategory)
                {
                    case PlayerStats.NoiseLevelCategory.Low:
                        targetSpeed = lowNoiseSpeed;
                        break;
                    case PlayerStats.NoiseLevelCategory.Medium:
                        targetSpeed = mediumNoiseSpeed;
                        break;
                    case PlayerStats.NoiseLevelCategory.High:
                        targetSpeed = highNoiseSpeed;
                        break;
                    case PlayerStats.NoiseLevelCategory.None:
                        StopChasing();
                        return;
                }
                agent.speed = Mathf.Lerp(agent.speed, targetSpeed, Time.deltaTime * speedSmoothness);
            }
        }
        else
        {
            Roam();
        }
        FlipSprite();
        LockRotation();
    }
    void LockRotation()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation.z);
    }
    void Roam()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            if (roamTimer <= 0f)
            {
                SetRoamTarget();
                roamTimer = roamDelay;
            }
            else
            {
                roamTimer -= Time.deltaTime;
            }
        }
        agent.speed = roamSpeed;
    }
    void SetRoamTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            roamTarget = hit.position;
            agent.SetDestination(roamTarget);
        }
    }
    public void StartChasing(GameObject player)
    {
        targetPlayer = player;
        playerStats = player.GetComponent<PlayerStats>();
        isChasing = true;
        //SoundManager.instance.Play("Chains");
        if (SoundManager.instance == true)
        {
            SoundManager.instance.Play("Chains");
        }
    }
    public void StopChasing()
    {
        isChasing = false;
        agent.ResetPath();
        if (SoundManager.instance == true)
        {
            SoundManager.instance.Stop("Chains");
        }
    }
    void FlipSprite()
    {
        if (agent.velocity.x > 0.1f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (agent.velocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(1f); 
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Player") && Time.time >= nextDamageTime)
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(10f);
                    nextDamageTime = Time.time + damageCooldown;
                }
            }
        }
    }
}