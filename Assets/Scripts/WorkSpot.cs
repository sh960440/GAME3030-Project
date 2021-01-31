using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSpot : MonoBehaviour
{
    public PlayerController player;
    public Transform itemPosition;
    public GameObject[] items;
    public Objs itemOnThis = Objs.NONE;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material brightMaterial;

    void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LOS"))
        {
            player.workSpot = this;
            GetComponent<MeshRenderer>().material = brightMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LOS"))
        {
            player.workSpot = null;
            GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }

    public void SpawnObj(Objs obj)
    {
        itemOnThis = obj;

        switch (obj)
        {
            case Objs.PLATE:
                GameObject plate = Instantiate(items[0], itemPosition);
                break;
            case Objs.RAW_STEAK:
                GameObject rawSteak = Instantiate(items[1], itemPosition);
                break;
            case Objs.COOKED_STEAK:
                GameObject cookedSteak = Instantiate(items[2], itemPosition);
                break;
            case Objs.OVERCOOKED_STEAK:
                GameObject overcookedSteak = Instantiate(items[3], itemPosition);
                break;
            case Objs.RAW_STEAK_IN_PLATE:
                GameObject rawSteakPlate = Instantiate(items[4], itemPosition);
                break;
            case Objs.COOKED_STEAK_IN_PLATE:
                GameObject cookedSteakPlate = Instantiate(items[5], itemPosition);
                break;
            case Objs.OVERCOOKED_STEAK_IN_PLATE:
                GameObject overcookedSteakPlate = Instantiate(items[6], itemPosition);
                break;
        }
    }

    public void RemoveItemOnTop()
    {
        Destroy(itemPosition.GetChild(0).gameObject);
        itemOnThis = Objs.NONE;
    }
}
