using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Range(0f, 500f)]public float velocity;
    Rigidbody rb;
	float dirX;
	float moveSpeed = 20f;
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	void Update () {
		dirX = Input.acceleration.x * moveSpeed;
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -7.5f, 7.5f), transform.position.y, transform.position.z);
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector3 (dirX, 0f, velocity * Time.deltaTime);
	}
}
