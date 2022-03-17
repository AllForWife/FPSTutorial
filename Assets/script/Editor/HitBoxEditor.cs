using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(HitBoxManage))]
// 自定義script 類型為HitBoxManage
public class HitBoxEditor : Editor
{
    public override void OnInspectorGUI()
    {   
        HitBoxManage hitBoxManage = (HitBoxManage)target;
        //獲得HitBoxManage這個當前目標
        DrawDefaultInspector();
        //繪製script上的參數

        if(GUILayout.Button("Generate")){
            hitBoxManage.HitBoxGenerate();
            //繪製一個叫做"Generate"的按鈕 按下則觸發hitBoxManage.HitBoxGenerate()這個方法
        }
    }
}
