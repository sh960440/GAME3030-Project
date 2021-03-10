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
                player.facingSpotType = SpotType.WORKAREA;
                break;
            case "DeliveryPlace":
                player.facingSpotType = SpotType.DELIVERY;
                break;
            case "PlateStack":
                player.facingSpotType = SpotType.PLATES;
                break;
            case "TrashBox":
                player.facingSpotType = SpotType.TRASH;
                break;
            case "Stove":
                player.facingSpotType = SpotType.STOVE;
                break;
            case "Meat":
                player.facingSpotType = SpotType.MEAT;
                break;
            case "Fries":
                player.facingSpotType = SpotType.FRIES;
                break;
            case "Fryer":
                player.facingSpotType = SpotType.FRYER;
                break;
            case "Microwave":
                //player.facingSpotType = SpotType.FRIES;
                break;
            case "Chicken":
                //player.facingSpotType = SpotType.FRIES;
                break;
            case "Cups":
                //player.facingSpotType = SpotType.FRIES;
                break;
            case "Drink":
                //player.facingSpotType = SpotType.FRIES;
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WorkSpot") || 
            other.CompareTag("DeliveryPlace") || 
            other.CompareTag("PlateStack") || 
            other.CompareTag("TrashBox") || 
            other.CompareTag("Stove") || 
            other.CompareTag("Meat") ||
            other.CompareTag("Fries") ||
            other.CompareTag("Fryer") ||
            other.CompareTag("Microwave") ||
            other.CompareTag("Chicken") ||
            other.CompareTag("Cups") ||
            other.CompareTag("Drink"))
        {
            player.facingSpotType = SpotType.NONE;
        }
    }
}