using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static event Action buttonClicked;
    public void PlayButton()
    {
        //erase Later
        SceneManager.LoadScene((int)SceneCode.Selection);
        //GameManager.instance.SwitchScene((int)SceneCode.Selection);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        buttonClicked?.Invoke();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Clicked() => buttonClicked?.Invoke();
}
