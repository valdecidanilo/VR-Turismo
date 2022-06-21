using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
public class TimerActionAim : MonoBehaviour
{
    float timeToAcceptAction = 1f;
    [SerializeField]float currentTimeAction;
    public GameObject crossHair;
    public GameManager gameManager;
    public GameObject currentbutton;
    public LayerMask layerUI;
    float currentFill;
    bool isInteract;
    void Awake(){
        XRSettings.gameViewRenderMode = GameViewRenderMode.BothEyes;
    }
    void Start(){
        currentTimeAction = timeToAcceptAction;
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
    void FixedUpdate(){
        if(isInteract){
            if(currentTimeAction > 0){
                crossHair.GetComponent<Image>().enabled = true;
                currentTimeAction -= 1f * Time.deltaTime;
                currentFill = Mathf.Abs(currentTimeAction - timeToAcceptAction) / timeToAcceptAction;
                crossHair.GetComponent<Image>().fillAmount = currentFill;
            }else{
                if(!gameManager.isTest){
                    gameManager.isTest = true;
                    crossHair.GetComponent<Image>().enabled = false;
                    gameManager.GoGame();
                }
                if(!gameManager.isPlayGame){
                    gameManager.isPlayGame = true;
                    crossHair.GetComponent<Image>().enabled = false;
                }
            }
        }else{
            currentTimeAction = timeToAcceptAction;
            crossHair.GetComponent<Image>().fillAmount = 0f;
        }
        //currentbutton = rayhit.collider.transform.gameObject;
        
    }
}
