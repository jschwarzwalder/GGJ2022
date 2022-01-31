using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionUIController : MonoBehaviour
{
    [SerializeField]
    public GameObject playerSetupMnuPrefab;

    public GameObject mainLayout;

    public Camera selectionCamera;

    [Header("Load Event")]
    [SerializeField] 
    PlayerSelectionSetupChannelSO playerSelectionSetupChannelEvent;

    PlayerSetupMenuController[] playerSetupMenus;

    [SerializeField] 
    GameObject pressToJoinUI;

    [SerializeField]
    GameObject[] characters; //default 0 is dog 1 is robot

    private void OnEnable()
	{
		playerSelectionSetupChannelEvent.OnSelectionSetupEventRequested += SetPlayerSetupUI;
	}

    private void OnDisable()
    {
        playerSelectionSetupChannelEvent.OnSelectionSetupEventRequested -= SetPlayerSetupUI;
    }

    private void Awake() {
        playerSetupMenus = new PlayerSetupMenuController[4];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
    }

    public void SetPlayerSetupUI(PlayerInput playerInput){
        playerSetupMenus[playerInput.playerIndex] = Instantiate(playerSetupMnuPrefab, transform.position, Quaternion.identity, null).GetComponent<PlayerSetupMenuController>();
        playerSetupMenus[playerInput.playerIndex].SetUIParent(playerInput, mainLayout, selectionCamera, characters[playerInput.playerIndex]);
        Debug.Log(playerInput.playerIndex);
        if (playerInput.playerIndex >= 1)
        {
            pressToJoinUI.SetActive(false);
        }
        ResetUIPlayersPosition();
    }

    public void ResetUIPlayersPosition(){
        for (int i = 0; i < playerSetupMenus.Length; i++)
        {
            if (playerSetupMenus[i] != null)
            {
                playerSetupMenus[i].ResetPos();
            }
        }
    }

}
