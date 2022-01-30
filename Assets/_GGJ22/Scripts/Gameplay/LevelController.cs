using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class LevelController : MonoBehaviour
{
    [SerializeField] Player [] players; //remember to set the first one(index 0) as the dog and (index 1) the robot
    [SerializeField] Transform [] currentSpawnPionts;

    Camera camera = default;
    int num = 0;

    public int playersOnScene = 0;

    [SerializeField] 
    PlayerGameInitializationChannelSO gameInitializationChannel;


    private void Awake() {
        
    }


    private void OnEnable()
	{
        gameInitializationChannel.OnPlayerConfigurationsRaised += StartGame;
	}

    private void OnDisable()
    {
        gameInitializationChannel.OnPlayerConfigurationsRaised -= StartGame;
    }
    public virtual void StartGame(PlayerConfiguration[] playerConfigurations){
        Debug.Log(playerConfigurations.Length);
        Player player = null;
    
        for (int i = 0; i < playerConfigurations.Length; i++)
        {
            player = players[playerConfigurations[i].playerIndex];
            if (player != null) {
                playersOnScene = playersOnScene + 1;
                //cameraFollow[i].transform.position = player.transform.position;
                //cameraFollow[i].transform.SetParent(player.transform);
                playerConfigurations[i].respawn = currentSpawnPionts[i];
                player.InitializePlayer(playerConfigurations[i], this);
            } else {
                Debug.Log("Something Wrong with the players spawners");
            }
        }
    }

    public string GetDogInteraction(){
        Debug.Log(players[0].CurrentPlayerInput);
        return "";
    }

    public void EndGame(){

    }

    void SetPlayers()
    {   
         
    }
}
