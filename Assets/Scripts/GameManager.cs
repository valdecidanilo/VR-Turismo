using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SpatialTracking;
public enum TypePowerUp{
    Speed,
    Defense
}
public class GameManager : MonoBehaviour
{
    public float time;
    public float distance, distanceTempSpeed;
    public Text txtDistance, txtDebug;
    public GameObject[] blocks;
    public int countBlocks;
    public TrackedPoseDriver trackedHead;
    public GameObject crossHair;
    public GameObject[] uiLife;
    public GameObject gameOverUI;
    public Bird bird;
    public LayerMask layerUI;
    public GameObject currentbutton;
    public List<GameObject> currentBlocks;
    public static Action<GameObject> createBlock;
    bool addSpeed;
    float currentFill;
    float timeToAcceptAction = 3f;
    float currentTimeAction;
    bool isTest;
    void Start (){
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        createBlock += CreateBlock;
    }
    void Destroy (){
        createBlock -= CreateBlock;
    }
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Debug.DrawRay(screenCenterPoint, Camera.main.transform.position, Color.red);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray,out RaycastHit rayhit, 999f, layerUI)){
            currentbutton = rayhit.collider.transform.gameObject;
            if(currentTimeAction > 0){
                currentTimeAction -= 1f * Time.deltaTime;
                currentFill = Mathf.Abs(currentTimeAction - 3f) / 3f;
                crossHair.GetComponent<Image>().fillAmount = currentFill;
            }else{
                if(!isTest){
                    isTest = true;
                    SceneManager.LoadScene("Minigame0");
                }
            }
        }else{
            currentTimeAction = timeToAcceptAction;
            currentbutton = null;
            crossHair.GetComponent<Image>().fillAmount = 0f;
        }
        if(bird.velocity == 0) { return; }
        distance += 0.1f * Time.deltaTime;
        distanceTempSpeed += 0.1f * Time.deltaTime;
        if(distanceTempSpeed % 4 >= 3.5f){
            distanceTempSpeed = 0;
            bird.AddVelocity();
        }
        txtDistance.text = distance.ToString("0.00");

        
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
        trackedHead.enabled = true;
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