using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChanger : MonoBehaviour
{
    public string nextScene;
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level_One")
        {
            Debug.LogWarning("Loaded LEVEL_1");

            GameObject foundSpawn = GameObject.FindWithTag("SpawnPoint");
            if (foundSpawn != null)
            {
                spawnPoint = foundSpawn.transform;
                Debug.Log("SpawnPoint found after scene load.");
            }
            else
            {
                Debug.LogWarning("SpawnPoint not found after scene load.");
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && spawnPoint != null)
            {
                player.transform.position = spawnPoint.position;
                Debug.Log("Player moved to spawn point.");

                PlayerBodyChanges parts = player.GetComponent<PlayerBodyChanges>();
                if (parts != null)
                {
                    if (parts.hasArms)
                    {
                        GameObject armPickup = GameObject.Find("Arm Pickup");
                        if (armPickup != null) Destroy(armPickup);
                        Debug.LogWarning("Destroyed Arm Pickup");
                    }

                    if (parts.hasHead)
                    {
                        GameObject headPickup = GameObject.Find("Head Pickup");
                        if (headPickup != null) Destroy(headPickup);
                        Debug.LogWarning("Destroyed Head Pickup");
                    }

                    if (parts.hasLegs)
                    {
                        GameObject torsoPickup = GameObject.Find("Torso Pickup");
                        if (torsoPickup != null) Destroy(torsoPickup);
                        Debug.LogWarning("Destroyed Torso Pickup (Legs)");
                    }
                }
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
