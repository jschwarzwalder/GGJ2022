using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishlevel : MonoBehaviour
{
    [SerializeField]
    SceneNames sceneToLoad;

    [SerializeField]
    ChangeSceneEventChannelSO loadSceneEvent;

    [SerializeField]
    VoidEventChannelSO showArtEvent;

    IEnumerator moveRoutine = default;

    bool active = false;

    private void OnTriggerEnter2D(Collider2D other) {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (!active)
        {
            active = true;
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
            }
            moveRoutine = ChangeRoutine();
            StartCoroutine(moveRoutine);
        }
    }


    private IEnumerator ChangeRoutine()
    {
        if(sceneToLoad == SceneNames.MainMenu){
            showArtEvent.RaiseEvent();
            yield return new WaitForSeconds(4);
        }
        yield return null;
        loadSceneEvent.RaiseEvent(sceneToLoad);

    }



    
}
