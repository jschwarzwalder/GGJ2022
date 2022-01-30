using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "Events/Kawaiiju Event Channel")]
public class KawaiijuEventChannelSO : ScriptableObject
{
	//add an interaction type
    public UnityAction<Player, Transform> OnKawaiijuCallRequested;

	public void RaiseEvent(Player caller, Transform receptor)
	{
		if (OnKawaiijuCallRequested != null)
		{
			OnKawaiijuCallRequested.Invoke(caller, receptor);
		}
		else
		{
			Debug.LogWarning("A Kawaiiju interaction event was requested, but nobody picked it up." +
				"Check why there is no SceneLoader already present, " +
				"and make sure it's listening on this Load Event channel.");
		}
	}
}
