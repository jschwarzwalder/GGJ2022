using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is a used for audioCues.
/// </summary>
[CreateAssetMenu(menuName = "Events/Request AudioClip")]
public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioKey, Vector3> OnAudioRequest;

	public void RaiseEvent(AudioKey audioKey, Vector3 pos)
	{
		if (OnAudioRequest != null)
		{
			OnAudioRequest.Invoke(audioKey, pos);
		}
		else
		{
			Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
				"Check why there is no SceneLoader already present, " +
				"and make sure it's listening on this Load Event channel.");
		}
	}
}
