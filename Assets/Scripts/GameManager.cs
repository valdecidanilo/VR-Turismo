using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public float time;
    public float distance;
    public Text txtDistance;
    void Update()
    {
        distance += 0.1f * Time.deltaTime;
        txtDistance.text = distance.ToString("0.00");
    }
}
