using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.XR;
public class BallonAction : MonoBehaviour
{
    float timeToAcceptAction = 1f;
    [SerializeField]float currentTimeAction;
    public GameObject crossHair;
    public LayerMask layerUI;
    float currentFill;
    public bool isInteract;
    void Awake(){
        //XRSettings.gameViewRenderMode = GameViewRenderMode.BothEyes;
    }
    void Start(){
        currentTimeAction = timeToAcceptAction;
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
}
