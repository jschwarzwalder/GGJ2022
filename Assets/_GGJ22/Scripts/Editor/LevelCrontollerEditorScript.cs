using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelController))]
public class LevelCrontollerEditorScript : Editor {
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox("Try generation a camera", MessageType.Info);
        LevelController logInManager = (LevelController)target;
        /*if(GUILayout.Button("instance a camera")){
            logInManager.SetCamera();
        }*/

    }
}
