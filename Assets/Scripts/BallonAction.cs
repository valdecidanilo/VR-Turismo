using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.XR;
public class BallonAction : Interaction
{
    public bool isInteract;

    public override void Execute(Pedalinho p){
        p.points += 50;
        GameObject txt = Instantiate(p.overPointsText);
        Vector3 pos = transform.position;
        txt.GetComponent<TextMesh>().text = "+" + 50;
        txt.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);
        txt.transform.LookAt(Camera.main.transform.position, Vector3.up);
        Destroy(gameObject);
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
}
