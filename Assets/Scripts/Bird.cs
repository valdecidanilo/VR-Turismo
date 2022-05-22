using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bird : MonoBehaviour
{
    [Range(0f, 500f)]public float velocity;
    Rigidbody rb;
	private bool gyroActive, defense;
	public int maxWidth;
	public float timerDefense;
	private Gyroscope gyro;
	public GameManager gameManager;
	public GameObject ptcDefense;
	void Start () {
		rb = GetComponent<Rigidbody>();
		EnabledGyro();
	}
	public void EnabledGyro()
    {
        if(gyroActive){ return; }
        if(SystemInfo.supportsGyroscope){
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroActive = gyro.enabled;
        }
        
    }
	void Update () {
		Move();
		if(defense){
			if(timerDefense > 0){
				timerDefense -= 1f * Time.deltaTime;
			}else{
				defense = false;
				timerDefense = 0;
				GetComponentInChildren<SphereCollider>().enabled = true;
				ptcDefense.SetActive(false);
			}
		}
	}
	void Move(){
		transform.Translate(Vector3.forward * velocity * Time.deltaTime);
		if(gyroActive){
            GetComponentInChildren<Animator>().SetFloat("SpeedX", -gyro.rotationRate.z / 15f);
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + (-gyro.rotationRate.z / 15f),-maxWidth, maxWidth), transform.localPosition.y, transform.localPosition.z);
        }else{
            var vel = Input.GetAxis("Horizontal") / 15f;
            transform.GetComponentInChildren<Animator>().SetFloat("SpeedX", vel);
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + (vel),-maxWidth, maxWidth), transform.localPosition.y, transform.localPosition.z);
        }
	}
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Point"){
			TypePowerUp currentType = other.GetComponent<PowerUp>().type;
			if(currentType == TypePowerUp.Speed){
				velocity += 0.1f;
			}else if(currentType == TypePowerUp.Defense){
				defense = true;
				timerDefense = 8f;
				GetComponentInChildren<SphereCollider>().enabled = false;
				ptcDefense.SetActive(true);
			}
			Destroy(other.gameObject);
		}
		if(other.tag == "Block"){
			SceneManager.LoadScene("Minigame0");
		}
		if(other.tag == "Create"){
			gameManager.CreateBlock(other.transform.parent.gameObject);
			//GameManager.createBlock?.Invoke(other.gameObject);
		}
	}
}