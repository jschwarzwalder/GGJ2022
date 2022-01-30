
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "Events/Change scene Event Channel")]
public class ChangeSceneEventChannelSO : ScriptableObject
{
    public UnityAction<SceneNames> OnSceneChangeRequested;

	public void RaiseEvent(SceneNames sceneToSwitch)
	{
		if (OnSceneChangeRequested != null)
		{
			OnSceneChangeRequested.Invoke(sceneToSwitch);
		}
		else
		{
			Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
				"Check why there is no SceneLoader already present, " +
				"and make sure it's listening on this Load Event channel.");
		}
	}
}
