using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public Transform respawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.SendMessage("RespawnDeathPlayer");
        }else if(!other.transform.CompareTag("Building")){
            other.gameObject.SetActive(false);
        }
    }
}
