using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Pedalinho : MonoBehaviour
{
    [Range(1f, 50f)] public float velocity;
    public Rigidbody rb;
    public GameObject ballon;
    public GameObject overPointsText;
    public Paths path;
    public GameObject crossHair;
    public GameObject canvasGameOver;
    public LayerMask layerUI;
    public Text txtPoints, txtTimer, txtFinal;
    float currentFill;
    public int points;
    [SerializeField]float currentTimeAction;
    float timeToAcceptAction = 1f;
    public float timeGeral;
    bool setGameOver;
    public int currentPoint;
    void Start(){
        currentTimeAction = timeToAcceptAction;
        timeGeral = 130f;
    }
    void FixedUpdate()
    {
        Move();
        Aim();
        if(timeGeral > 0){
            timeGeral -= 1f * Time.deltaTime;
        }else{
            if(!setGameOver){
               setGameOver = true;
               CallGameOver(); 
            }
        }
    }
    void LateUpdate() {
        txtPoints.text = points.ToString();
        txtTimer.text = timeGeral.ToString("0:00");
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
                    interact.Execute(this);
                    crossHair.GetComponent<Image>().enabled = false;
                }
            }
        }else{
            currentTimeAction = timeToAcceptAction;
            crossHair.GetComponent<Image>().fillAmount = 0f;
        }
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
                    b.name = "Ballon " + i;
                    b.transform.localPosition = Vector3.zero;
                    Vector3 posFinal = Vector3.forward * 10f + Vector3.up * 3f;
                    float size = Random.Range(1f, 1.4f);
                    b.transform.localScale = new Vector3(size, size, size);
                    b.transform.localPosition = new Vector3(Random.Range(posFinal.x - offsetX, posFinal.x + offsetX),
                                                        Random.Range(2f, posFinal.y + offsetY),
                                                        Random.Range(posFinal.z - offsetZ, posFinal.z + offsetZ));
                    Destroy(b, 7f);
                }
            }
        }
        Quaternion lookOnLook = Quaternion.LookRotation(path.paths[currentPoint].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, 0.05f);
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
}
