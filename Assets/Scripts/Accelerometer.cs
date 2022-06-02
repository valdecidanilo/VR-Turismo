using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    public bool isFlat = true;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 tilt = Input.acceleration;

        if(isFlat){
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.red);
        rb.AddForce(tilt);
    }
}
