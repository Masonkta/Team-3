using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject interactionUI;
    private PlayerInteractions pi;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pi = collision.GetComponent<PlayerInteractions>();
            pi.getInteraction(this);
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
        Destroy(gameObject);
    }

}
