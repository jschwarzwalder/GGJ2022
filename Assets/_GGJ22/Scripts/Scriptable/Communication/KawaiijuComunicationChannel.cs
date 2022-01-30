using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KawaiijuComunicationChannel", menuName = "KawaiijuComunicationChannel")]
public class KawaiijuComunicationChannel : ScriptableObject
    {
        public string damageLayerName;

        [SerializeField] LayerMask punchable;
    }