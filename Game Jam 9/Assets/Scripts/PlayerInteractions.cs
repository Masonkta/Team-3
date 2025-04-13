using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public float holdTime = 3f;
    private Scrollbar progressBar;
    private float holdProg = 0f;
    private PickUp currItem;
    private Embodier curInteractable;
    private bool hasPickUp = false;
    private bool isPickUp = false;
    private Inventory inven;

    void Start()
    {
        inven = GetComponent<Inventory>();
    }
    void Update()
    {
        if (isPickUp) {
            if (currItem != null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Debug.Log("pressing the e button");
                    if (!hasPickUp)
                    {
                        hasPickUp = true;
                    }

                    holdProg += Time.deltaTime;
                    progressBar.size = holdProg / holdTime;

                    if (holdProg >= holdTime)
                    {
                        currItem.end();
                        clearInteraction();
                    }
                }
                else if (hasPickUp)
                {
                    holdProg -= Time.deltaTime;
                    progressBar.size = progressBar.size = holdProg / holdTime;
                    if (holdProg <= 0)
                    {
                        clearInteraction();
                    }
                }
            }
        } 
        else 
        {
            if (curInteractable != null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (inven.carryingPart)
                    {
                        Debug.Log("pressing the e button");
                        if (!hasPickUp)
                        {
                            hasPickUp = true;
                        }

                        holdProg += Time.deltaTime;
                        progressBar.size = holdProg / holdTime;

                        if (holdProg >= holdTime)
                        {
                            curInteractable.end();
                            clearInteraction();
                        }
                        }
                        else if (hasPickUp)
                        {
                            holdProg -= Time.deltaTime;
                            progressBar.size = progressBar.size = holdProg / holdTime;
                            if (holdProg <= 0)
                            {
                                clearInteraction();
                            }
                    }
                    else
                    {
                        clearInteraction();
                    }
                }
            }
        }
    }
    public void getPickUp(PickUp item)
    {
        Debug.Log("item recieved");
        currItem = item;
        hasPickUp = true;
        isPickUp = true;
        progressBar = item.interactionUI.GetComponentInChildren<Scrollbar>();
        progressBar.size = 0;
        holdProg = 0;
    }
    public void staticInteract(Embodier emb)
    {
        Debug.Log("item recieved");
        curInteractable = emb;
        hasPickUp = true;
        isPickUp = false;
        progressBar = emb.interactionUI.GetComponentInChildren<Scrollbar>();
        progressBar.size = 0;
        holdProg = 0;
    }
    public void clearInteraction()
    {
        holdProg = 0;
        hasPickUp = false;
    }
}
