using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    public bool hasHead = false;

    void Update()
    {
        AudioListener.pause = !hasHead;
    }
}
