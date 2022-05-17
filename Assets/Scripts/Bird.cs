using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Range(0f, 500f)]public float velocity;
	public Quaternion baseRotation = new Quaternion(0, 0, 1, 0);
    Rigidbody rb;
	float dirX;
	float moveSpeed = 20f;
	void Start () {
		rb = GetComponent<Rigidbody>();
		//GyroManager.Instance.EnabledGyro();
	}
	void Update () {
		//dirX = Input.acceleration.x * moveSpeed;
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
		//transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -50f, 50f), transform.position.y, transform.position.z);
		//transform.localRotation = GyroManager.Instance.GetGyroRotation() * baseRotation;
	}

	void FixedUpdate()
	{
		//rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, velocity * Time.deltaTime);
	}
}
