using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(HitBoxManage))]
public class HitBoxEditor : Editor
{
    public override void OnInspectorGUI()
    {   
        HitBoxManage hitBoxManage = (HitBoxManage)target;
        if(DrawDefaultInspector()){

        }

        if(GUILayout.Button("Generate")){
            hitBoxManage.HitBoxGenerate();
        }
    }
}
