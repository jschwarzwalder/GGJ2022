using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    [SerializeField]
    Player playerRef;

    [SerializeField] 
    TransformEventChannelSO appearCue;
    [SerializeField] 
    TransformEventChannelSO disappearCue;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if((other.CompareTag("PlantInteractiveObject") && playerRef.CharachterId == InteractiveCharacterId.plantDog) ||
           (other.CompareTag("RobotInteractiveObject") && playerRef.CharachterId == InteractiveCharacterId.robot)){
               appearCue.RaiseEvent(other.transform);
               playerRef.SetObjectToInteract(other.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if((other.CompareTag("PlantInteractiveObject") && playerRef.CharachterId == InteractiveCharacterId.plantDog) ||
           (other.CompareTag("RobotInteractiveObject") && playerRef.CharachterId == InteractiveCharacterId.robot)){
               disappearCue.RaiseEvent(other.transform);
               playerRef.SetObjectToInteract(null);
        }
    }
}
