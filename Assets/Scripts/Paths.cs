using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public PathPoint[] paths;
    void OnDrawGizmos() {
        paths = transform.GetComponentsInChildren<PathPoint>();
        if(paths.Length <= 0){ return; }
        for (int i = 0; i < paths.Length; i++)
        {
            Gizmos.DrawWireSphere(paths[i].transform.position, 0.5f);
            if(i != paths.Length-1){
                Gizmos.DrawLine(paths[i].transform.position, paths[i + 1].transform.position);
            }else{
                Gizmos.DrawLine(paths[i].transform.position, paths[0].transform.position);
            }
        }
    }
}
