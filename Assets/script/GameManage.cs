using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GameManage : MonoBehaviour 
{
    public CinemachineVirtualCamera playerCM;

    public static GameManage gm;
    // Start is called before the first frame update
    void Awake()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
