using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAi enemyAI = other.GetComponent<EnemyAi>();
            if (enemyAI != null)
            {
                enemyAI.StartChasing(transform.root.gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAi enemyAI = other.GetComponent<EnemyAi>();
            if (enemyAI != null)
            {
                enemyAI.StopChasing();
            }
        }
    }
}
