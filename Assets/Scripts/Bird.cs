using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Range(0f, 500f)]public float velocity;
    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
}
