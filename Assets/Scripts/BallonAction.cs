using UnityEngine;
public class BallonAction : Interaction
{
    public bool isInteract;
    public int points;
    public float op;
    Material ballon;
    void Start(){
        ballon = GetComponent<Renderer>().material;
        ballon.color = new Color(ballon.color.r, ballon.color.g, ballon.color.b, 0f);
    }
    void Update(){
        if(op < 1f){
            op += 0.6f * Time.deltaTime;
        }
        ballon.color = new Color(ballon.color.r, ballon.color.g, ballon.color.b, op);
    }
    public override void Execute(Aim p){
        Pedalinho.onDestroyBallon?.Invoke(gameObject);
    }
    public void SetInteracton(bool active){
        isInteract = active;
    }
}
