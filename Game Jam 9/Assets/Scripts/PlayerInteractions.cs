using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public float sewTime = 2f;
    private Scrollbar progressBar;
    private float sewProgress = 0f;
    private PickUp currItem;
    private bool hasPickUp = false;


    void Update()
    {
        Debug.Log("player interaction update call");
        if (currItem != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("pressing the e button");
                if (!hasPickUp)
                {
                    hasPickUp = true;
                }

                sewProgress += Time.deltaTime;
                progressBar.size = sewProgress / sewTime;

                if (sewProgress >= sewTime)
                {
                    currItem.end();
                    clearInteraction();
                }
            }
            else if (hasPickUp)
            {
                clearInteraction();
            }
        }
    }
    public void getInteraction(PickUp item)
    {
        Debug.Log("item recieved");
        currItem = item;
        hasPickUp = true;
        progressBar = item.interactionUI.GetComponentInChildren<Scrollbar>();
        progressBar.size = 0;
        sewProgress = 0;
    }
    public void clearInteraction()
    {
        //currItem = null;
        progressBar.size = 0;
        sewProgress = 0;
        hasPickUp = false;
    }
}
