using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "Events/Player Game Initialization Event Channel")]
public class PlayerGameInitializationChannelSO : ScriptableObject
{
    //maybe think about generalize this 
	public UnityAction<PlayerConfiguration[]> OnPlayerConfigurationsRaised;
	public void RaiseEvent(PlayerConfiguration[] playerConfigurations)
	{
		OnPlayerConfigurationsRaised.Invoke(playerConfigurations);
	}
}
