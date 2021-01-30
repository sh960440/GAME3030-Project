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
    [SerializeField] private float rotStep = 10.0f;
    private Vector3 moveDirection;
    private Quaternion destRot;
    public CharacterController controller;

    // Work spots
    public WorkSpot workSpot;
    public SpotType facingSpot = SpotType.NONE;

    // Pick/Drop
    public GameObject holdingPosition;
    public Objs holdingObj = Objs.NONE;
    [SerializeField] private GameObject[] items;
    //private bool isHolding = false;
    

    void Awake()
    {
        facingSpot = SpotType.NONE;
    }

    void Start()
    {
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
            //Debug.Log(facingSpot);

            if (holdingObj == Objs.NONE)
            {
                switch (facingSpot)
                {
                    case SpotType.NONE:
                        
                        break;
                    case SpotType.WORKAREA:
                        
                        break;
                    case SpotType.MEAT:
                        holdingPosition = Instantiate(items[0], holdingPosition.GetComponent<Transform>().position, Quaternion.identity, GetComponent<Transform>());
                        holdingObj = Objs.RAW_STEAK;
                        break;
                    case SpotType.PLATES:
                        holdingPosition = Instantiate(items[1], holdingPosition.GetComponent<Transform>().position, Quaternion.identity, GetComponent<Transform>());
                        holdingObj = Objs.PLATE;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (workSpot)
                {
                    switch (holdingObj)
                    {
                        case Objs.PLATE:
                            workSpot.itemPosition = Instantiate(items[1], workSpot.itemPosition.GetComponent<Transform>().position, Quaternion.identity, workSpot.itemPosition.GetComponent<Transform>());
                            //Destroy(holdingPosition);
                            Destroy(GetComponentInChildren<Plate>().gameObject);
                            holdingObj = Objs.NONE;
                            break;
                        case Objs.RAW_STEAK:
                            
                            break;
                        case Objs.COOKED_STEAK:

                            break;

                        default:
                            break;
                    }
                }
            }
            
            if (Input.GetKeyDown("g"))
            {

            }
        }
    }
}
