using UnityEngine;
using UnityEngine.UI;

public class NoiseGaugeScript : MonoBehaviour
{
    public Image needle;
    public PlayerStats stats;
    void Start()
    {
        UpdateNoiseGauge();
    }


    void Update()
    {
        UpdateNoiseGauge();
    }

    void UpdateNoiseGauge()
    {
        float noise = stats.noiseLevel;
        float angle;

        if (noise <= 3.1f)
        {
            angle = Mathf.Lerp(30f, -85f, noise / 3.1f);
        }
        else if (noise <= 7.1f)
        {
            angle = Mathf.Lerp(-85f, -220f, (noise - 3.1f) / (7.1f - 3.1f));
        }
        else
        {
            angle = -220f;
        }

        needle.rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }


}
