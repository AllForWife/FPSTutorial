using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Mirror;
public class Gun : NetworkBehaviour
{
    Ray ray; //射線
    float raylength = 100f; //射線最大長度
    RaycastHit hit; //被射線打到的物件
    public float shootSpeed=1;
    public GameObject bulletholePrefab;
    public GameObject fireFlash;
    public LayerMask hitTarget;
    public Vector3 shakePos;
    // Vector3 shakeStartPos;

    public bool fireBool;
    public bool shakeBool;
    
    // public LayerMask hit;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(fireTime());
       StartCoroutine(shake());
    }
    public IEnumerator fireTime()
    {
        while(true){
            if(fireBool)
            {
                fireFlash.SetActive(true);
                yield return new WaitForSeconds(shootSpeed);
                fireFlash.SetActive(false);
                fireBool=false;
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
            
        }

    }

    public IEnumerator shake()
    {
        while(true){

            if(shakeBool)
            { //tweener.PlayBackwards()
                
                Vector3 startPos = transform.localPosition;
                Vector3 endPos = new Vector3(startPos.x + shakePos.x, startPos.y + shakePos.y ,startPos.z + shakePos.z) ;
                transform.DOLocalMove(endPos,shootSpeed);
                yield return new WaitForSeconds(shootSpeed);
                transform.DOLocalMove(startPos,shootSpeed);
                // tweener.Play();
                //transform.DOShakePosition(0.1f,strength:0.1f);
                yield return new WaitForSeconds(shootSpeed);
                shakeBool=false;
            }
            yield return new WaitForFixedUpdate();
        }
        
    }
    public void fire(){
        if(!fireBool){
            Renderer renderer;  
            MeshCollider meshCollider;  
            // RaycastHit hit;  
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            //由攝影機射到是畫面正中央的射線
            // (射線,out 被射線打到的物件,射線長度)，out hit 意思是：把"被射線打到的物件"帶給hit
            if (Physics.Raycast(ray, out hit, raylength,layerMask:hitTarget))
            {
                if(hit.transform.CompareTag("Enemy")){
                    hit.transform.gameObject.SetActive(false);
                }
                // GameObject bulletHole = Instantiate(bulletholePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity);
                GameObject bulletHole = Instantiate(bulletholePrefab, hit.point , Quaternion.identity);
                bulletHole.transform.LookAt(hit.point + hit.normal);
                Destroy(bulletHole, 3f);
                //transform. (shakeEndPos);
                
                //當射線打到物件時會在Scene視窗畫出黃線，方便查閱
                Debug.DrawLine(ray.origin, hit.point, Color.yellow);
                print(hit.transform.name);
                //在Console視窗印出被射線打到的物件名稱，方便查閱                       
            }
            fireBool=true;
            if(!shakeBool)
                shakeBool=true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
