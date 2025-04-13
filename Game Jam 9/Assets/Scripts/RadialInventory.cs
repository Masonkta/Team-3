using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RadialInventory : MonoBehaviour
{
    public GameObject radialMenu;
    public List<RadialSection> radialSections; // One section per part (e.g., arm, leg)
    public Inventory playerInventory;

    public KeyCode toggleKey = KeyCode.Q;
    private bool menuOpen = false;

    void Start()
    {
        radialMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ToggleMenu(false);
        }

        if (menuOpen)
        {
            UpdateRadialDisplay();
        }
    }

    void ToggleMenu(bool state)
    {
        menuOpen = state;

        if (state)
        {
            radialMenu.SetActive(true);
            radialMenu.transform.localScale = Vector3.zero;
            radialMenu.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

            foreach (RadialSection section in radialSections)
            {
                section.sectionObject.SetActive(true);
                section.sectionObject.transform.localScale = Vector3.zero;
                section.sectionObject.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            }
        }
        else
        {
            radialMenu.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack)
                .OnComplete(() => radialMenu.SetActive(false));

            foreach (RadialSection section in radialSections)
            {
                section.sectionObject.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack)
                    .OnComplete(() => section.sectionObject.SetActive(false));
            }
        }
    }

    void UpdateRadialDisplay()
    {
        for (int i = 0; i < radialSections.Count; i++)
        {
            bool hasPart = playerInventory.carryingPart && playerInventory.partNum == i;
            bool equipped = !playerInventory.carryingPart && playerInventory.partNum == i;

            Image icon = radialSections[i].icon;

            if (equipped)
            {
                icon.color = new Color(1f, 1f, 1f, 1f); // fully visible
            }
            else if (hasPart)
            {
                icon.color = new Color(1f, 1f, 1f, 0.5f); // half visible
            }
            else
            {
                icon.color = new Color(1f, 1f, 1f, 0.2f); // dim
            }
        }
    }
}

[System.Serializable]
public class RadialSection
{
    public GameObject sectionObject;
    public Image icon;
}
