using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ManagerInput : MonoBehaviour
{   
    public static ManagerInput instance = default;
    IEnumerator listeneToDevices;

    bool listening = false;
    List<InputDevice> inputDevices = new List<InputDevice>();

    [SerializeField] GameObject playerPrefab;

    [SerializeField]private List<PlayerConfiguration> playerConfigs = new List<PlayerConfiguration>();

    public List<PlayerConfiguration> PlayerConfigs {
        get{ return playerConfigs; }
    }

    [SerializeField] int maxPlayers = 2;

    Dictionary<int, Dictionary<int, GameObject>> cameraDicts = new Dictionary<int, Dictionary<int, GameObject>>();

    public PlayerSetupMenuController[] playerSetupMenus;

    [SerializeField] 
    PlayerSelectionSetupChannelSO playerSelectionSetupEvent;

    [SerializeField]
    ChangeSceneEventChannelSO loadSceneEvent;

    [SerializeField]
    IntEventChannelSO readyPlayerEvent;

    [SerializeField] 
    PlayerGameInitializationChannelSO gameInitializationChannel;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if(scene.name.CompareTo(SceneChangeManager.scenesDict[SceneNames.Game]) == 0){
            Debug.Log(playerConfigs.Count);
            gameInitializationChannel.RaiseEvent(playerConfigs.ToArray());
        }
        if(scene.name.CompareTo(SceneChangeManager.scenesDict[SceneNames.SelectionScreen]) == 0){
            foreach (var item in playerConfigs.ToArray())
            {
                Destroy(item.input.gameObject);
            }
            playerConfigs.Clear();
        }
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    private void OnEnable()
	{
        SceneManager.sceneLoaded += OnSceneLoaded;
        readyPlayerEvent.OnEventRaised += ReadyPlayer;
	}

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        readyPlayerEvent.OnEventRaised -= ReadyPlayer;
    }

    private void Awake() {

        playerConfigs.Clear();

    }
    
    public void PlayerJoin(PlayerInput playerInput){
        if (SceneManager.GetActiveScene().name == SceneChangeManager.scenesDict[SceneNames.SelectionScreen])
        {
            if(!playerConfigs.Any(pi => playerInput.playerIndex == pi.playerIndex)){
                playerSelectionSetupEvent.RaiseEvent(playerInput);
                playerConfigs.Add(new PlayerConfiguration(playerInput));
                Debug.Log(playerConfigs[playerConfigs.Count-1]);
                Debug.Log(playerInput);
                //Debug.Log(playerConfigs.Count);
                //Debug.Log("HEre");
            }
        }
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady=true;
        if(playerConfigs.Count <= maxPlayers && PlayerConfigs.Count > 1 && playerConfigs.All(p => p.isReady ==true)){
            foreach (var item in playerConfigs)
            {
                item.input.transform.SetParent(transform);
            }
            loadSceneEvent.RaiseEvent(SceneNames.Game);
        }
    }

    public void ListenDevices(bool turnOn=true){
        if(listeneToDevices!=null){
            StopCoroutine(listeneToDevices);
        }
        if(turnOn){
            listening = true;
            //listeneToDevices = ListeneToDevicesRoutine();
            StartCoroutine(listeneToDevices);
        }
        
    }
}

[System.Serializable]
public struct PlayerCamera
{
    public GameObject camera;
    public int playerIndex;
}

[System.Serializable]
public struct CamerasByMaxplayer
{
    public int maxPlayerSetting;
    public List<PlayerCamera> cameras;
}

[System.Serializable]
public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput playerInput)
    {
        playerIndex = playerInput.playerIndex;
        input = playerInput;
    }

    public PlayerInput input { get; private set; }
    public int playerIndex { get; private set; }
    public bool isReady { get; set; }
    public Transform respawn {get; set;}
}

