using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image targetImage;
    public void ToggleImage()
    {
        if (targetImage != null)
        {
            bool isCurrentlyActive = targetImage.gameObject.activeSelf;
            targetImage.gameObject.SetActive(!isCurrentlyActive);
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        LoadScene("Title Screen");
    }
    public void GoToSettings()
    {
        LoadScene("Settings Screen");
    }
    public void GoToMainGame()
    {
        LoadScene("Main Scene");
    }
}
