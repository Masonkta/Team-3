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
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.8f, 0.1f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.055f, -0.25f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.7f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, -0.3f);
    }
    public void addHead()
    {
        eyes_only.SetActive(false);
        head.SetActive(true);
        torsoHead.SetActive(false);
        torso.SetActive(false);
        fullbody.SetActive(false);
        // adjust hitbox
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.7f, 0.9f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.04f, -0.05f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.7f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0.035f, -0.1f);
    }
    public void addTorsoHead()
    {
        eyes_only.SetActive(false);
        head.SetActive(false);
        torsoHead.SetActive(true);
        torso.SetActive(false);
        fullbody.SetActive(false);
        // adjust hitbox
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.9f, 2.2f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.045f, -0.07f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.95f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0.03f, -0.35f);
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
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.9f, 2.2f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.045f, -0.07f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.95f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0.03f, -0.35f);
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
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.9f, 2.25f);
        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.04f, -0.55f);
        // adjust interact zone
        gameObject.GetComponent<CircleCollider2D>().radius = 0.95f;
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0.03f, -0.35f);
    }
}
