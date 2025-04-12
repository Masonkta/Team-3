using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D clickCursor;
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        SetCursor(defaultCursor);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetCursor(clickCursor);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetCursor(defaultCursor);
        }
    }

    void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, hotspot, cursorMode);
    }
}
