using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public Item desiredDish;
    public Image dishImage;
    public Image timerImage;
    public Sprite[] dishSprites;
    public float targetTime;
    public float remainingTime;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime >= 0.0f)
        {
            remainingTime -= Time.deltaTime;
            timerImage.fillAmount = remainingTime / targetTime;
        }
        else
        {
            GameManager.Instance.DelieverFood(Item.NONE);
            Reset();
            this.gameObject.SetActive(false);
        }
    }

    public void NewOrder(Item order)
    {
        desiredDish = order;

        switch (order)
        {
            case Item.COOKED_STEAK_IP:
                dishImage.sprite = dishSprites[0];
                targetTime = 30.0f;
                remainingTime = 30.0f;
                break;
            case Item.COOKED_FRIES_IP:
                dishImage.sprite = dishSprites[1];
                targetTime = 40.0f;
                remainingTime = 40.0f;
                break;
            case Item.COOKED_CHICKEN_IP:
                dishImage.sprite = dishSprites[2];
                targetTime = 40.0f;
                remainingTime = 40.0f;
                break;
            case Item.DRINK:
                dishImage.sprite = dishSprites[3];
                targetTime = 20.0f;
                remainingTime = 20.0f;
                break;
        }
    }

    public void Reset()
    {
        desiredDish = Item.NONE;

        targetTime = 0.0f;
        remainingTime = 0.0f;

        timerImage.fillAmount = 1.0f;
    }
}
