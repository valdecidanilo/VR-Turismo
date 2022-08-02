using UnityEngine;
using UnityEngine.UI;
public class Aim : MonoBehaviour {
    public GameObject crossHair;
    public LayerMask layerUI;
    float currentFill;
    [SerializeField]float currentTimeAction;
    float timeToAcceptAction = 1f;
    private void Update() {
        AimGet();
    }
    void AimGet(){
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
}