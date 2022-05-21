using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GyroManager : MonoBehaviour
{
    public Text debug;
    public Transform bird;
    private static GyroManager instance;
    public static GyroManager Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<GyroManager>();
                if(instance == null){
                    instance = new GameObject("Spawned GyroManager", typeof(GyroManager)).GetComponent<GyroManager>();
                }
            }
            return instance;
        }
        set{
            instance = value;
        }
    }
    private Gyroscope gyro;
    private Quaternion rotation;
    private bool gyroActive;
    public void EnabledGyro()
    {
        if(gyroActive){ return; }
        if(SystemInfo.supportsGyroscope){
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroActive = gyro.enabled;
        }
        
    }
    void Start(){
        EnabledGyro();
    }

    void Update()
    {
        // ROTATION RATE Z 
        if(gyroActive){
            rotation = gyro.attitude;
            debug.text = "Rotation: " + rotation + "\n" + "Rotation Rate: " + gyro.rotationRate + "\n" + "Aceleration: " + gyro.userAcceleration;
            bird.position = new Vector3(Mathf.Clamp(bird.position.x + (-gyro.rotationRate.z / 15f),-3f, 3f), bird.position.y, bird.position.z);
        }else{
            var vel = Input.GetAxis("Horizontal") / 15f;
            debug.text = "Rotation: " + vel;
            bird.GetComponentInChildren<Animator>().SetFloat("SpeedX", vel);
            bird.position = new Vector3(Mathf.Clamp(bird.position.x + (vel),-3f, 3f), bird.position.y, bird.position.z);
        }
    }
    public Quaternion GetGyroRotation(){
        return rotation;
    }
}
