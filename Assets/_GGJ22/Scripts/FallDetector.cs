using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public Transform respawn;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlantDog") || other.CompareTag("Robot"))
        {
            other.transform.position = respawn.position;
        }
    }
}
