using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChanger : MonoBehaviour
{
    public string nextScene;
    public static int from = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
