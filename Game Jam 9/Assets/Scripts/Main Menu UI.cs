using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image activeImage;
    public Image inactiveImage;
    public Slider volumeSlider;

    public void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = 0;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }
    public void ToggleImage()
    {
            bool isCurrentlyActive = activeImage.gameObject.activeSelf;
            activeImage.gameObject.SetActive(!isCurrentlyActive);
            inactiveImage.gameObject.SetActive(isCurrentlyActive);

        if (inactiveImage.gameObject.activeSelf)
        {
            AudioListener.volume = 0f;
            PlayerPrefs.SetFloat("MasterVolume", 0f);
            PlayerPrefs.Save();
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
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
}
