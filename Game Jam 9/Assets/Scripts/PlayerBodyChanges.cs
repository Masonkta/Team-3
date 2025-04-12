using UnityEngine;

public class PlayerBodyChanges : MonoBehaviour
{
    [Header("Pick Up Parts")]
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    [Header("Checks for each part")]
    public bool hasHead;
    public bool hasLegs;
    public bool hasArms;
    [Header("Sprites for each combo")]
    public GameObject eyes_only;
    public GameObject fullbody;


    public void carryPart(int num)
    {
        switch(num){
            case 1:
                p1.SetActive(true);
                Debug.Log("PART 1");
                break;
            case 2:
                p2.SetActive(true);
                Debug.Log("PART 2");
                break;
            case 3:
                p3.SetActive(true);
                Debug.Log("PART 3");
                break;
            default:
                Debug.Log("incorrect body part number");
                break;
        }
    }

    public void gainPart(int num)
    {
        switch(num){
            case 1:
                p1.SetActive(false);
                hasHead = true;
                break;
            case 2:
                p2.SetActive(false);
                hasLegs = true;
                break;
            case 3:
                p3.SetActive(false);
                hasArms = true;
                break;
            default:
                Debug.Log("incorrect body part number");
                break;
        }
    }

    void Start()
    {
        eyes_only.SetActive(true);
        fullbody.SetActive(false);
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.7f, 1f);
        gameObject.GetComponent<CircleCollider2D>().radius = 0.75f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0.02f, -0.3f);
    }

    void Update()
    {
        if (hasArms && hasHead && hasLegs)
        {
            eyes_only.SetActive(false);
            fullbody.SetActive(true);
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.3f, 3.5f);
            gameObject.GetComponent<CircleCollider2D>().radius = 1.5f;
            gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0.02f, -0.7f);
        }
    }
}
