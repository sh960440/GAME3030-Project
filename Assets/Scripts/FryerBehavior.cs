using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryerBehavior : MonoBehaviour
{
    //public PlayerController player;
    public Transform itemPosition;
    public GameObject[] items;
    public Item itemOnFryer = Item.NONE;
    
    public IEnumerator StartCookingRawFries()
    {
        GameObject rawFries = Instantiate(items[0], itemPosition);
        itemOnFryer = Item.RAW_FRIES;
        yield return new WaitForSeconds(3.0f);
        RemoveItemOnTop();
        GameObject cookedFries = Instantiate(items[1], itemPosition);
        itemOnFryer = Item.COOKED_FRIES;

        StartCoroutine("CookedFriesTimer");
    }

    public IEnumerator StartCookingRawChicken()
    {
        GameObject rawChicken = Instantiate(items[3], itemPosition);
        itemOnFryer = Item.RAW_CHICKEN;
        yield return new WaitForSeconds(3.0f);
        RemoveItemOnTop();
        GameObject cookedChicken = Instantiate(items[4], itemPosition);
        itemOnFryer = Item.COOKED_CHICKEN;

        StartCoroutine("CookedChickenTimer");
    }

    public IEnumerator CookedFriesTimer()
    {
        yield return new WaitForSeconds(5.0f);
        RemoveItemOnTop();
        GameObject overcookedFries = Instantiate(items[2], itemPosition);
        itemOnFryer = Item.OVERCOOKED_FRIES;
    }

    public IEnumerator CookedChickenTimer()
    {
        yield return new WaitForSeconds(5.0f);
        RemoveItemOnTop();
        GameObject overcookedChicken = Instantiate(items[5], itemPosition);
        itemOnFryer = Item.OVERCOOKED_CHICKEN;
    }

    public void RemoveItemOnTop()
    {
        Destroy(itemPosition.GetChild(0).gameObject);
        itemOnFryer = Item.NONE;
    }
}
