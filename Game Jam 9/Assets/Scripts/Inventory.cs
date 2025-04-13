using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool carryingPart;
    public int partNum;
    public void addPartItem(int num)
    {
        carryingPart = true;
        partNum = num;
    }
    public void removePartItem(int num)
    {
        carryingPart = false;
        partNum = 0;
    }
}
