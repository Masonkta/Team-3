using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Vitals")]
    public float Health = 100f;
    public float stamina = 100f;

    [Header("Noise")]
    public float noiseLevel = 0f;
    public float maxNoiseLevel = 10f;
    public float noiseDecayRate = 1f;
    public CircleCollider2D noiseCollider;


    public enum NoiseLevelCategory
    {
        None,
        Low,
        Medium,
        High
    }
    public NoiseLevelCategory CurrentNoiseCategory
    {
        get
        {
            if (noiseLevel == 0) return NoiseLevelCategory.None;
            if (noiseLevel <= 1) return NoiseLevelCategory.Low;
            if (noiseLevel <= 4) return NoiseLevelCategory.Medium;
            return NoiseLevelCategory.High;
        }
    }
    void Start()
    {
        Transform noiseColliderTransform = transform.Find("Noise Collider");
        if (noiseColliderTransform != null)
        {
            noiseCollider = noiseColliderTransform.GetComponent<CircleCollider2D>();
        }
    }
    void Update()
    {
        if (noiseLevel > 0)
        {
            noiseLevel -= noiseDecayRate * Time.deltaTime;
            noiseLevel = Mathf.Clamp(noiseLevel, 0f, maxNoiseLevel);
            Debug.Log("noise not 0");
        }

        if (noiseCollider != null)
        {
            noiseCollider.radius = 1 + noiseLevel;
        }
    }
    public void AddNoise(float amount)
    {
        noiseLevel = Mathf.Clamp(noiseLevel + amount, 0f, maxNoiseLevel);
    }
    public void AddNoiseWalk(float amount)
    {
        if (noiseLevel < 3) 
        {
            noiseLevel += amount;
            noiseLevel = Mathf.Clamp(noiseLevel, 0f, 3f);
        }
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player died!");
    }
}
