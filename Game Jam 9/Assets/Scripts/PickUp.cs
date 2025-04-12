using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject interactionUI;
    public int bodyPartNum;
    private GameObject player;
    private PlayerInteractions playerInt;
    private PlayerBodyChanges playerUpdate;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInt = collision.GetComponent<PlayerInteractions>();
            playerUpdate = collision.GetComponent<PlayerBodyChanges>();
            playerInt.getInteraction(this);
            interactionUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionUI.SetActive(false);
        }
    }

    public void end()
    {
        playerUpdate.bodyUpate(bodyPartNum);
        Destroy(gameObject);
    }

}
