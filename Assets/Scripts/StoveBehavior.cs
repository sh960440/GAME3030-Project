using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBehavior : MonoBehaviour
{
    public Transform itemPosition;
    public GameObject[] items;
    public Item itemOnStove = Item.NONE;
    
    public void DropSteak(Item steak)
    {
        if (steak == Item.RAW_STEAK)
        {
            StartCoroutine(StartCookingRawSteak());
        }
        else if (steak == Item.COOKED_STEAK)
        {
            StartCoroutine(StartCookingCookedSteak());
        }
        else if (steak == Item.OVERCOOKED_STEAK)
        {
            GameObject overcookedSteak = Instantiate(items[2], itemPosition);
            itemOnStove = Item.OVERCOOKED_STEAK;
        }
    }
    IEnumerator StartCookingRawSteak()
    {
        GameObject rawSteak = Instantiate(items[0], itemPosition);
        itemOnStove = Item.RAW_STEAK;
        yield return new WaitForSeconds(4.5f);
        RemoveItemOnTop();
        StartCoroutine(StartCookingCookedSteak());
    }

    IEnumerator StartCookingCookedSteak()
    {
        GameObject cookedSteak = Instantiate(items[1], itemPosition);
        itemOnStove = Item.COOKED_STEAK;
        yield return new WaitForSeconds(6.0f);
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
