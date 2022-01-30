using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{   
    [SerializeField]
    GameObject vineGO;
    
    [SerializeField]
    Transform startPoint;

    [SerializeField]
    Transform endPoint;

    [SerializeField]
    float timeMoving=3;

    [SerializeField]
    float timeStaying=2;
    [SerializeField]
    float timeBack=1;
    
    [SerializeField] 
    TransformEventChannelSO appearCue;
    [SerializeField] 
    TransformEventChannelSO disappearCue;
    [SerializeField] 
    TransformEventChannelSO activateEvent;

    bool active = false;

    IEnumerator moveRoutine = default;

    float timeToWait;

    private void OnEnable()
	{
        activateEvent.OnTransformRaised += Interact;
        appearCue.OnTransformRaised += AppearCue;
        disappearCue.OnTransformRaised += DisappearCue;
	}

    private void OnDisable()
    {
        activateEvent.OnTransformRaised -= Interact;
        appearCue.OnTransformRaised -= AppearCue;
        disappearCue.OnTransformRaised -= DisappearCue;
    }
    private void Awake() {
        startPoint.position = vineGO.transform.position;
        startPoint.SetParent(null);
        endPoint.SetParent(null);
    }

    public void  Interact(Transform t){
        if(t == transform && !active){
            Move();
        }
    }

    public void AppearCue(Transform t){
        if(t == transform){

        }
    }

    public void DisappearCue(Transform t){
        if(t == transform){

        }
    }

    private void Move()
    {
        active = true;
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        moveRoutine = MoveRoutine(startPoint.position, endPoint.position, timeMoving,true);
        StartCoroutine(moveRoutine);
    }


    private IEnumerator MoveRoutine(Vector3 start, Vector3 end, float timeGrowing, bool wait=false)
    {
        timeToWait = timeGrowing;
        while (timeToWait > 0)
        {
            vineGO.transform.position = Vector3.Lerp(start, end, 1 - (timeToWait / timeGrowing));
            timeToWait -= Time.deltaTime;
            yield return null;
        }
        if(wait){
            yield return new WaitForSeconds(timeStaying);
            yield return MoveRoutine(endPoint.position, startPoint.position, timeBack);
        }else{
            active = false;
        }


    }
}
