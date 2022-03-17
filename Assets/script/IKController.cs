using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// IK 控制
public class IKController : MonoBehaviour
{
    public Transform bodyObj = null;
    public Transform leftFootObj = null; 
    public Transform rightFootObj = null; 
    public Transform leftHandObj = null; 
    public Transform rightHandObj = null; 
    public Transform lookAtObj = null; 
    public Transform rightElbowobj = null; 
    public Transform leftElbowobj = null; 
    public Transform rightKneeobj = null;
    public Transform leftKneeobj = null; 

    public Animator avatar;

    //  IK啟用
    public bool ikActive = false;



    void Start()
    {


    }



    void Update()
    {
        // 如果 IK 没有啟用
        //把對應的控制附上動畫本身的值
        if (!ikActive)
        {
            if (bodyObj != null)
            {
                bodyObj.position = avatar.bodyPosition;
                bodyObj.rotation = avatar.bodyRotation;
            }
            if (leftFootObj != null)
            {
                leftFootObj.position = avatar.GetIKPosition(AvatarIKGoal.LeftFoot);
                leftFootObj.rotation = avatar.GetIKRotation(AvatarIKGoal.LeftFoot);
            }
            if (rightFootObj != null)
            {
                rightFootObj.position = avatar.GetIKPosition(AvatarIKGoal.RightFoot);
                rightFootObj.rotation = avatar.GetIKRotation(AvatarIKGoal.RightFoot);
            }
            if (leftHandObj != null)
            {
                leftHandObj.position = avatar.GetIKPosition(AvatarIKGoal.LeftHand);
                leftHandObj.rotation = avatar.GetIKRotation(AvatarIKGoal.LeftHand);
            }
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
            if (leftElbowobj != null)
            {
                leftElbowobj.position = avatar.GetIKHintPosition(AvatarIKHint.LeftElbow);
            }
            if (rightKneeobj != null)
            {
                rightKneeobj.position = avatar.GetIKHintPosition(AvatarIKHint.RightKnee);
            }
            if (leftKneeobj != null)
            {
                leftKneeobj.position = avatar.GetIKHintPosition(AvatarIKHint.LeftKnee);
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
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0f);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0f);
            avatar.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0f);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0f);
            avatar.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            avatar.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
            avatar.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            avatar.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

            avatar.SetLookAtWeight(1.0f, 0.3f, 0.6f, 1.0f, 0.5f);

            avatar.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1.0f);
            avatar.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 0f);
            avatar.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 0f);
            avatar.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 0f);

            if (bodyObj != null)
            {
                avatar.bodyPosition = bodyObj.position;
                avatar.bodyRotation = bodyObj.rotation;
            }
            if (leftFootObj != null)
            {
                avatar.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
                avatar.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);
            }
            if (rightFootObj != null)
            {
                avatar.SetIKPosition(AvatarIKGoal.RightFoot, rightFootObj.position);
                avatar.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
            }
            if (leftHandObj != null)
            {
                avatar.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                avatar.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }
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
            if (leftElbowobj != null)
            {
                avatar.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowobj.position);
            }
            if (rightKneeobj != null)
            {
                avatar.SetIKHintPosition(AvatarIKHint.RightKnee, rightKneeobj.position);
            }
            if (leftKneeobj != null)
            {
                avatar.SetIKHintPosition(AvatarIKHint.LeftKnee, leftKneeobj.position);
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
