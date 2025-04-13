using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    public PlayerBodyChanges body;

    void Update()
    {
        if (!body.hasHead) {
            AudioListener.volume = 0f;
        }
        else
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            AudioListener.volume = savedVolume;
        }
    }
}
