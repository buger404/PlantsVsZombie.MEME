using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelController : MonoBehaviour
{
    public AudioClip Snd,Buzzer,RemoveSnd;
    public MouseFollow Icon;
    private Vector3 originPos;

    public void OnClick(){
        if(Global.Seeding){
            if(Global.TargetSeed == null){
                Global.Seeding = false;
                Global.PlaySnd(Snd);
                Icon.Enabled = false;
                Icon.gameObject.transform.localPosition = originPos;
            }else{
                Global.PlaySnd(Buzzer);
            }
        }else{
            Global.PlaySnd(Snd);
            originPos = Icon.transform.localPosition;
            Global.Seeding = true;
            Global.TargetSeed = null;
            Icon.Enabled = true;
        }
    }

    private void Update() {
        if(Input.GetMouseButtonUp(0)){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(pos,Vector2.zero)){
                if(hit.collider.tag == "Plants"){
                    PlantController plant = hit.collider.GetComponent<PlantController>();
                    Global.SeedPlanted[plant.plantID] = false;
                    Global.PlaySnd(RemoveSnd);
                    Destroy(hit.collider.gameObject);
                    Global.Seeding = false;
                    Icon.Enabled = false;
                    Icon.gameObject.transform.localPosition = originPos;
                    break;
                }
            }
        }
    }
}
