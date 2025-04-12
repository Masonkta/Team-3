using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
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

    [Header("Roaming Settings")]
    public float roamSpeed = .5f;     
    public float roamRadius = 10f;    
    public float roamDelay = 3f;       
    private Vector3 roamTarget;        
    private float roamTimer;           

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disable rotation and up-axis updates
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
        LockRotation();
    }

    void LockRotation()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation.z); // Keep only the Z rotation
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
    }
    public void StopChasing()
    {
        isChasing = false;
        agent.ResetPath();
    }
}
