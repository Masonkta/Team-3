using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public GameObject doorClosed;
    public GameObject doorOpen;

    public GameObject interactionUI;
    public string openText = "Hold E to Open";
    public string closeText = "Hold E to Close";
    public string kickText = "Press E to Kick Down Door";

    public float holdDuration = 2f;
    public KeyCode interactKey = KeyCode.E;
    public float quietOpenNoise = 0.5f;
    public float kickNoise = 5f;

    private bool playerInRange = false;
    private bool isOpen = false;
    private float holdTimer = 0f;
    private bool isHolding = false;

    private Text uiText;
    public Scrollbar progressBar;
    private PlayerBodyChanges playerBody;
    private PlayerStats playerStats;

    void Start()
    {
        if (interactionUI != null)
        {
            uiText = interactionUI.GetComponentInChildren<Text>();
            if (progressBar != null) progressBar.size = 0f;

            Debug.Log("UI text and progress bar found and assigned.");
        }

        UpdateDoorState(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerBody = collision.GetComponent<PlayerBodyChanges>();
            playerStats = collision.GetComponent<PlayerStats>();

            playerInRange = true;
            interactionUI?.SetActive(true);
            UpdateUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            interactionUI?.SetActive(false);
            ResetHold();
        }
    }

    private void Update()
    {
        if (!playerInRange || playerBody == null || playerStats == null)
            return;

        if (!playerBody.hasArms && !playerBody.hasLegs)
        {
            interactionUI?.SetActive(false);
            return;
        }

        // Ensure UI is always visible while in range
        if (!interactionUI.activeSelf)
            interactionUI.SetActive(true);

        UpdateUI(); // Keep the UI updated with current state

        if (playerBody.hasArms)
        {
            if (Input.GetKey(interactKey))
            {
                isHolding = true;
                holdTimer += Time.deltaTime;

                if (progressBar != null)
                    progressBar.size = holdTimer / holdDuration;

                if (holdTimer >= holdDuration)
                {
                    ToggleDoor();
                    playerStats.AddNoise(quietOpenNoise);
                    ResetHold();
                }
            }
            else if (isHolding)
            {
                ResetHold();
            }
        }
        else if (!playerBody.hasArms && playerBody.hasLegs)
        {
            if (Input.GetKeyDown(interactKey))
            {
                UpdateDoorState(!isOpen);
                playerStats.AddNoise(kickNoise);
            }
        }
    }

    private void ToggleDoor()
    {
        UpdateDoorState(!isOpen);
    }

    private void UpdateDoorState(bool open)
    {
        isOpen = open;
        if (doorClosed != null) doorClosed.SetActive(!isOpen);
        if (doorOpen != null) doorOpen.SetActive(isOpen);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (uiText == null) return;

        if (!playerBody.hasArms && playerBody.hasLegs)
        {
            uiText.text = kickText;
        }
        else
        {
            uiText.text = isOpen ? closeText : openText;
        }

    }

    private void ResetHold()
    {
        isHolding = false;
        holdTimer = 0f;
        if (progressBar != null)
            progressBar.size = 0f;
    }
}
