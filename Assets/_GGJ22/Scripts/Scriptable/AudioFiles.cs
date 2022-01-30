using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioKey{
    arrowSwitch, enter, switchKaiju, 
}

/// <summary>
/// This class is a used for scene loading events.
/// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
/// </summary>
[CreateAssetMenu(menuName = "FileContainer/ Audio File Container")]
public class AudioFiles : ScriptableObject
{
    [SerializeField]
    List<AudioKeyFile> AudioList = default;

    Dictionary<AudioKey, List<AudioClip>> audioDict = new Dictionary<AudioKey, List<AudioClip>>();


    private void Awake() {
        SetAudioDict();
        
    }

    public void SetAudioDict(){
        foreach (var item in AudioList)
        {
            if (!audioDict.ContainsKey(item.audioKey))
            {
                audioDict.Add(item.audioKey, new List<AudioClip>());
            }
            audioDict[item.audioKey].Add(item.audioClip);
        }
    }

    public AudioClip GetClip(AudioKey audioKey){
        return (audioDict.ContainsKey(audioKey))? audioDict[audioKey][Random.Range(0, audioDict[audioKey].Count)] : null;
    } 
}

[System.Serializable]
public struct AudioKeyFile{
    public AudioKey audioKey;
    public AudioClip audioClip;
} 
