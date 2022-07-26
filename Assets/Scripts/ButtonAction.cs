using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.XR;
public class ButtonAction : Interaction
{
    public bool isInteract;
    public override void Execute(Pedalinho p){
        if(gameObject.name == "btnRestart"){
            SceneManager.LoadScene("vinicola");
        }
        if(gameObject.name == "btnMenu"){
            SceneManager.LoadScene("Menu");
        }
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
}
