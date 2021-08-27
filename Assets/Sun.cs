using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    bool locked = false;
    Vector3 sunpos;
    private void Awake() {
        sunpos = GameObject.Find("SunDot").transform.localPosition;
    }
    private void OnMouseEnter() {
        if(locked) return;
        locked = true;
        this.GetComponent<AudioSource>().Play();
    }
    private void Update() {
        if(!locked) return;
        Vector3 pos = transform.localPosition;
        pos += (sunpos - pos) / 10f;
        transform.localPosition = pos;
        Vector3 scale = transform.localScale;
        scale += (Vector3.zero - scale) / 30f;
        transform.localScale = scale;

        if(scale.x <= 0.4f){
            Global.Sun += 25;
            Destroy(this.gameObject);
        }
    }
}
