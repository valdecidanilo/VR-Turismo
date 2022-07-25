using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pedalinho : MonoBehaviour
{
    [Range(1f, 50f)] public float velocity;
    public Rigidbody rb;
    public GameObject ballon;
    public GameObject overPointsText;
    public Paths path;
    public GameObject crossHair;
    public LayerMask layerUI;
    public Text txtPoints, txtTimer;
    float currentFill;
    public int points;
    [SerializeField]float currentTimeAction;
    float timeToAcceptAction = 1f;
    float timeGeral;
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
        }
    }
    void LateUpdate() {
        txtPoints.text = points.ToString();
        txtTimer.text = timeGeral.ToString("0:00");
    }
    void Aim(){

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Color.red);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerUI))
        {
            if(hit.collider.TryGetComponent<BallonAction>(out BallonAction ballon)){
                if(currentTimeAction > 0){
                    crossHair.GetComponent<Image>().enabled = true;
                    currentTimeAction -= 1f * Time.deltaTime;
                    currentFill = Mathf.Abs(currentTimeAction - timeToAcceptAction) / timeToAcceptAction;
                    crossHair.GetComponent<Image>().fillAmount = currentFill;
                }else{
                    crossHair.GetComponent<Image>().enabled = false;
                    points += 50;
                    GameObject txt = Instantiate(overPointsText);
                    Vector3 p = ballon.transform.position;
                    txt.GetComponent<TextMesh>().text = "+" + 50;
                    txt.transform.position = new Vector3(p.x + 0.5f, p.y + 0.5f, p.z);
                    txt.transform.LookAt(Camera.main.transform.position, Vector3.up);
                    Destroy(ballon.gameObject);
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
                    b.GetComponent<BallonAction>().crossHair = GameObject.Find("crosshairFill");
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
