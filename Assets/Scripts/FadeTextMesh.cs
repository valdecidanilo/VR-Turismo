using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FadeTextMesh : MonoBehaviour
{
    public TextMeshPro txt;
    float op;
    void Start(){
        txt = GetComponentInChildren<TextMeshPro>();
        op = 1f;
    }
    void Update()
    {
        transform.Translate(Vector3.up * 0.7f * Time.deltaTime);
        if(txt.color.a > 0){
            op -= 0.7f * Time.deltaTime;
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, op);
        }else{
            Destroy(gameObject);
        }
    }
}
