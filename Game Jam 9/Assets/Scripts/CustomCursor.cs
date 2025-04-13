using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotatingCursor : MonoBehaviour
{
    [Header("Cursor Sprites")]
    public Sprite idleCursorSprite;
    public Sprite movingCursorSprite;
    public Sprite clickCursorSprite;

    [Header("References")]
    public RectTransform cursorImage;
    public Image cursorImageComponent;
    public Canvas canvas;

    [Header("Settings")]
    public float rotationSpeed = 20f;
    public float movementThreshold = 0.5f;
    public float movementLingerTime = 0.2f; 
    public float spriteRotationOffset = -90f; 

    private Vector3 lastMousePosition;
    private Vector2 moveDirection;
    private float movementTimer = 0f;

    void Start()
    {
        Cursor.visible = false;
        lastMousePosition = Input.mousePosition;
        SetCursor(idleCursorSprite, false);
    }

    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        Vector2 delta = currentMousePosition - lastMousePosition;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPoint
        );
        cursorImage.anchoredPosition = localPoint;

        bool isClicking = Input.GetMouseButton(0);
        bool isMoving = delta.sqrMagnitude > movementThreshold;

        if (isMoving)
        {
            moveDirection = delta.normalized;
            movementTimer = movementLingerTime;
        }
        else
        {
            movementTimer -= Time.deltaTime;
        }

        if (isClicking)
        {
            SetCursor(clickCursorSprite, false);
        }
        else if (movementTimer > 0f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + spriteRotationOffset;
            SetCursor(movingCursorSprite, true, angle);
        }
        else
        {
            SetCursor(idleCursorSprite, false);
        }

        lastMousePosition = currentMousePosition;
    }

    void SetCursor(Sprite sprite, bool shouldRotate, float angle = 0f)
    {
        if (cursorImageComponent.sprite != sprite)
        {
            cursorImageComponent.sprite = sprite;
        }

        if (shouldRotate)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            cursorImage.rotation = Quaternion.Lerp(cursorImage.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            cursorImage.rotation = Quaternion.identity;
        }
    }
}
