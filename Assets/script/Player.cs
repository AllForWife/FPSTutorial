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
    void move()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");
        float hMove = inputHorizontal  * moveSpeed * Time.deltaTime;
        float vMove = inputVertical * moveSpeed * Time.deltaTime;
        Vector3 target= Vector3.zero;
        if(inputHorizontal!=0 || inputVertical!=0)
        {
            target = transform.forward * vMove + transform.right * hMove ;
            isMoving = true;
        }
        else {
            target = Vector3.zero;
            isMoving = false;
        }

        target.y = rigidbody.velocity.y;
        //print(target);
        rigidbody.velocity = target;
        //Vector3 moveInput = new Vector3( h, 0 ,v );
        //rigidbody.velocity = moveInput;
    }
    void isGroundRayUpdate(){
        var startPos = transform.position;
        startPos.y = transform.position.y + hitDistance;
        var jumpRay = Physics.Raycast(startPos, -transform.up, hitDistance *2, groundLayer);
        isGrounded = jumpRay;
        Debug.DrawRay(startPos, -transform.up * hitDistance*2, Color.red);
    }
    void animatorStateUpdate()
    {
        animator.SetBool("isGround",isGrounded);
        animator.SetFloat("moveH",inputHorizontal);
        animator.SetFloat("moveV",inputVertical);
        animator.SetBool("isMoving",isMoving);
        animator.SetBool("isCrouch",isCrouch);
    }
    void jump()
    {
            //ray = RaycastHit//Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (isGrounded)
        {
           // rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.velocity += jumpSpeed * Vector3.up * Time.deltaTime;
        }
        else
            Debug.Log(isGrounded);
        //Debug.DrawRay(startPos, -transform.up,Color.red ,hitDistance);
    }
    void Crouch(bool set)
    {
        if(set && isGrounded)
        {
            if(!isCrouch){          
                isCrouch = true;         
                GetComponent<CapsuleCollider>().height/=2;
                moveSpeed/=2;
            }
        }
        else 
        {
            if(isCrouch)
            {
                GetComponent<CapsuleCollider>().height*=2;
                moveSpeed*=2;
                isCrouch = false;
            }
            
        }

    }
    void cameraControl(){
 
        float x = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
		float y = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        rotateX += x;
        rotateY += y;
        if(rotateY >= maxRotateY)
            rotateY=maxRotateY;
        else if(rotateY <= minRotateY)
            rotateY=minRotateY;
        transform.rotation = Quaternion.Euler(0,rotateX,0);
        camera.localRotation = Quaternion.Euler(-rotateY,0,0);
    }
 
    // Start is called before the first frame update
    public Transform playerCamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }
    
    void Update(){
        isGroundRayUpdate();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpInputKey=true;
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
