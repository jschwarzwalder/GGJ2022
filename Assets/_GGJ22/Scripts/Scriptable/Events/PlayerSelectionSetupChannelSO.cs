using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "Events/Selection Setup Event Channel")]
public class PlayerSelectionSetupChannelSO : ScriptableObject
{
    public UnityAction<PlayerInput> OnSelectionSetupEventRequested;

	public void RaiseEvent(PlayerInput playerInput)
	{
		if (OnSelectionSetupEventRequested != null)
		{
			OnSelectionSetupEventRequested.Invoke(playerInput);
		}
		else
		{
			Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
				"Check why there is no SceneLoader already present, " +
				"and make sure it's listening on this Load Event channel.");
		}
	}
}
