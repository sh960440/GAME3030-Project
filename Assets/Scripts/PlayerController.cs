using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpotType
{
    NONE,
    WORKAREA,
    DELIVERY,
    PLATES,
    TRASH,
    STOVE,
    MEAT,
    FRYER,
    FRIES,
    CHICKEN,
    CUPS,
    DRINK
}
public class PlayerController : MonoBehaviour
{
    // Movement and rotation
    public float moveSpeed;
    [SerializeField] private float rotStep = 1.0f;
    private Vector3 moveDirection;
    private Quaternion destRot;
    public CharacterController controller;

    // Work spots interaction
    public WorkSpot workSpot;
    public SpotType facingSpotType = SpotType.NONE;

    // Pick/Drop
    public Transform holdingPosition;
    public Item holdingItem = Item.NONE;
    [SerializeField] private GameObject[] items;

    [SerializeField] AudioSource pickSound;
    [SerializeField] AudioSource dropSound;
    [SerializeField] AudioSource drinkSound;
    
    void Start()
    {
        holdingItem = Item.NONE;
        facingSpotType = SpotType.NONE;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 移動
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > float.Epsilon || Mathf.Abs(Input.GetAxis("Vertical")) > float.Epsilon)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0.0f, Input.GetAxis("Vertical") * moveSpeed);
            destRot = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, rotStep);
            controller.Move(moveDirection * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 假如手上沒東西
            if (holdingItem == Item.NONE)
            {
                // 拿起前面的東西
                switch (facingSpotType)
                {
                    case SpotType.WORKAREA:
                        SpawnItemOnHands(workSpot.itemOnSpot);
                        workSpot.RemoveItemOnTop();
                        break;
                    case SpotType.MEAT:
                        SpawnItemOnHands(Item.RAW_STEAK);
                        break;
                    case SpotType.FRIES:
                        SpawnItemOnHands(Item.RAW_FRIES);
                        break;
                    case SpotType.CHICKEN:
                        SpawnItemOnHands(Item.RAW_CHICKEN);
                        break;
                    case SpotType.PLATES:
                        SpawnItemOnHands(Item.PLATE);
                        break;
                    case SpotType.STOVE:
                        FindObjectOfType<StoveBehavior>().StopAllCoroutines();
                        switch (FindObjectOfType<StoveBehavior>().itemOnStove)
                        {
                            case Item.RAW_STEAK:
                                SpawnItemOnHands(Item.RAW_STEAK);
                                FindObjectOfType<StoveBehavior>().RemoveItemOnTop();
                                break;
                            case Item.COOKED_STEAK:
                                SpawnItemOnHands(Item.COOKED_STEAK);
                                FindObjectOfType<StoveBehavior>().RemoveItemOnTop();
                                break;
                            case Item.OVERCOOKED_STEAK:
                                SpawnItemOnHands(Item.OVERCOOKED_STEAK);
                                FindObjectOfType<StoveBehavior>().RemoveItemOnTop();
                                break;
                        }
                        break;
                    case SpotType.FRYER:
                        FindObjectOfType<FryerBehavior>().StopAllCoroutines();
                        switch (FindObjectOfType<FryerBehavior>().itemOnFryer)
                        {
                            case Item.RAW_FRIES:
                                SpawnItemOnHands(Item.RAW_FRIES);
                                FindObjectOfType<FryerBehavior>().RemoveItemOnTop();
                                break;
                            case Item.COOKED_FRIES:
                                SpawnItemOnHands(Item.COOKED_FRIES);
                                FindObjectOfType<FryerBehavior>().RemoveItemOnTop();
                                break;
                            case Item.OVERCOOKED_FRIES:
                                SpawnItemOnHands(Item.OVERCOOKED_FRIES);
                                FindObjectOfType<FryerBehavior>().RemoveItemOnTop();
                                break;
                            case Item.RAW_CHICKEN:
                                SpawnItemOnHands(Item.RAW_CHICKEN);
                                FindObjectOfType<FryerBehavior>().RemoveItemOnTop();
                                break;
                            case Item.COOKED_CHICKEN:
                                SpawnItemOnHands(Item.COOKED_CHICKEN);
                                FindObjectOfType<FryerBehavior>().RemoveItemOnTop();
                                break;
                            case Item.OVERCOOKED_CHICKEN:
                                SpawnItemOnHands(Item.OVERCOOKED_CHICKEN);
                                FindObjectOfType<FryerBehavior>().RemoveItemOnTop();
                                break;
                        }
                        break;
                    case SpotType.CUPS:
                        SpawnItemOnHands(Item.CUP);
                        break;
                }
                pickSound.Play();
            }
            // 假如手上有東西
            else
            {
                switch (facingSpotType)
                {
                    case SpotType.WORKAREA:
                        if (workSpot.itemOnSpot == Item.PLATE && (holdingItem == Item.RAW_STEAK || holdingItem == Item.COOKED_STEAK || holdingItem == Item.OVERCOOKED_STEAK || holdingItem == Item.RAW_FRIES || holdingItem == Item.COOKED_FRIES || holdingItem == Item.OVERCOOKED_FRIES || holdingItem == Item.RAW_CHICKEN || holdingItem == Item.COOKED_CHICKEN || holdingItem == Item.OVERCOOKED_CHICKEN))
                        {
                            workSpot.RemoveItemOnTop();
                            switch (holdingItem)
                            {
                                case Item.RAW_STEAK:
                                    workSpot.SpawnObj(Item.RAW_STEAK_IP);
                                    break;
                                case Item.COOKED_STEAK:
                                    workSpot.SpawnObj(Item.COOKED_STEAK_IP);
                                    break;
                                case Item.OVERCOOKED_STEAK:
                                    workSpot.SpawnObj(Item.OVERCOOKED_STEAK_IP);
                                    break;
                                case Item.RAW_FRIES:
                                    workSpot.SpawnObj(Item.RAW_FRIES_IP);
                                    break;
                                case Item.COOKED_FRIES:
                                    workSpot.SpawnObj(Item.COOKED_FRIES_IP);
                                    break;
                                case Item.OVERCOOKED_FRIES:
                                    workSpot.SpawnObj(Item.OVERCOOKED_FRIES_IP);
                                    break;
                                case Item.RAW_CHICKEN:
                                    workSpot.SpawnObj(Item.RAW_CHICKEN_IP);
                                    break;
                                case Item.COOKED_CHICKEN:
                                    workSpot.SpawnObj(Item.COOKED_CHICKEN_IP);
                                    break;
                                case Item.OVERCOOKED_CHICKEN:
                                    workSpot.SpawnObj(Item.OVERCOOKED_CHICKEN_IP);
                                    break;
                                default:
                                    break;
                            }
                            RemoveItemOnHands();
                            dropSound.Play();
                        }
                        else if (workSpot.itemOnSpot == Item.NONE)
                        {
                            workSpot.SpawnObj(holdingItem);
                            RemoveItemOnHands();
                            dropSound.Play();
                        }
                        break;

                    case SpotType.STOVE:
                        if (FindObjectOfType<StoveBehavior>().itemOnStove == Item.NONE)
                        {
                            if (holdingItem == Item.RAW_STEAK || holdingItem == Item.COOKED_STEAK || holdingItem == Item.OVERCOOKED_STEAK)
                            {
                                RemoveItemOnHands();
                                FindObjectOfType<StoveBehavior>().StartCoroutine("StartCookingRawSteak");
                                dropSound.Play();
                            }
                        }
                        break;

                    case SpotType.FRYER:
                        if (FindObjectOfType<FryerBehavior>().itemOnFryer == Item.NONE)
                        {
                            if (holdingItem == Item.RAW_FRIES || holdingItem == Item.COOKED_FRIES)
                            {
                                RemoveItemOnHands();
                                FindObjectOfType<FryerBehavior>().StartCoroutine("StartCookingRawFries");
                                dropSound.Play();
                            }
                            else if (holdingItem == Item.RAW_CHICKEN || holdingItem == Item.COOKED_CHICKEN)
                            {
                                RemoveItemOnHands();
                                FindObjectOfType<FryerBehavior>().StartCoroutine("StartCookingRawChicken");
                                dropSound.Play();
                            }
                        }
                        break;

                    case SpotType.DELIVERY:
                        GameManager.Instance.DelieverFood(holdingItem);
                        RemoveItemOnHands();
                        break;

                    case SpotType.DRINK:
                        if (holdingItem == Item.CUP)
                        {
                            RemoveItemOnHands();
                            SpawnItemOnHands(Item.DRINK);
                            drinkSound.Play();
                        }
                        break;

                    case SpotType.TRASH:
                        if (holdingItem != Item.NONE)
                        {
                            RemoveItemOnHands();
                            dropSound.Play();
                        }
                        break;
                }
            }
        }
    }

    void SpawnItemOnHands(Item obj)
    {
        holdingItem = obj;
        GameObject item = Instantiate(items[(int)obj], holdingPosition);
    }

    void RemoveItemOnHands()
    {
        Destroy(holdingPosition.GetChild(0).gameObject);
        holdingItem = Item.NONE;
    }
}
