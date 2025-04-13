using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerStats stats;
    public Slider healthbar;
    public Slider staminabar;
    public PlayerBodyChanges player;

    void Start()
    {
        UpdateHealthBar();
        staminabar.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateHealthBar();
        if (player.hasLegs && staminabar.gameObject.activeSelf)
        {
            UpdateStaminaBar();
        }
        else if (player.hasLegs && !staminabar.gameObject.activeSelf)
        {
            staminabar.gameObject.SetActive(true);
        }
    }

    void UpdateHealthBar()
    {
        float healthNormalized = Mathf.Clamp01(stats.Health / stats.MaxHealth);
        healthbar.value = healthNormalized;
    }
    void UpdateStaminaBar()
    {
        float staminaNormalized = Mathf.Clamp01(stats.Stamina / stats.MaxStamina);
        staminabar.value = staminaNormalized;
    }

}
