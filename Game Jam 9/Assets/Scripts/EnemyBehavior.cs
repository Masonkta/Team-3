using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // MOVEMENT VARIABLES
    public float moveSpeed = 2f;
    public float range = 5f; // How far from the starting point the enemy can move
    public float waitTime = 2f; // Time to wait before picking a new target
    private Vector2 startPos;
    private Vector2 newPos;
    private float waitCounter;

    // Player Tracking
    private Vector2 playerPos;
    private bool playerFound;


    void Start()
    {
        startPos = transform.position;
        //playerPos = transform.position;
        getPos();
        //Debug.Log(playerPos);
    }

    void Update()
    {
        if (playerFound == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, moveSpeed * 1.2f * Time.deltaTime);

            //Debug.Log("to player movement");

            if (Vector2.Distance(transform.position, playerPos) < 0.1f)
            {
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    playerFound = false;
                    getPos();
                }
            }
        }
        // Move toward the target
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);

            //Debug.Log("Idle movement");

            if (Vector2.Distance(transform.position, newPos) < 0.1f)
            {
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    getPos();
                }
            }
        }
    }

    void getPos()
    {
        float randomX = Random.Range(-range, range);
        float randomY = Random.Range(-range, range);
        newPos = startPos + new Vector2(randomX, randomY);
        waitCounter = waitTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerPos = collision.transform.position;
            playerFound = true;
            Debug.Log(playerPos);
        }
    }
    /*void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerFound = false;
        }
    }*/
}
