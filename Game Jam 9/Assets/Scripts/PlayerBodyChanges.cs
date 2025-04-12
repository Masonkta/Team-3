using UnityEngine;

public class PlayerBodyChanges : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public bool hasHead;
    public bool hasLegs;
    public bool hasArms;


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
}
