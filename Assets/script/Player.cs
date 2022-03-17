using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public Transform camera;
    public Rigidbody rigidbody;
    public Animator animator;
    public float moveSpeed;
    public float mouseSpeed;
    public float jumpSpeed;
    public bool jumpInputKey;
    float rotateX = 0;
    float rotateY = 0;
    float minRotateY = -90;
    float maxRotateY = 90;  
    public Vector3 cameraPos;

    public float hitDistance = 0.16f;
    public bool isGrounded;
    public bool isMoving;
    public bool isCrouch;
    public LayerMask groundLayer;
    float inputHorizontal;
    float inputVertical;
    public Gun weapon;
    void move()//移動
    {
        inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");
        // horizontal為Unity的預設Input 對應按下A、D或左右鍵時 會獲得(左)-1~1(右)的值
        // Vertical為Unity的預設Input 對應按下W、S或上下鍵時 會獲得(下)-1~1(上)的值
        float hMove = inputHorizontal  * moveSpeed * Time.deltaTime;
        float vMove = inputVertical * moveSpeed * Time.deltaTime;
        Vector3 target= Vector3.zero;
        
        if(inputHorizontal!=0 || inputVertical!=0)//當讀取到玩家有輸入移動按鍵
        {
            //設定目標移動位置為當前玩家正前方向量*Vertical + 當前玩家右方向量*horizontal
            target = transform.forward * vMove + transform.right * hMove ;
            //設定當前狀態為正在移動 之後動畫狀態機會用到
            isMoving = true;
        }
        else {
            //沒移動的話 就把目標移動位置設為0
            target = Vector3.zero;
            //設定當前狀態為未移動 之後動畫狀態機會用到
            isMoving = false;
        }
        //將目標移動位置y向量設為當前速率的y 以避免在跳躍時移動造成可無視重力在空中平移
        target.y = rigidbody.velocity.y;
        //將rigidbody的速率設成目標移動位置讓玩家移動
        rigidbody.velocity = target;
    }
    void isGroundRayUpdate(){
        var startPos = transform.position;
        startPos.y = transform.position.y + hitDistance;
        //發射一條射線 用以偵測玩家腳下所佔的是地板 以此來判斷是否可以跳躍
        //Physics.Raycast(發射位置(玩家位置+發射距離), 發射方向(下方), 發射長度(距離*2), 射線能射中的圖層);
        bool jumpRay = Physics.Raycast(startPos, -transform.up, hitDistance *2, groundLayer);
        isGrounded = jumpRay;
        // 可在Scene視窗中預覽的射線
        Debug.DrawRay(startPos, -transform.up * hitDistance*2, Color.red);
    }
    void animatorStateUpdate()
    {
        // 隨時更新animator的狀態機
        animator.SetBool("isGround",isGrounded);
        animator.SetFloat("moveH",inputHorizontal);
        animator.SetFloat("moveV",inputVertical);
        animator.SetBool("isMoving",isMoving);
        animator.SetBool("isCrouch",isCrouch);
    }
    void jump()
    {
        if (isGrounded)
        {
            rigidbody.velocity += jumpSpeed * Vector3.up * Time.deltaTime;
        }
        else
            Debug.Log(isGrounded);
    }
    void Crouch(bool set)//蹲下
    {
        if(set && isGrounded)
        {
            if(!isCrouch){          
                isCrouch = true;         
                GetComponent<CapsuleCollider>().height/=2;
                moveSpeed/=2;
                //當蹲下時，將碰撞盒高度/2、移動速度/2
            }
        }
        else 
        {
            if(isCrouch)
            {
                GetComponent<CapsuleCollider>().height*=2;
                moveSpeed*=2;
                isCrouch = false;
                //當從蹲下站起來，將碰撞盒高度*2、移動速度*2
            }
            
        }

    }
    void cameraControl(){
 
        float x = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
		float y = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
        //讀取滑鼠的輸入
        rotateX += x;
        rotateY += y;
        if(rotateY >= maxRotateY)
            rotateY=maxRotateY;
        else if(rotateY <= minRotateY)
            rotateY=minRotateY;
        //限制移動速度的範圍
        transform.rotation = Quaternion.Euler(0,rotateX,0);
        //將玩家自己的y軸旋轉角度根據滑鼠的x軸旋轉
        camera.localRotation = Quaternion.Euler(-rotateY,0,0);
        //將玩家camera的x軸旋轉角度根據滑鼠的-y軸旋轉
    }
 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        //鎖定使用者的滑鼠 並讓其不顯示
    }
    
    void Update(){
        isGroundRayUpdate();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpInputKey=true;
            //由於跳躍多了一個射線偵測地板的動作
            //使得放於FixedUpdate偵測按鍵會卡頓
            //所以需要將其邏輯偵(按鍵偵測(放於Update))跟物理偵(執行實際移動(放於FixedUpdate))拆開來
        }
        

        animatorStateUpdate();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        cameraControl();
        
        if(Input.GetKey(KeyCode.Mouse0)){
            // fire();
            weapon.fire();
        }
        if(jumpInputKey)
        {
            jump();
            jumpInputKey=false;
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }
}
