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
    MEAT
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
    public SpotType facingSpot = SpotType.NONE;

    // Pick/Drop
    public Transform holdingPosition;
    public Objs holdingObj = Objs.NONE;
    [SerializeField] private GameObject[] items;
    
    void Start()
    {
        facingSpot = SpotType.NONE;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > float.Epsilon || Mathf.Abs(Input.GetAxis("Vertical")) > float.Epsilon)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0.0f, Input.GetAxis("Vertical") * moveSpeed);
            destRot = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destRot, rotStep);
            controller.Move(moveDirection * Time.deltaTime);
        }

        if (Input.GetKeyDown("f"))
        {
            // 假如手上沒東西
            if (holdingObj == Objs.NONE)
            {
                // 拿起前面的東西
                switch (facingSpot)
                {
                    case SpotType.WORKAREA:
                        SpawnItemOnHands(workSpot.itemOnThis);
                        workSpot.RemoveItemOnTop();
                        break;
                    case SpotType.MEAT:
                        SpawnItemOnHands(Objs.RAW_STEAK);
                        break;
                    case SpotType.PLATES:
                        SpawnItemOnHands(Objs.PLATE);
                        break;
                    case SpotType.STOVE:
                        FindObjectOfType<StoveBehavior>().StopAllCoroutines();
                        switch (FindObjectOfType<StoveBehavior>().itemOnStove)
                        {
                            case Objs.RAW_STEAK:
                                SpawnItemOnHands(Objs.RAW_STEAK);
                                FindObjectOfType<StoveBehavior>().RemoveItemOnTop();
                                break;
                            case Objs.COOKED_STEAK:
                                SpawnItemOnHands(Objs.COOKED_STEAK);
                                FindObjectOfType<StoveBehavior>().RemoveItemOnTop();
                                break;
                            case Objs.OVERCOOKED_STEAK:
                                SpawnItemOnHands(Objs.OVERCOOKED_STEAK);
                                FindObjectOfType<StoveBehavior>().RemoveItemOnTop();
                                break;
                        }
                        break;
                }
            }
            // 假如手上有東西
            else
            {
                if (facingSpot == SpotType.WORKAREA)
                {
                    if (workSpot.itemOnThis == Objs.PLATE)
                    {
                        switch (holdingObj)
                        {
                            case Objs.RAW_STEAK:
                                workSpot.RemoveItemOnTop();
                                RemoveItemOnHands();
                                workSpot.SpawnObj(Objs.RAW_STEAK_IN_PLATE);
                                break;
                            case Objs.COOKED_STEAK:
                                workSpot.RemoveItemOnTop();
                                RemoveItemOnHands();
                                workSpot.SpawnObj(Objs.COOKED_STEAK_IN_PLATE);
                                break;
                            case Objs.OVERCOOKED_STEAK:
                                workSpot.RemoveItemOnTop();
                                RemoveItemOnHands();
                                workSpot.SpawnObj(Objs.OVERCOOKED_STEAK_IN_PLATE);
                                break;
                        }
                    }
                    else
                    {
                        workSpot.SpawnObj(holdingObj);
                        RemoveItemOnHands();
                    }
                }
                else if (facingSpot == SpotType.STOVE)
                {
                    if (holdingObj == Objs.RAW_STEAK || holdingObj == Objs.COOKED_STEAK || holdingObj == Objs.OVERCOOKED_STEAK)
                    {
                        RemoveItemOnHands();
                        FindObjectOfType<StoveBehavior>().StartCoroutine("StartCookingRawSteak");
                    }
                }
                else if (facingSpot == SpotType.DELIVERY)
                {
                    if (holdingObj == Objs.COOKED_STEAK_IN_PLATE)
                    {
                        RemoveItemOnHands();
                    }
                }
                else if (facingSpot == SpotType.TRASH)
                {
                    if (holdingObj != Objs.NONE)
                    {
                        RemoveItemOnHands();
                    }
                }
            }
        }
        if (Input.GetKeyDown("g"))
        {
            
        }
    }

    void SpawnItemOnHands(Objs obj)
    {
        holdingObj = obj;
        GameObject item = Instantiate(items[(int)obj], holdingPosition);
    }

    void RemoveItemOnHands()
    {
        Destroy(holdingPosition.GetChild(0).gameObject);
        holdingObj = Objs.NONE;
    }
}
