using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public GameObject interactionUI;
    public GameObject errorUI;
    private GameObject player;
    private PlayerInteractions playerInt;
    private PlayerBodyChanges pBody;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInt = collision.GetComponent<PlayerInteractions>();
            pBody = collision.GetComponent<PlayerBodyChanges>();

            if (pBody.hasArms)
            {
                interactionUI.SetActive(true);
                playerInt.finaDoor(this);
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
        Debug.Log("THE GAME ENDS");
    }
}
