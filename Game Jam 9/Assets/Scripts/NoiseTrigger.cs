using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip lowNoiseClip;
    public AudioClip mediumNoiseClip;
    public AudioClip highNoiseClip;

    public AudioSource audioSource;
    public PlayerStats playerStats;

    public GameObject targetPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        if (targetPlayer != null)
        {
            playerStats = targetPlayer.GetComponent<PlayerStats>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAi enemyAI = other.GetComponent<EnemyAi>();
            enemyAI.StartChasing(transform.root.gameObject);
            PlayMonsterReactionSound();
        }

        if (other.CompareTag("Boss")) {
            BossScript BossAi = other.GetComponent<BossScript>();
            BossAi.StartChasing(transform.root.gameObject);
            PlayMonsterReactionSound();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        EnemyAi enemyAI = other.GetComponent<EnemyAi>();
        if (enemyAI != null)
        {
            enemyAI.StopChasing();
        }

        if (other.CompareTag("Boss"))
        {
            BossScript BossAi = other.GetComponent<BossScript>();
            if (BossAi != null)
            {
                BossAi.StopChasing();
            }
        }
    }
    void PlayMonsterReactionSound()
    {
        if (playerStats == null || targetPlayer == null) return;

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
            float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);

            float minVolume = 0.1f;
            float maxVolume = 1f;

            float volume = Mathf.Clamp01(1f - (distance));
            volume = Mathf.Lerp(minVolume, maxVolume, volume);

            audioSource.PlayOneShot(selectedClip, volume);
        }
    }
}
