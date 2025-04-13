using UnityEngine;

public class ChestScript : MonoBehaviour
{
    // Optional: You can assign these in the Inspector or find them by name
    private GameObject closedLid;
    private GameObject openLid;
    private GameObject key;

    void Start()
    {
        closedLid = transform.Find("Closed").gameObject;
        openLid = transform.Find("Open").gameObject;
        key = transform.Find("Key").gameObject;

        closedLid.SetActive(true);
        openLid.SetActive(false);
        key.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            closedLid.SetActive(false);
            openLid.SetActive(true);
            key.SetActive(true);
        }
    }
}
