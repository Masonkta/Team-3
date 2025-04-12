using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverClickSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Sprite greySprite;
    public Sprite redSprite;
    public Sprite greenSprite;
    private Image buttonImage;
    private bool isHovering = false;
    private bool isClicking = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = greySprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (!isClicking)
            buttonImage.sprite = redSprite;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (!isClicking)
            buttonImage.sprite = greySprite;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isClicking = true;
        buttonImage.sprite = greenSprite;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isClicking = false;

        if (isHovering)
            buttonImage.sprite = redSprite;
        else
            buttonImage.sprite = greySprite;
    }
}
