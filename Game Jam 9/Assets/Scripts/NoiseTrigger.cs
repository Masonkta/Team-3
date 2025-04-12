using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip lowNoiseClip;
    public AudioClip mediumNoiseClip;
    public AudioClip highNoiseClip;

    public AudioSource audioSource;
    public PlayerStats playerStats;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyAi enemyAI = other.GetComponent<EnemyAi>();
        enemyAI.StartChasing(transform.root.gameObject);
        PlayMonsterReactionSound();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        EnemyAi enemyAI = other.GetComponent<EnemyAi>();
        if (enemyAI != null)
        {
            enemyAI.StopChasing();
        }
    }

    void PlayMonsterReactionSound()
    {
        if (playerStats == null) return;

        AudioClip selectedClip = null;
        switch (playerStats.CurrentNoiseCategory)
        {
            case PlayerStats.NoiseLevelCategory.Low:
                selectedClip = lowNoiseClip;
                break;
            case PlayerStats.NoiseLevelCategory.Medium:
                selectedClip = mediumNoiseClip;
                break;
            case PlayerStats.NoiseLevelCategory.High:
                selectedClip = highNoiseClip;
                break;
        }

        if (selectedClip != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(selectedClip);
        }
    }
}
