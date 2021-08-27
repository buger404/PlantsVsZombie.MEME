using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public int Hurt = 1;
    public AudioClip sndHit;
    public enum FireRoutine{
        Normal
    }
    public FireRoutine routine;
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if(routine == FireRoutine.Normal){
            pos.x += 0.1f;
        }
        transform.localPosition = pos;
    }
}
