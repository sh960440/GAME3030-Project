using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSpot : MonoBehaviour
{
    public PlayerController player;
    public GameObject itemPosition;
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
}
