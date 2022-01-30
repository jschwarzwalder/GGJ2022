using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioFiles audioFiles;

    [SerializeField]
    AudioEventChannelSO audioEventChannel;

    [SerializeField]
    GameObject AudioPrefab;
    List<AudioSource> audioSources = new List<AudioSource>();

    void OnEnable() {
        audioEventChannel.OnAudioRequest += RequestAudio;    
    }

    void OnDisable()
    {
        audioEventChannel.OnAudioRequest -= RequestAudio;
    }

    private void Awake() 
    {
        audioFiles.SetAudioDict();
        for (int i = 0; i < 20; i++)
        {
            audioSources.Add(Instantiate(AudioPrefab, transform.position, Quaternion.identity, transform).GetComponent<AudioSource>());
        }
    }


    public void RequestAudio(AudioKey audioKey, Vector3 pos){
        //Debug.Log("here");
        AudioClip audioToPlay = audioFiles.GetClip(audioKey);
        //Debug.Log(audioToPlay);
        AudioSource audioSource = null;
        if(audioToPlay!=null){
            foreach (var item in audioSources)
            {
                if (!item.isPlaying)
                {
                    audioSource = item;
                    break;
                }
            }
            if(audioSource == null){
                audioSources.Add(Instantiate(AudioPrefab, transform.position, Quaternion.identity, transform).GetComponent<AudioSource>());
                audioSource = audioSources[audioSources.Count-1];
            }
            audioSource.PlayOneShot(audioToPlay, 1);
        }
    }
}
