using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float Hurt = 1;
    public float xSpeed = 0.1f,ySpeed = 0f;
    public bool IsTrigger = false;
    public List<GameObject> Hurted = new List<GameObject>();
    public AudioClip sndHit;
    public enum FireRoutine{
        Normal
    }
    public FireRoutine routine;
    private void Awake() {
        Destroy(this.gameObject,10f);
    }
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if(routine == FireRoutine.Normal){
            pos.x += xSpeed;
            pos.y += ySpeed;
        }
        transform.localPosition = pos;
    }
}
