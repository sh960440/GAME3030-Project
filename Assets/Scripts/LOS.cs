using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "WorkSpot":
                player.facingSpot = SpotType.WORKAREA;
                break;
            case "DeliveryPlace":
                player.facingSpot = SpotType.DELIVERY;
                break;
            case "PlateStack":
                player.facingSpot = SpotType.PLATES;
                break;
            case "TrashBox":
                player.facingSpot = SpotType.TRASH;
                break;
            case "Stove":
                player.facingSpot = SpotType.STOVE;
                break;
            case "Meat":
                player.facingSpot = SpotType.MEAT;
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WorkSpot") || other.CompareTag("DeliveryPlace") || other.CompareTag("PlateStack") || other.CompareTag("TrashBox") || other.CompareTag("Stove") || other.CompareTag("Meat"))
        {
            player.facingSpot = SpotType.NONE;
        }
    }
}