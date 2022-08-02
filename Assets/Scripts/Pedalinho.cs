using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class Pedalinho : MonoBehaviour
{
    [Range(0f, 50f)] public float velocity;
    public Rigidbody rb;
    public GameObject ballon;
    public Animator _anim;
    public GameObject overPointsText;
    public GameObject ptc, bulletShot;
    public Paths path;
    public GameObject crossHair;
    public GameObject canvasGameOver;
    public LayerMask layerUI;
    public Text txtPoints, txtTimer, txtFinal, txtBallons;
    float currentFill;
    public int points, ballons;
    [SerializeField]float currentTimeAction;
    float timeToAcceptAction = 1f;
    public float timeGeral;
    bool setGameOver;
    public int currentPoint;
    public static Action<GameObject> onDestroyBallon;
    void Start(){
        currentTimeAction = timeToAcceptAction;
        timeGeral = 130f;
        onDestroyBallon += DestroyBallon;
    }
    private void OnDisable() {
        onDestroyBallon -= DestroyBallon;
    }
    void FixedUpdate()
    {
        Move();
        //Aim();
        /*if(timeGeral > 0){
            timeGeral -= 1f * Time.deltaTime;
        }else{
            if(!setGameOver){
               setGameOver = true;
               CallGameOver(); 
            }
        }*/
        if(ballons >= 10 && !setGameOver){
            setGameOver = true;
            CallGameOver(); 
        }
    }
    void LateUpdate() {
        txtPoints.text = points.ToString();
        txtBallons.text = ballons.ToString() + "/10";
        //txtTimer.text = timeGeral.ToString("0:00");
    }
    void CallGameOver(){
        velocity = 0;
        SavePoints();
        canvasGameOver.SetActive(true);
        txtFinal.text = $"Melhor pontuação: {PlayerPrefs.GetInt("highscore")} \n pontuação atual: {points}";
    }
    public void RestartGame(){
        SceneManager.LoadScene("vinicola");
    }
    public void GotoMenu(){
        SceneManager.LoadScene("Menu");
    }
    void SavePoints(){
        if(points > PlayerPrefs.GetInt("highscore")){
            PlayerPrefs.SetInt("highscore", points);
        }
    }
    void Aim(){

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Color.red);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerUI))
        {
            if(hit.collider.TryGetComponent<Interaction>(out Interaction interact)){
                if(currentTimeAction > 0){
                    crossHair.GetComponent<Image>().enabled = true;
                    currentTimeAction -= 1f * Time.deltaTime;
                    currentFill = Mathf.Abs(currentTimeAction - timeToAcceptAction) / timeToAcceptAction;
                    crossHair.GetComponent<Image>().fillAmount = currentFill;
                }else{
                    interact.Execute(null);
                    crossHair.GetComponent<Image>().enabled = false;
                    _anim.SetTrigger("Shot");
                    ballons++;
                    StartCoroutine("waitShotGun");
                }
            }
        }else{
            currentTimeAction = timeToAcceptAction;
            crossHair.GetComponent<Image>().fillAmount = 0f;
        }
    }
    public void DestroyBallon(GameObject pos){
        _anim.SetTrigger("Shot");
        StartCoroutine("waitShotGun");
        ballons++;
        points += 50;
        GameObject p = Instantiate(ptc);
        p.transform.position = pos.transform.position;
        GameObject txt = Instantiate(overPointsText);
        txt.GetComponentInChildren<TextMeshPro>().text = "+" + points;
        txt.transform.position = new Vector3(pos.transform.position.x + 0.5f, pos.transform.position.y + 0.5f, pos.transform.position.z);
        txt.transform.LookAt(Camera.main.transform.position, Vector3.up);
        Destroy(pos);
    }
    void Move(){
        if(Vector3.Distance(transform.position, path.paths[currentPoint].transform.position) < 1f){
            currentPoint++;
            if(currentPoint >= path.paths.Length){
                Debug.Log("TERMINOU");
                currentPoint = 0;
            }else{
                float offsetX = 5f;
                float offsetY = 1f;
                float offsetZ = 2f;
                Debug.Log("PROXIMO PATH");
                for (int i = 0; i < 3; i++)
                {
                    GameObject b = Instantiate(ballon, path.paths[currentPoint - 1].transform);
                    BallonAction ba = b.GetComponent<BallonAction>();
                    if(i == 0){
                        b.name = "Ballon Vermelho";
                        b.GetComponent<Renderer>().material.color = Color.red;
                        ba.points = 50;
                    }
                    if(i == 1){
                        b.name = "Ballon Verde";
                        b.GetComponent<Renderer>().material.color = Color.green;
                        ba.points = 30;
                    }
                    if(i == 2){
                        b.name = "Ballon Azul";
                        b.GetComponent<Renderer>().material.color = Color.blue;
                        ba.points = 10;
                    }
                    b.transform.localPosition = Vector3.zero;
                    Vector3 posFinal = Vector3.forward * 10f + Vector3.up * 3f;
                    float size = UnityEngine.Random.Range(1f, 1.4f);
                    b.transform.localScale = new Vector3(size, size, size);
                    b.transform.localPosition = new Vector3(UnityEngine.Random.Range(posFinal.x - offsetX, posFinal.x + offsetX),
                                                        UnityEngine.Random.Range(2f, posFinal.y + offsetY),
                                                        UnityEngine.Random.Range(posFinal.z - offsetZ, posFinal.z + offsetZ));
                    Destroy(b, 7f);
                }
            }
        }
        Quaternion lookOnLook = Quaternion.LookRotation(path.paths[currentPoint].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, 0.05f);
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
    IEnumerator waitShotGun(){
        bulletShot.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        bulletShot.SetActive(false);
    }
}
