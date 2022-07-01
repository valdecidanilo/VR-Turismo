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
    float currentFill;
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
        if(isInteract){
            RaycastHit hit;
            Debug.DrawRay(gameManager.VRCam.transform.localPosition, transform.TransformDirection(Vector3.forward), Color.red);
            if (Physics.Raycast(gameManager.VRCam.transform.localPosition, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerUI))
            {
                if(gameManager.currentbutton == null){
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
