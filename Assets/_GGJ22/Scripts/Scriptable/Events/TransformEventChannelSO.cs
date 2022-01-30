using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
public class TransformEventChannelSO : ScriptableObject
{
    //maybe think about generalize this 
	public UnityAction<Transform> OnTransformRaised;
	public void RaiseEvent(Transform transform)
	{
		OnTransformRaised.Invoke(transform);
	}
}