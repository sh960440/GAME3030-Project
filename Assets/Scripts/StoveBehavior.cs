using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBehavior : MonoBehaviour
{
    //public PlayerController player;
    public Transform itemPosition;
    public GameObject[] items;
    public Item itemOnStove = Item.NONE;
    
    public IEnumerator StartCookingRawSteak()
    {
        Debug.Log("Start Cooking");
        GameObject rawSteak = Instantiate(items[0], itemPosition);
        itemOnStove = Item.RAW_STEAK;
        yield return new WaitForSeconds(3.0f);
        RemoveItemOnTop();
        GameObject cookedSteak = Instantiate(items[1], itemPosition);
        itemOnStove = Item.COOKED_STEAK;

        StartCoroutine("CookedSteakTimer");
    }

    public IEnumerator CookedSteakTimer()
    {
        yield return new WaitForSeconds(5.0f);
        RemoveItemOnTop();
        GameObject overcookedSteak = Instantiate(items[2], itemPosition);
        itemOnStove = Item.OVERCOOKED_STEAK;
    }

    public void RemoveItemOnTop()
    {
        Destroy(itemPosition.GetChild(0).gameObject);
        itemOnStove = Item.NONE;
    }
}
