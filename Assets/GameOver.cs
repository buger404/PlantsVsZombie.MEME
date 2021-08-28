using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public AudioClip Scream;
    void OK(){
        Global.PlaySnd(Scream);
        foreach(GameObject go in transform.root){
            if(go.tag == "Zombies")
                Destroy(go);
        }
    }
}
