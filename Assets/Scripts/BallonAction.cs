using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.XR;
public class BallonAction : Interaction
{
    public bool isInteract;
    public int points;
    public float op;
    Material ballon;
    void Start(){
        ballon = GetComponent<Renderer>().material;
        ballon.color = new Color(ballon.color.r, ballon.color.g, ballon.color.b, 0f);
    }
    void Update(){
        if(op < 1f){
            op += 0.6f * Time.deltaTime;
        }
        ballon.color = new Color(ballon.color.r, ballon.color.g, ballon.color.b, op);
    }
    public override void Execute(Pedalinho p){
        p.points += 50;
        GameObject ptc = Instantiate(p.ptc);
        ptc.transform.position = transform.position;
        GameObject txt = Instantiate(p.overPointsText);
        Vector3 pos = transform.position;
        txt.GetComponentInChildren<TextMeshPro>().text = "+" + points;
        txt.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);
        txt.transform.LookAt(Camera.main.transform.position, Vector3.up);
        Destroy(gameObject);
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
}
