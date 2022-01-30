using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPool : MonoBehaviour {

    public float probabilityOfScared=0.15f;
    public int populationSize;

    List<ParticlesId> ChangeTheColorParticleId= new List<ParticlesId>();

    public float probabilityOfPuker = 0.05f;
    public List<ParticlesObject> particlesPrefabs;
    // 0 is puke
    // 2 is faint
    // 3 is Meh
    Dictionary<ParticlesId, ParticlesObject > particlesRef = new Dictionary<ParticlesId, ParticlesObject>();

    Dictionary<ParticlesId, List<List<ParticleSystem>>> particlesDict = new Dictionary<ParticlesId, List<List<ParticleSystem>>>();

    ParticleSystem.MainModule auxMain;
    ParticleSystemRenderer auxRenderer;

    // Use this for initialization
    void Awake () {

        foreach (ParticlesObject item in particlesPrefabs)
        {
            particlesRef.Add(item.particleId, item);
            particlesDict.Add(item.particleId, new List<List<ParticleSystem>>());
            if (item.changeColor)
            {
                ChangeTheColorParticleId.Add(item.particleId);
            }
        }

       

        //GameObject aux;

        List<ParticleSystem> auxPs = new List<ParticleSystem>();

        //populationSize =  GameManager.instance.GetPopulationSize();
        int targetPop = Mathf.FloorToInt(populationSize * probabilityOfPuker) ;

        ParticlesId auxId = ParticlesId.puke;

        // only 
        for (int i = 0; i < targetPop; i++)
        {
            auxPs = new List<ParticleSystem>();
            foreach (ParticleSystem particleSystem in Instantiate(particlesRef[auxId].particles, transform, true).GetComponentsInChildren<ParticleSystem>())
            {
                auxPs.Add(particleSystem);
            }
            
            particlesDict[auxId].Add(auxPs);
        }


        targetPop = Mathf.FloorToInt(populationSize * probabilityOfScared);

        auxId = ParticlesId.frightened;

        for (int i = 0; i < targetPop; i++)
        {
            auxPs = new List<ParticleSystem>();
            foreach (ParticleSystem particleSystem in Instantiate(particlesRef[auxId].particles, transform, true).GetComponentsInChildren<ParticleSystem>())
            {
                auxPs.Add(particleSystem);
            }

            particlesDict[auxId].Add(auxPs);
        }

        auxId = ParticlesId.meh;

        for (int i = 0; i < 4; i++)
        {
            auxPs = new List<ParticleSystem>();
            foreach (ParticleSystem particleSystem in Instantiate(particlesRef[auxId].particles, transform, true).GetComponentsInChildren<ParticleSystem>())
            {
                auxPs.Add(particleSystem);
            }

            particlesDict[auxId].Add(auxPs);
        }

        auxId = ParticlesId.endSession;

        for (int i = 0; i < 4; i++)
        {
            auxPs = new List<ParticleSystem>();
            foreach (ParticleSystem particleSystem in Instantiate(particlesRef[auxId].particles, transform, true).GetComponentsInChildren<ParticleSystem>())
            {
                auxPs.Add(particleSystem);
            }

            particlesDict[auxId].Add(auxPs);
        }
    }


    public void PlayParticlesEffect(ParticlesId particlesId, Transform parentPosition, Vector3 offset, int playerId=0)
    {
        //print("called oparticles"+ particlesId);
        bool availablePs=false;

        List<ParticleSystem> psListRef= new List<ParticleSystem>();

        int i = 0;

        foreach (var item in particlesDict)
        {
            //first search for the plarticle id
            if (item.Key==particlesId)
            {
                //Search for an available system
                while (!availablePs && i < item.Value.Count)
                {
                    foreach (List<ParticleSystem> psList in item.Value)
                    {
                        foreach (ParticleSystem p in psList)
                        {
                            availablePs = true;
                            if (p.isPlaying)
                            {
                                availablePs = false;
                            }
                        }
                        if (availablePs)
                        {
                            psListRef = psList;
                            break;
                        }
                        i++;
                    }
                }

                //if not found, instantiate it
                if (i>= item.Value.Count-1)
                {
                    psListRef = new List<ParticleSystem>();
                    foreach (ParticleSystem particleSystem in Instantiate(particlesRef[item.Key].particles, transform, true).GetComponentsInChildren<ParticleSystem>())
                    {
                        psListRef.Add(particleSystem);
                    }

                    particlesDict[item.Key].Add(psListRef);
                }
                break;
            }
        }


        //set the particles stuff and play it
        foreach (ParticleSystem item in psListRef)
        {
            
            item.transform.position = parentPosition.position;
            //set the offset
            item.transform.position += (parentPosition.forward) * offset.z + (parentPosition.right) * offset.x + (parentPosition.up) * offset.y;
            item.transform.parent = parentPosition;
            item.Play();
        }




        /*
        switch (particlesId)
        {
            case 1:
                while (i<pukePool.Count && pukePool[i].isPlaying)
                {
                    
                    i++;
                }
                if (i == pukePool.Count)
                {
                    pukePool.Add(Instantiate(particlesPrefabs[0], Vector3.zero, Quaternion.identity, transform).GetComponent<ParticleSystem>());
                    
                }
                auxMain = pukePool[i].main;
                auxMain.startColor = GameManager.instance.GetPlayerColor(playerId);
                //auxRenderer.material.color = GameManager.instance.GetPlayerColor(playerId);
                //set the position of the particles
                pukePool[i].transform.position =  parentPosition.position;
                //set the offset
                pukePool[i].transform.position += (parentPosition.forward) *offset.z + (parentPosition.right) * offset.x + (parentPosition.up) * offset.y;
                pukePool[i].transform.parent = parentPosition;
                pukePool[i].Play();
                break;
            case 2:
                while (i < faintPool.Count && faintPool[i].isPlaying)
                {
                    
                    i++;
                }
                if (i == faintPool.Count)
                {
                    faintPool.Add(Instantiate(particlesPrefabs[0], Vector3.zero, Quaternion.identity, transform).GetComponent<ParticleSystem>());
                    faintPool[i].Play();
                }
                faintPool[i].transform.position = parentPosition.position;
                faintPool[i].transform.parent = parentPosition;
                faintPool[i].Play();
                break;
            case 3:
                while (i < mehPool.Count && (mehPool[i][0].isPlaying || mehPool[i][1].isPlaying))
                {
                    
                    i++;
                }

                if (i == mehPool.Count)
                {

                    ParticleSystem[] auxArr = new ParticleSystem[2];
                    GameObject aux = Instantiate(particlesPrefabs[2], Vector3.zero, Quaternion.identity, transform);
                    //aux.SetActive(false);
                    int j = 0;
                    foreach (ParticleSystem p in aux.GetComponentsInChildren<ParticleSystem>())
                    {
                        auxArr[j] = p;
                        j++;
                    }
                    
                    mehPool.Add(auxArr);
                    mehPool[i][1].Play();
                    mehPool[i][0].Play();
                }

                mehPool[i][0].transform.position = parentPosition.position+Vector3.up*1.5f;
                mehPool[i][0].transform.parent = parentPosition;
                mehPool[i][0].Play();
                mehPool[i][1].transform.position = parentPosition.position + Vector3.up * 1.5f;
                mehPool[i][1].transform.parent = parentPosition;
                mehPool[i][1].Play();
                break;
        }
        */
    }

    public void ResetParticles()
    {
        foreach (var item in particlesDict)
        {
            foreach (var ps in item.Value)
            {
                
                foreach (var pSystem in ps)
                {
                    pSystem.transform.parent = this.transform;
                    pSystem.Stop();
                }
            }
        }
    }
	
}

public enum ParticlesId
{
    none, puke, meh, frightened, endSession
}
[System.Serializable]
public struct ParticlesObject 
{
    public bool changeColor;
    public GameObject particles;
    public ParticlesId particleId;
}


