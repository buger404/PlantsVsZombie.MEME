using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float Hurt = 1;
    public float xSpeed = 0.1f,ySpeed = 0f;
    public bool IsTrigger = false;
    public string FireName;
    public List<GameObject> Hurted = new List<GameObject>();
    public AudioClip sndHit;
    public enum FireRoutine{
        Normal
    }
    public FireRoutine routine;


    private void Awake() {
        Destroy(this.gameObject,10f);
    }
    public void Crash(GameObject other){
        Hurted.Add(other);
        if(FireName == "RealFire"){
            this.GetComponent<SpriteRenderer>().color /= 2;
            Hurt /= 2;
        }
        if(FireName == "IntallkFire"){
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            other.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if(routine == FireRoutine.Normal){
            pos.x += xSpeed;
            pos.y += ySpeed;
        }
        transform.localPosition = pos;
        if(FireName == "IntallkFire"){
            if(pos.x > 9f){
                foreach(GameObject go in Hurted){
                    go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
