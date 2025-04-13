using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject interactionUI;
    public GameObject errorUI;
    public int bodyPartNum;
    private GameObject player;
    private PlayerInteractions playerInt;
    private PlayerBodyChanges playerUpdate;
    private Inventory pInven;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInt = collision.GetComponent<PlayerInteractions>();
            playerUpdate = collision.GetComponent<PlayerBodyChanges>();
            pInven = collision.GetComponent<Inventory>();

            if (pInven.carryingPart == false)
            {
                playerInt.getPickUp(this);
                interactionUI.SetActive(true);
            }
            else
            {
                errorUI.SetActive(true);
                playerInt.clearInteraction();
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionUI.SetActive(false);
            errorUI.SetActive(false);
        }
    }
    public void end()
    {
        playerUpdate.carryPart(bodyPartNum);
        pInven.addPartItem(bodyPartNum);
        Destroy(gameObject);
    }

}
