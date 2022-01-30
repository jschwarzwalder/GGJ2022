using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SfxAudioObject : MonoBehaviour
{
    AudioSource audio;
    bool canBeInterrupt;
    public bool CanBeIntrrupt
    {
        get
        {
            return canBeInterrupt;
        }
        set
        {
            canBeInterrupt = value;
        }
    }
    bool isPlaying;
    public bool IsPlaying
    {
        get
        {
            return audio.isPlaying;
        }
    }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(Transform parent, AudioClip clip, bool interrupt = false, bool loop=false, float relation=1, float maxVolume=1, float minVolume=0.35f)
    {
        if (canBeInterrupt || !IsPlaying)
        {
            canBeInterrupt = interrupt;
            if (parent != null)
            {
                transform.position = parent.position;
            }
            audio.pitch = relation;
            //transform.parent = parent;
            audio.loop = loop;
            audio.volume = Random.Range(minVolume, maxVolume);
            audio.clip = clip;
            audio.Play();
        }
        
    }

    public void Stop()
    {
        audio.Stop();
    }

}

