using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public AudioCollection[] audioCollections;  // Array of sound settings (Set in Unity Inspector)
    public SoundPool soundPool; // Reference to the SoundPool (Assign in Unity)
    private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundMusicSource;
    public static SoundManager instance;

    [Header("Audio Mixers")]
    public AudioMixer masterMixer;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    // Play a Sound by Name
    public void Play(string name, bool _varyPitch = false)
    {
        Debug.Log("SoundManager: Play " + name);
        AudioCollection ac = audioCollections.FirstOrDefault(s => s.name == name);
        if (ac != null && soundPool != null)
        {
            SoundData sound = ac.sounds[Random.Range(0, ac.sounds.Length)];
            GameObject audioObject = soundPool.GetPooledObject();
            if (audioObject != null)
            {
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();
                audioSource.clip = sound.clip;
                audioSource.volume = sound.volume;

                if (ac.type == AudioCollection.TypeOfSound.Music)
                    audioSource.outputAudioMixerGroup = musicMixerGroup;
                else
                    audioSource.outputAudioMixerGroup = sfxMixerGroup;
                    
                if (_varyPitch)
                    audioSource.pitch = sound.pitch + Random.Range(-0.3f, 0.3f);
                else
                    audioSource.pitch = sound.pitch;
                audioSource.loop = sound.loop;
                audioObject.SetActive(true);
                audioSource.Play();

                // Disable the object after the sound finishes playing
                if (!sound.loop)
                {
                    StartCoroutine(DeactivateAfterPlay(audioSource));
                }
            }
        }
        else
        {
            Debug.LogWarning($"Sound '{name}' was not found!");
        }
    }

    public void PlayIndexed(string name, int index, bool _varyPitch = false)
    {
        Debug.Log("SoundManager: Play " + name);
        AudioCollection ac = audioCollections.FirstOrDefault(s => s.name == name);
        if (ac != null && soundPool != null)
        {
            SoundData sound = ac.sounds[index];
            GameObject audioObject = soundPool.GetPooledObject();
            if (audioObject != null)
            {
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();

                if (ac.type == AudioCollection.TypeOfSound.Music)
                    audioSource.outputAudioMixerGroup = musicMixerGroup;
                else
                    audioSource.outputAudioMixerGroup = sfxMixerGroup;

                audioSource.clip = sound.clip;
                audioSource.volume = sound.volume;
                if (_varyPitch)
                    audioSource.pitch = sound.pitch + Random.Range(-0.3f, 0.3f);
                else
                    audioSource.pitch = sound.pitch;
                audioSource.loop = sound.loop;
                audioObject.SetActive(true);
                audioSource.Play();

                // Disable the object after the sound finishes playing
                if (!sound.loop)
                {
                    StartCoroutine(DeactivateAfterPlay(audioSource));
                }
            }
        }
        else
        {
            Debug.LogWarning($"Sound '{name}' was not found!");
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public bool IsBackgroundMusicPlaying()
    {
        return backgroundMusicSource != null && backgroundMusicSource.isPlaying;
    }

    // Stop a Specific Sound
    public void Stop(string name)
    {
        AudioCollection ac = audioCollections.FirstOrDefault(s => s.name == name);
        if (ac != null)
        {
            SoundData sound = ac.sounds[Random.Range(0, ac.sounds.Length)];
            foreach (GameObject obj in soundPool.pooledObjects)
            {
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                if (audioSource.clip == sound.clip && obj.activeInHierarchy)
                {
                    audioSource.Stop();
                    obj.SetActive(false);
                    return;
                }
            }
        }
    }

    // Stop All Sounds
    public void StopAll()
    {
        foreach (GameObject obj in soundPool.pooledObjects)
        {
            obj.SetActive(false);
        }
    }

    /*public void Update()
    {
        if(GameManager.Instance.battleOn == false){
            
            PlayBackgroundMusic();
            Debug.Log("PlayingBackGround");
            
        }

        if(GameManager.Instance.battleOn == true)
        {
            PlayBattleMusic();
        }
    }*/

    // Deactivate Pooled Object After Sound Finishes
    private IEnumerator DeactivateAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        source.gameObject.SetActive(false);
    }

    // Quick Play Methods for Specific Sounds
    /*public void PlayDashSound() => Play("Dash");
    public void PlayLandingSound() => Play("Landing");
    public void PlayEnemyHurtSound() => Play("EnemyHurt");
    public void PlayRadialPopInSound() => Play("RadialPop-In");
    public void PlayRadialPopOutSound() => Play("RadialPop-Out");
    public void PlayRadialSwitchSound() => Play("RadialSwitch");
    public void PlayBreakableSound() => Play("Breakable");
    public void PlayBossRoarSound() => Play("BossRoar");
    public void PlayEnemyDeathSound() => Play("EnemyDeath");
    public void PlayTransportPortalSound() => Play("TransportPortal");*/

    public void PlayPlayerHurtSound() => Play("PlayerHurt");
    public void PlayPlayerDeathSound() => Play("PlayerDeath");
    public void PlayFootstepsSound() => Play("Footsteps");
    public void PlayChainsSound() => Play("Chains");
    public void PlayOpenDoorSound() => Play("OpenDoor");
    public void PlayCloseDoorSound() => Play("CloseDoor");
    public void PlayRummagingSound() => Play("Rummaging");
    public void PlayStitchingSound() => Play("Stitching");
    public void PlayLowGrowlSound() => Play("LowGrowl");
    public void PlayMediumGrowlSound() => Play("MediumGrowl");
    public void PlayLoudGrowlSound() => Play("LoudGrowl");
    public void PlayKickDoorSound() => Play("KickDoor");
    public void PlayZombieBreathing() => Play("ZombieBreating");

}

