using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractiveCharacterId{
    plantDog, robot
}

public class PullPushObject : MonoBehaviour
{
    [SerializeField]
    InteractiveCharacterId characterId;
    
    [SerializeField]
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other) {
        if((other.CompareTag("PlantDog") && characterId != InteractiveCharacterId.plantDog) ||
           (other.CompareTag("Robot") && characterId != InteractiveCharacterId.robot)){
               rb.constraints =  RigidbodyConstraints2D.FreezePosition;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if((other.CompareTag("PlantDog") && characterId != InteractiveCharacterId.plantDog) ||
           (other.CompareTag("Robot") && characterId != InteractiveCharacterId.robot)){
               rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
