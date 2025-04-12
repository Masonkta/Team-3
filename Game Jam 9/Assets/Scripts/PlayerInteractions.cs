using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public float sewTime = 2f;
    public Scrollbar progressBar;

    private float sewProgress = 0f;
    private PickUp currItem;
    private bool hasPickUp = false;


    void Update()
    {
        
    }

    public void getInteraction(PickUp item)
    {
        currItem = item;
        hasPickUp = true;
    }
}
