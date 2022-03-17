using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// IK 控制
public class IKController : MonoBehaviour
{
    public Transform rightHandObj = null; 
    public Transform lookAtObj = null; 
    public Transform rightElbowobj = null; 

    public Animator avatar;

    //  IK啟用
    public bool ikActive = false;

    void Update()
    {
        // 如果 IK 没有啟用
        //把對應的控制附上動畫本身的值
        if (!ikActive)
        {
            if (rightHandObj != null)
            {
                rightHandObj.position = avatar.GetIKPosition(AvatarIKGoal.RightHand);
                rightHandObj.rotation = avatar.GetIKRotation(AvatarIKGoal.RightHand);
            }
            if (lookAtObj != null)
            {
                lookAtObj.position = avatar.bodyPosition + avatar.bodyRotation * new Vector3(0, 0.5f, 1);
            }
            if (rightElbowobj != null)
            {
                rightElbowobj.position = avatar.GetIKHintPosition(AvatarIKHint.RightElbow);
            }
        }
    }


    /// IK 動畫控制專用方法
    /// "layerIndex"動畫層
    void OnAnimatorIK(int layerIndex)
    {
        // 沒有Animator就返回
        if (avatar == null)
            return;
        // IK啟用
        //1、 各部位賦予權重值1(最大)
        //2、 各部位位置賦值
        //3、 各部位旋轉賦值

        if (ikActive)
        {
           
            avatar.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

            avatar.SetLookAtWeight(1,1,1,1,1);

            avatar.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1.0f);
            
            if (rightHandObj != null)
            {
                avatar.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                avatar.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }
            if (lookAtObj != null)
            {
                avatar.SetLookAtPosition(lookAtObj.position);
            }

            if (rightElbowobj != null)
            {
                avatar.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowobj.position);
            }
            

        }
        // IK 不啟用 
        //各部位權重歸0    
        else
        {
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            avatar.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            avatar.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            avatar.SetLookAtWeight(0.0f);
            avatar.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 0);
            avatar.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0);
            avatar.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 0);
            avatar.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 0);
        }
    }
}