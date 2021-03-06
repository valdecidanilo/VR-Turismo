using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.XR;
public class TimerActionAim : MonoBehaviour
{
    float timeToAcceptAction = 1f;
    [SerializeField]float currentTimeAction;
    public GameObject crossHair;
    public GameManager gameManager;
    public LayerMask layerUI;
    public SpriteRenderer currentbg;
    public Sprite[] bg;
    int idBg;
    float currentFill, timeNextBg;
    bool isInteract;
    void Awake(){
        //XRSettings.gameViewRenderMode = GameViewRenderMode.BothEyes;
    }
    void Start(){
        currentTimeAction = timeToAcceptAction;
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
    void FixedUpdate(){
        if(currentbg != null){
            if(timeNextBg > 0){
                timeNextBg -= 1f * Time.deltaTime;
            }else{
                idBg++;
                if(idBg > bg.Length){
                    idBg = 0;
                }
                timeNextBg = 3f;
                currentbg.sprite = bg[idBg];
            }
        }
        if(isInteract){
            RaycastHit hit;
            Debug.DrawRay(gameManager.VRCam.transform.position, gameManager.VRCam.transform.TransformDirection(Vector3.forward), Color.red);
            Debug.DrawRay(gameManager.VRCam.transform.localPosition, gameManager.VRCam.transform.TransformDirection(Vector3.forward), Color.blue);
            if (Physics.Raycast(gameManager.VRCam.transform.position, gameManager.VRCam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerUI))
            {
                Debug.Log(hit.collider.transform.gameObject.name);
                if(gameManager.currentbutton == null){
                    Debug.Log("ACHOU");
                    gameManager.currentbutton = hit.collider.transform.gameObject; 
                }
            }
            if(currentTimeAction > 0){
                crossHair.GetComponent<Image>().enabled = true;
                currentTimeAction -= 1f * Time.deltaTime;
                currentFill = Mathf.Abs(currentTimeAction - timeToAcceptAction) / timeToAcceptAction;
                crossHair.GetComponent<Image>().fillAmount = currentFill;
            }else{
                if(!gameManager.isTest){
                    gameManager.isTest = true;
                    crossHair.GetComponent<Image>().enabled = false;
                    gameManager.ButtonAction();
                }
                /*if(!gameManager.isPlayGame){
                    gameManager.isPlayGame = true;
                    crossHair.GetComponent<Image>().enabled = false;
                }*/
            }
        }else{
            currentTimeAction = timeToAcceptAction;
            crossHair.GetComponent<Image>().fillAmount = 0f;
            if(gameManager.currentbutton != null){
               gameManager.currentbutton = null; 
               gameManager.isTest = false;
            }
        }
        //currentbutton = rayhit.collider.transform.gameObject;
        
    }
}
