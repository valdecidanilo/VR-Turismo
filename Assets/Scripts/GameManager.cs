using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TypePowerUp{
    Speed,
    Defense
}
public class GameManager : MonoBehaviour
{
    public float time;
    public float distance;
    public Text txtDistance, txtDebug;
    public GameObject[] blocks;
    public int countBlocks;
    public List<GameObject> currentBlocks;
    public static Action<GameObject> createBlock;
    void Start (){
        createBlock += CreateBlock;
    }
    void Destroy (){
        createBlock -= CreateBlock;
    }
    void Update()
    {
        distance += 0.1f * Time.deltaTime;
        txtDistance.text = distance.ToString("0.00");
    }
    public void CreateBlock(GameObject oldPos){
        Debug.Log("Create " + oldPos.transform.position.z);
        CheckBlock();
        var next = new Vector3(0f, 0f, oldPos.transform.position.z + 6.8f);
        GameObject b = Instantiate(blocks[countBlocks], next, Quaternion.identity);
        
        //b.transform.position = next;
        countBlocks++;
        if(countBlocks > blocks.Length -1){
            countBlocks = 0;
        }
        b.name = "Box " + countBlocks;
        currentBlocks.Insert(0, b);
        
    }
    public void CheckBlock(){
        if(currentBlocks.Count > 2){
            Destroy(currentBlocks[2]);
            currentBlocks.RemoveAt(2);
            Debug.Log("Removeu Ultimo");
        }
    }
}
