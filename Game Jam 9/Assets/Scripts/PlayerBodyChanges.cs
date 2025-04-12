using UnityEngine;

public class PlayerBodyChanges : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;


    private bool hasHead;
    private bool hasLegs;
    private bool hasArms;

    public void bodyUpate(int num)
    {
        switch(num){
            case 1:
                p1.SetActive(true);
                hasHead = true;
                Debug.Log("PART 1");
                break;
            case 2:
                p2.SetActive(true);
                hasLegs = true;
                Debug.Log("PART 2");
                break;
            case 3:
                p3.SetActive(true);
                hasArms = true;
                Debug.Log("PART 3");
                break;
            default:
                Debug.Log("incorrect body part number");
                break;
        }
    }
}
