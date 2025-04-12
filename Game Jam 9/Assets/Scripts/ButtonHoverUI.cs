using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite redSprite;
    public Sprite greenSprite;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = redSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = greenSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = redSprite;
    }
}
