using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNames {Initialization, MainMenu, SelectionScreen, Game, Level_1, Level_2}

public class SceneChangeManager : MonoBehaviour
{
    public static Dictionary<SceneNames, string> scenesDict = new Dictionary<SceneNames, string>()
    {
        {SceneNames.Initialization, "Initialization"},
        {SceneNames.SelectionScreen, "SelectionScene"},
        {SceneNames.Level_1, "Level_1"},
        {SceneNames.Level_2, "Level_2"},
        {SceneNames.MainMenu, "MainMenu"},
    };

    AsyncOperation operation = default;   
    IEnumerator loadSceneRoutine;
    
    [Header("Load Event")]
    [SerializeField] 
    ChangeSceneEventChannelSO loadSceneEvent;

    private List<Scene> scenesToUnload = new List<Scene>();

    private void OnEnable()
	{
		loadSceneEvent.OnSceneChangeRequested += LoadScene;
	}

    private void OnDisable()
    {
        loadSceneEvent.OnSceneChangeRequested -= LoadScene;
    }
    private void Start()
	{
		if (SceneManager.GetActiveScene().name == scenesDict[SceneNames.Initialization])
		{
			LoadScene(SceneNames.SelectionScreen);
		}
	}

    public void LoadScene(SceneNames sceneToSwitch){
        if (operation == null || operation.isDone)
        {
            if(loadSceneRoutine != null){
                StopCoroutine(loadSceneRoutine);
            }
            loadSceneRoutine = LoadSceneRoutine(sceneToSwitch);
            StartCoroutine(loadSceneRoutine);
        }
    }

    IEnumerator LoadSceneRoutine(SceneNames sceneToSwitch){

        AddScenesToUnload();

        //Maybe add a fader here
        yield return new WaitForSeconds(0.25f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(scenesDict[sceneToSwitch], LoadSceneMode.Additive);
        

        while (!operation.isDone) {

            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesDict[sceneToSwitch]));
        UnloadScenes();
    }

    private void AddScenesToUnload()
	{
		for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name.CompareTo(scenesDict[SceneNames.Initialization]) != 0)
			{
				Debug.Log("Added scene to unload = " + scene.name);
				//Add the scene to the list of the scenes to unload
				scenesToUnload.Add(scene);
			}
		}
	}
    private void UnloadScenes()
	{
		if (scenesToUnload != null)
		{
			for (int i = 0; i < scenesToUnload.Count; ++i)
			{
				//Unload the scene asynchronously in the background
				SceneManager.UnloadSceneAsync(scenesToUnload[i]);
			}
		}
		scenesToUnload.Clear();
	}

    private void ExitGame()
	{
		Application.Quit();
		Debug.Log("Exit!");
	}
}
