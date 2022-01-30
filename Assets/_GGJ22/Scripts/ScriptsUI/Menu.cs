using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    ChangeSceneEventChannelSO loadSceneEvent;

    public void PlayButton()
    {
        loadSceneEvent.RaiseEvent(SceneNames.SelectionScreen);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
