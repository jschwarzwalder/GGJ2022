using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using TMPro;

public class PlayerSetupMenuController : MonoBehaviour
{
    [SerializeField] InputSystemUIInputModule inputModule;

    int playerIndex;
    
    [SerializeField]
    TextMeshProUGUI titleText;
    
    [SerializeField]
    GameObject menuPanel;
    
    [SerializeField]
    GameObject readyPanel;

    [SerializeField]
    Button readyBtn;

    [SerializeField]
    RectTransform rectTransform;

    [SerializeField]
    RectTransform centerPos;

    float ignoreInputTime = 1.5f;
    float lastIgnoreTime = 0f;
    bool inputEnable;

    int currentColor=0;

    int currentKawaiiju=0;

    //GameObject playerRef;

    List<SpriteRenderer> playerMeshRenderer = new List<SpriteRenderer>();

    RaycastHit hit;

    Ray ray;

    Vector3 targetLookPosition;

    IEnumerator resetPosRoutine;

    Camera cameraRef;

    [SerializeField]
    IntEventChannelSO readyPlayerEvent;

    [SerializeField]
    AudioEventChannelSO audioRequestChennelSO;

    Material currentMaterial;

    GameObject selectedCharacter;

    public void SetUIParent(PlayerInput input, GameObject mainLayout, Camera selectionCamera, GameObject character) {
        cameraRef = selectionCamera;
        input.uiInputModule = inputModule;
        transform.SetParent(mainLayout.transform);
        rectTransform.localScale = Vector3.one;
        SetPlayerIndex(input.playerIndex);

        selectedCharacter = character;

        
        ResetPos();
    }

    public void ResetPos(){
        if(resetPosRoutine!= null){
            StopCoroutine(resetPosRoutine);
        }
        resetPosRoutine = ResetPositionRoutine();
        StartCoroutine(resetPosRoutine);
    }

    IEnumerator ResetPositionRoutine(){
        yield return new WaitForSeconds(0.05f);
        ray = cameraRef.ScreenPointToRay(centerPos.position);
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        selectedCharacter.transform.position = hit.point;
        selectedCharacter.SetActive(true);
    }

    public void SetPlayerIndex(int pi)
    {
        audioRequestChennelSO.RaiseEvent(AudioKey.enter, transform.position);
        playerIndex = pi;
        titleText.SetText("Player "+ (pi+1).ToString());
        lastIgnoreTime = Time.time + ignoreInputTime;
    }

    public void ReadyPlayer()
    {
        if(Time.time > lastIgnoreTime){
            inputEnable = true;
        }
        if(!inputEnable){
            return;
        }
        audioRequestChennelSO.RaiseEvent(AudioKey.enter, transform.position);
        readyPlayerEvent.RaiseEvent(playerIndex);
        menuPanel.SetActive(false);
        lastIgnoreTime = Time.time + ignoreInputTime;
    }

}

