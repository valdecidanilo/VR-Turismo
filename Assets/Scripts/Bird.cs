using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarte;
public class Bird : MonoBehaviour
{
    [Range(0.5f, 10f)]public float velocity;
    public Rigidbody rb;
	Animator anim;
	private bool gyroActive, defense;
	public int maxWidth, life;
	public float timerDefense;
	private Gyroscope gyro;
	public GameManager gameManager;
	public GameObject ptcDefense;
	public ParticleSystem ptcFeather;
	void Start () {
		Debug.Log(GetComponent<Rigidbody>().gameObject.name);
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
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
	void FixedUpdate () {
		Move();
		if(defense){
			if(timerDefense > 0){
				timerDefense -= 1f * Time.deltaTime;
			}else{
				defense = false;
				timerDefense = 0;
				ptcDefense.SetActive(false);
			}
		}
	}
	protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
    }
	void Move(){
		if(velocity == 0){ return; }
		//camera.rotation = GyroToUnity(Input.gyro.attitude);
		transform.Translate(Vector3.forward * velocity * Time.deltaTime);
		if(gyroActive){
			//gameManager.txtDebug.text = "Rotation: " + gyro.attitude + "\n" + "Rotation Rate: " + gyro.rotationRate + "\n" + "Aceleration: " + gyro.userAcceleration;
            GetComponentInChildren<Animator>().SetFloat("SpeedX", -gyro.rotationRate.z / 15f);
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + (-gyro.rotationRate.z / 15f),-maxWidth, maxWidth), transform.localPosition.y, transform.localPosition.z);
        }else{
            var vel = Input.GetAxis("Horizontal") / 15f;
            transform.GetComponentInChildren<Animator>().SetFloat("SpeedX", vel);
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + (vel),-maxWidth, maxWidth), transform.localPosition.y, transform.localPosition.z);
        }
	}
	public void AddVelocity(){
		velocity += 0.1f;
	}
	private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Point"){
			TypePowerUp currentType = other.GetComponent<PowerUp>().type;
			if(currentType == TypePowerUp.Speed){
				AddVelocity();
			}else if(currentType == TypePowerUp.Defense){
				defense = true;
				timerDefense = 8f;
				ptcDefense.SetActive(true);
			}
			Destroy(other.gameObject);
		}
		if(other.tag == "Block" && !defense){
			ptcFeather.Play();
			game.android.Vibrate(70);
			if(life > 1){
				life--;
				gameManager.UpdateLife(life);
			}else{
				anim.SetTrigger("Fall");
				anim.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, -0.02f, 0), ForceMode.Impulse);
				velocity = 0;
				gameManager.CallGameOver();
				//SceneManager.LoadScene("Minigame0");
			}
		}
		if(other.tag == "Create"){
			gameManager.CreateBlock(other.transform.parent.gameObject);
			//GameManager.createBlock?.Invoke(other.gameObject);
		}
	}
}