using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioCue : MonoBehaviour
{
    public AudioClip cueClip;
    public bool playOnStart = false;
    public bool oneShot = true;
    public float delay = 0f;

    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = savedVolume;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = cueClip;

        if (playOnStart)
            PlayCue();
    }

    public void PlayCue()
    {
        if (oneShot && hasPlayed) return;

        hasPlayed = true;
        audioSource.PlayDelayed(delay);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PlayCue();
    }
}
