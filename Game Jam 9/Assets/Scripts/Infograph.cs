using UnityEngine;

public class InfoButtonController : MonoBehaviour
{
    public GameObject infoPanel;

    public void ToggleInfo()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}