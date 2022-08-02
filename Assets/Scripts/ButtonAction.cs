using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.XR;
public class ButtonAction : Interaction
{
    public bool isInteract;
    public override void Execute(Aim p){
        if(gameObject.name == "btnRestart"){
            GetComponent<Button>().onClick?.Invoke();
        }
        if(gameObject.name == "btnMenu"){
            GetComponent<Button>().onClick?.Invoke();
        }
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
}
