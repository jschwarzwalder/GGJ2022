using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxCode
{
    possesion, click, openSpaceDoors, closeSpaceDoor, openingDock1, openingDock2, openingBothDocks,
    blueShellSpace1, blueShellSpace2,
    Meh, frightened, puke, possesionFail, possessionSuccess,
    SpaceSteps,
    SpaceSpaceShipArraving, SpaceShipLeaving , SpaceShipLoop,
    
    none,

    //Hell
    HellDemonLaugh, HellSpawner,HellAbbiReachOut, HellContractSpawns,
    HellAbbiReturns, HellTileRumbleIntro, HellTileRumbleLoop, HellTileRumbleOut,
    HellTileUp, HellLavaSplash, HellHumanBurning, HellTileDown, HellIntroClip

}
public class PoolManager : MonoBehaviour
{

    // the prefabs of the pools
    public GameObject audioObject;

    public List<AudioInfo> audioInfoList = new List<AudioInfo>();

    List<SfxAudioObject> audioPool = new List<SfxAudioObject>();

    Dictionary<AudioKeys, AudioInfo> audioDic =
            new Dictionary<AudioKeys, AudioInfo>();

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            audioPool.Add(Instantiate(audioObject, transform.position + Vector3.up * 1.5f, Quaternion.identity, transform).GetComponent<SfxAudioObject>());
        }

        foreach (AudioInfo audioI in audioInfoList)
        {
            audioDic.Add(audioI.audioID, audioI);
        }

    }

    public void PlaySfx(Transform parent, AudioKeys clipCode, bool canInterrupt = true, bool force2Play = false,float relation=1)
    {
        if (audioDic.ContainsKey(clipCode))
        {
            SfxAudioObject res = null;
            foreach (SfxAudioObject item in audioPool)
            {
                if (item.CanBeIntrrupt || !item.IsPlaying)
                {
                    res = item;
                    break;
                }
            }
            if (res != null)
            {
                res.PlaySound(parent, audioDic[clipCode].clip[Random.Range(0, audioDic[clipCode].clip.Length)], canInterrupt,false,relation);
            }
            else if (force2Play)
            {
                audioPool.Add(Instantiate(audioObject, transform.position + Vector3.up * 1.5f, Quaternion.identity, transform).GetComponent<SfxAudioObject>());
                audioPool[audioPool.Count - 1].PlaySound(parent, audioDic[clipCode].clip[Random.Range(0, audioDic[clipCode].clip.Length)], canInterrupt,false,relation);
            }
        }
    }


    public void PlaySfx(Transform parent, AudioClip clip, bool canInterrupt = true, bool force2Play = false, float relation = 1, float max = 1, float min = 0.35f)
    {
        
        SfxAudioObject res = null;
        foreach (SfxAudioObject item in audioPool)
        {
            if (item.CanBeIntrrupt || !item.IsPlaying)
            {
                res = item;
                break;
            }
        }
        if (res != null)
        {
            res.PlaySound(parent, clip, canInterrupt, false, relation,max,min);
        }
        else if (force2Play)
        {
            audioPool.Add(Instantiate(audioObject, transform.position + Vector3.up * 1.5f, Quaternion.identity, transform).GetComponent<SfxAudioObject>());
            audioPool[audioPool.Count - 1].PlaySound(parent, clip, canInterrupt, false, relation,max,min);
        }
        
    }
    public AudioClip GetAudioClip(AudioKeys audioId)
    {
        return (audioDic.ContainsKey(audioId)) ? audioDic[audioId].clip[Random.Range(0, audioDic[audioId].clip.Length)]: null;
    }

    public void ResetAudio()
    {
        foreach (SfxAudioObject item in audioPool)
        {
            item.transform.SetParent(transform);
        }
    }
}

