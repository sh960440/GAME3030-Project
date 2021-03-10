using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSpot : MonoBehaviour
{
    public PlayerController player;
    public Transform itemPosition;
    public GameObject[] itemPrefabs;
    public Item itemOnSpot = Item.NONE;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material brightMaterial;

    void Start()
    {
        itemOnSpot = Item.NONE;
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

    public void SpawnObj(Item obj)
    {
        itemOnSpot = obj;
        GameObject plate = Instantiate(itemPrefabs[(int)obj], itemPosition);
    }

    public void RemoveItemOnTop()
    {
        Destroy(itemPosition.GetChild(0).gameObject);
        itemOnSpot = Item.NONE;
    }
}
