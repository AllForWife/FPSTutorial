using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitBoxBone
{
    public string name;
    public Transform bone;
    [Range(0,10)]
    public float damageCoe;
    [HideInInspector]
    public DamageReceive receive;
    
    [Range(0,1)]
    public float radius;
    public HitBoxBone(string name,Transform bone)
    {
        this.name = name;
        this.bone = bone;
        if(this.radius == 0)
        {
            this.radius = 0.01f;
        }
        if(this.damageCoe == 0)
        {
            damageCoe=1;
        }
    }
}

public class HitBoxManage : MonoBehaviour
{
    public Player player;
    public List<HitBoxBone> HitBoxList = new List<HitBoxBone>();
    // Start is called before the first frame update
    void addHitBox(HumanBodyBones bone)
    {
        if(!HitBoxList.Exists(x => x.name == bone.ToString()))
        {
            string name = bone.ToString();
            Transform boneTransform = player.animator.GetBoneTransform(bone);
            HitBoxList.Add(new HitBoxBone(name,boneTransform));
        }
    }
    void setHitBoxColider(HumanBodyBones start,HumanBodyBones end)
    {
        Transform startTransform = player.animator.GetBoneTransform(start);
        Transform endTransform = player.animator.GetBoneTransform(end);
        CapsuleCollider collider = startTransform.GetComponent<CapsuleCollider>();
        if(collider == null)
        {
            collider = startTransform.gameObject.AddComponent<CapsuleCollider>();
        }
        collider.direction = 0;
        collider.isTrigger = true;

        HitBoxBone hitbox = HitBoxList.Find(x => x.name == start.ToString());
        DamageReceive damageReceive = startTransform.GetComponent<DamageReceive>();
        if(damageReceive==null)
        {
            damageReceive = startTransform.gameObject.AddComponent<DamageReceive>();
        }
        if(damageReceive.damageCoe != hitbox.damageCoe){
            damageReceive.damageCoe = hitbox.damageCoe;
        }
        collider.height = Vector3.Distance(startTransform.position, endTransform.position);
        collider.radius = hitbox.radius;
        collider.center = new Vector3(-collider.height/2,0,0);
    }
    void setHitBoxColider(HumanBodyBones start)
    {
        Transform startTransform = player.animator.GetBoneTransform(start);
        CapsuleCollider collider = startTransform.GetComponent<CapsuleCollider>();
        if(collider == null)
        {
            collider = startTransform.gameObject.AddComponent<CapsuleCollider>();
        }
        collider.direction = 0;
        collider.isTrigger = true;
        HitBoxBone hitbox = HitBoxList.Find(x => x.name == start.ToString());
        DamageReceive damageReceive = startTransform.GetComponent<DamageReceive>();
        if(damageReceive==null)
        {
            damageReceive = startTransform.gameObject.AddComponent<DamageReceive>();
        }
        if(damageReceive.damageCoe != hitbox.damageCoe){
            damageReceive.damageCoe = hitbox.damageCoe;
        }
        collider.height = hitbox.radius;
        collider.radius = hitbox.radius;
        collider.center = new Vector3(-collider.height/2,0,0);
    }
    //頭 脖子 胸 腹部 左右肩膀 左右手肘  左右腿 左右腳
    public void HitBoxGenerate(){
        player = GetComponent<Player>();
        if(player!=null){

            addHitBox(HumanBodyBones.Head);
            addHitBox(HumanBodyBones.Neck);
            addHitBox(HumanBodyBones.Chest);
            addHitBox(HumanBodyBones.Hips);
            setHitBoxColider(HumanBodyBones.Head);
            setHitBoxColider(HumanBodyBones.Neck,HumanBodyBones.Head);
            setHitBoxColider(HumanBodyBones.Chest,HumanBodyBones.Neck);
            setHitBoxColider(HumanBodyBones.Hips,HumanBodyBones.Chest);

            addHitBox(HumanBodyBones.LeftUpperArm);
            addHitBox(HumanBodyBones.LeftLowerArm);
            //addHitBox(HumanBodyBones.LeftHand);
            setHitBoxColider(HumanBodyBones.LeftUpperArm,HumanBodyBones.LeftLowerArm);
            setHitBoxColider(HumanBodyBones.LeftLowerArm,HumanBodyBones.LeftHand);

            addHitBox(HumanBodyBones.RightUpperArm);
            addHitBox(HumanBodyBones.RightLowerArm);
            //addHitBox(HumanBodyBones.RightHand);
            setHitBoxColider(HumanBodyBones.RightLowerArm,HumanBodyBones.RightHand);
            setHitBoxColider(HumanBodyBones.RightUpperArm,HumanBodyBones.RightLowerArm);

            addHitBox(HumanBodyBones.LeftUpperLeg);
            addHitBox(HumanBodyBones.LeftLowerLeg);
            //addHitBox(HumanBodyBones.LeftFoot);
            setHitBoxColider(HumanBodyBones.LeftUpperLeg,HumanBodyBones.LeftLowerLeg);
            setHitBoxColider(HumanBodyBones.LeftLowerLeg,HumanBodyBones.LeftFoot);

            addHitBox(HumanBodyBones.RightUpperLeg);
            addHitBox(HumanBodyBones.RightLowerLeg);    
            //addHitBox(HumanBodyBones.RightFoot); 
            setHitBoxColider(HumanBodyBones.RightUpperLeg,HumanBodyBones.RightLowerLeg);
            setHitBoxColider(HumanBodyBones.RightLowerLeg,HumanBodyBones.RightFoot);
        }
    }
}
