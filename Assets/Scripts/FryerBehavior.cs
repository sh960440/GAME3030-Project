using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryerBehavior : MonoBehaviour
{
    public Transform itemPosition;
    public GameObject[] items;
    public Item itemOnFryer = Item.NONE;
    
    public void DropIngredient(Item ingredient)
    {
        switch (ingredient)
        {
            case Item.RAW_FRIES:
                StartCoroutine(StartCookingRawFries());
                break;
            case Item.COOKED_FRIES:
                StartCoroutine(StartCookingCookedFries());
                break;
            case Item.OVERCOOKED_FRIES:
                GameObject overcookedFries = Instantiate(items[2], itemPosition);
                itemOnFryer = Item.OVERCOOKED_FRIES;
                break;
            case Item.RAW_CHICKEN:
                StartCoroutine(StartCookingRawChicken());
                break;
            case Item.COOKED_CHICKEN:
                StartCoroutine(StartCookingCookedChicken());
                break;
            case Item.OVERCOOKED_CHICKEN:
                GameObject overcookedChicken = Instantiate(items[5], itemPosition);
                itemOnFryer = Item.OVERCOOKED_CHICKEN;
                break;
        }
    }

    IEnumerator StartCookingRawFries()
    {
        GameObject rawFries = Instantiate(items[0], itemPosition);
        itemOnFryer = Item.RAW_FRIES;
        yield return new WaitForSeconds(3.0f);
        RemoveItemOnTop();
        StartCoroutine(StartCookingCookedFries());
    }

    IEnumerator StartCookingRawChicken()
    {
        GameObject rawChicken = Instantiate(items[3], itemPosition);
        itemOnFryer = Item.RAW_CHICKEN;
        yield return new WaitForSeconds(4.0f);
        RemoveItemOnTop();
        StartCoroutine(StartCookingCookedChicken());
    }

    IEnumerator StartCookingCookedFries()
    {
        GameObject cookedFries = Instantiate(items[1], itemPosition);
        itemOnFryer = Item.COOKED_FRIES;
        yield return new WaitForSeconds(6.0f);
        RemoveItemOnTop();
        GameObject overcookedFries = Instantiate(items[2], itemPosition);
        itemOnFryer = Item.OVERCOOKED_FRIES;
    }

    IEnumerator StartCookingCookedChicken()
    {
        GameObject cookedChicken = Instantiate(items[4], itemPosition);
        itemOnFryer = Item.COOKED_CHICKEN;
        yield return new WaitForSeconds(7.0f);
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
