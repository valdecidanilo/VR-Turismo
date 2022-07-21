using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedalinho : MonoBehaviour
{
    [Range(1f, 50f)] public float velocity;
    public Rigidbody rb;
    public Paths path; 
    public int currentPoint;
    void FixedUpdate()
    {
        Move();
    }
    void Move(){
        if(Vector3.Distance(transform.position, path.paths[currentPoint].transform.position) < 1f){
            currentPoint++;
            Debug.Log("PROXIMO PATH");
        }
        Quaternion lookOnLook = Quaternion.LookRotation(path.paths[currentPoint].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, 0.05f);
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
}
