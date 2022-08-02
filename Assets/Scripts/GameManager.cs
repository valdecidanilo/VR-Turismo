using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.SpatialTracking;
public enum TypePowerUp{
    Speed,
    Defense
}
public class GameManager : MonoBehaviour
{
    public float time, timeReset;
    public float distance, distanceTempSpeed;
    public Text txtDistance, txtDebug, txtResetTime;
    public GameObject[] blocks;
    public GameObject VRSystem;
    public GameObject VRCam;
    public int countBlocks;
    //public TrackedPoseDriver trackedHead;
    public GameObject crossHair;
    public GameObject[] uiLife;
    public GameObject gameOverUI;
    public Bird bird;
    public GameObject currentbutton;
    public List<GameObject> currentBlocks;
    public static Action<GameObject> createBlock;
    bool addSpeed;
    float currentFill;
    float timeToAcceptAction = 3f;
    float currentTimeAction;
    public bool isTest, isPlayGame, onTimeReset;
    void Start (){
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        createBlock += CreateBlock;
    }
    void Destroy (){
        createBlock -= CreateBlock;
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu"){
            return;
        }
        /*if(onTimeReset){
            if(timeReset > 0f){
                timeReset -= 1f * Time.deltaTime;
                txtResetTime.text = "Reiniciando em " + timeReset.ToString("00") + " segundos";
            }else{
                txtResetTime.text = "Reiniciando em 0 segundos";
                onTimeReset = false;
                GoMenu();
            }   
        }*/
        if(bird.velocity == 0) { return; }
        distance += 0.1f * Time.deltaTime;
        distanceTempSpeed += 0.1f * Time.deltaTime;
        if(distanceTempSpeed % 4 >= 3.5f){
            distanceTempSpeed = 0;
            bird.AddVelocity();
        }
        txtDistance.text = distance.ToString("0.00");

    }
    public void SetTimeReset(){
        onTimeReset = true;
        timeReset = 3f;
        
    }
    public void Log(string str){
        Debug.Log(str);
    }
    public void ButtonAction(){
        currentbutton.GetComponent<Button>().onClick?.Invoke();
    }
    public void GoGame(){
        SceneManager.LoadScene("Minigame0");
    }
    public void GoLago(){
        SceneManager.LoadScene("vinicola");
    }
    public void GoMenu(){
        SceneManager.LoadScene("Menu");
    }
    public void CreateBlock(GameObject oldPos){
        CheckBlock();
        var next = new Vector3(0f, 0f, oldPos.transform.position.z + 6.8f);
        GameObject b = Instantiate(blocks[countBlocks], next, Quaternion.identity);
        countBlocks++;
        if(countBlocks > blocks.Length -1){
            countBlocks = 0;
        }
        b.name = "Box " + countBlocks;
        currentBlocks.Insert(0, b);
        
    }
    public void ActiveCrossHair(bool active){
        crossHair.SetActive(active);
    }
    public void Restart(){
        SceneManager.LoadScene("Minigame0");
    }
    public void CallGameOver(){
        //trackedHead.enabled = true;
        ActiveCrossHair(true);
        gameOverUI.SetActive(true);
    }
    public void UpdateLife(int currentlife){
        foreach (var l in uiLife)
        {
            l.SetActive(false);
        }
        for (int i = 0; i < currentlife; i++)
        {
            uiLife[i].SetActive(true);
        }
    }
    public void CheckBlock(){
        if(currentBlocks.Count > 2){
            Destroy(currentBlocks[2]);
            currentBlocks.RemoveAt(2);
        }
    }
}