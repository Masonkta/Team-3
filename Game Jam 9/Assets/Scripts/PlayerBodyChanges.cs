using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    public GameObject head;
    public GameObject torsoHead;
    public GameObject torso;
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
                addHead();
                break;
            case 2:
                p2.SetActive(false);
                hasLegs = true;
                addTorso();
                break;
            case 3:
                p3.SetActive(false);
                addFullBody();
                hasArms = true;
                break;
            default:
                Debug.Log("incorrect body part number");
                eyesOnly();
                break;
        }
    }

    void Start()
    {
        eyesOnly();
    }
    void Update()
    {
        if(!hasHead && hasLegs && !hasArms)
        {
            addTorso();
        }
        else if (hasHead && hasLegs && !hasArms)
        {
            addTorsoHead();
        }
        else if (hasHead && !hasLegs && !hasArms)
        {
            addHead();
        }
        else if (hasHead && hasLegs && hasArms)
        {
            addFullBody();
        }
        else { eyesOnly(); }
    }

    public void eyesOnly()
    {
        eyes_only.SetActive(true);
        head.SetActive(false);
        torsoHead.SetActive(false);
        torso.SetActive(false);
        fullbody.SetActive(false);
        // adjust hitbox
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.65f, 0.65f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.03f, 0f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.4f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0.03f, 0f);
    }
    public void addHead()
    {
        eyes_only.SetActive(false);
        head.SetActive(true);
        torsoHead.SetActive(false);
        torso.SetActive(false);
        fullbody.SetActive(false);
        // adjust hitbox
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.7f, 0.88f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.0097f, 0.019f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.52f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0,0);
    }
    public void addTorsoHead()
    {
        eyes_only.SetActive(false);
        head.SetActive(false);
        torsoHead.SetActive(true);
        torso.SetActive(false);
        fullbody.SetActive(false);
        // adjust hitbox
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.857f, 1.4f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.008f, -0.31f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.4f);
        // adjust sight light
        //gameObject.GetComponent<Light2D>().transform.position = new Vector3(-0.2f, 0f);
    }
    public void addTorso()
    {
        eyes_only.SetActive(false);
        head.SetActive(false);
        torsoHead.SetActive(false);
        torso.SetActive(true);
        fullbody.SetActive(false);
        // adjust hitbox
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.857f, 1.4f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.008f, -0.31f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.65f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.4f);
        // adjust sight light
        //gameObject.GetComponent<Light2D>().transform.position = new Vector3(-0.2f, 0f);
    }
    public void addFullBody()
    {
        eyes_only.SetActive(false);
        head.SetActive(false);
        torsoHead.SetActive(false);
        torso.SetActive(false);
        fullbody.SetActive(true);
        // adjust hitbox
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.857f, 1.4f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.008f, -0.31f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.69f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0f, -0.4f);
    }
}
