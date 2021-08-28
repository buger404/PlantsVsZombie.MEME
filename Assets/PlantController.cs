using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public float HP = 10;
    public float BuffCD;
    public string plantName;
    public int plantID;
    private float CDGained = 0;
    private int buffCount = 0;
    private float originBuffCD;
    private float buffK = 0;

    public void BuffProcess(){
        if(plantName == "404"){
            if(CanAttack(12f,0)){
                if(buffCount == 0){
                    if(Random.Range(0,5) == 3){
                        originBuffCD = BuffCD; BuffCD = 0.001f;
                        buffCount = 150;
                    }else{
                        Fire("Fire");
                    }
                }else{
                    buffK += 0.15f;
                    if(buffK > 3.14) buffK -= 6.28f;
                    FireController fire = Fire("CrazyFire");
                    fire.xSpeed = Mathf.Cos(buffK) * 0.2f;
                    fire.ySpeed = Mathf.Sin(buffK) * 0.2f;
                    buffCount--;
                    if(buffCount == 0) BuffCD = originBuffCD;
                }
            }
        }
        if(plantName == "icelolly"){
            GameObject sun = Resources.Load<GameObject>("Prefabs\\Sun");
            sun = Instantiate(sun,transform.localPosition,Quaternion.identity);
            sun.SetActive(true);
        }
        if(plantName == "vantong"){
            if(CanAttack(12f,0)){
                if(Random.Range(0,15) == 6){
                    Fire("StarBomb");
                }else{
                    Fire("StarFire");
                }
            }
        }
        if(plantName == "nanshenger"){
            if(CanAttack(2f,0)){
                if(buffCount == 0){
                    originBuffCD = BuffCD; BuffCD = 0.05f;
                    buffCount = 9;
                }else{
                    GameObject fire = Fire("Diamond").gameObject;
                    fire.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1);
                    buffCount--;
                    if(buffCount == 0) BuffCD = originBuffCD;
                }
            }
        }
    }
    public void Die(){
        if(plantName == "nanshenger"){
            for(float i = -3.14f;i < 3.14;i += 0.1f){
                GameObject fire = Fire("Diamond").gameObject;
                fire.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f),1);
                fire.GetComponent<FireController>().xSpeed = Mathf.Cos(i) * 0.1f;
                fire.GetComponent<FireController>().ySpeed = Mathf.Sin(i) * 0.1f;
                buffCount--;
                if(buffCount == 0) BuffCD = originBuffCD;
            }
        }
    }
    private bool CanAttack(float xDir,float yDir){
        foreach(RaycastHit2D hit in Physics2D.RaycastAll(transform.localPosition,new Vector2(xDir,yDir))){
            if(hit.collider.gameObject.tag == "Zombies") {
                if(hit.collider.gameObject.GetComponent<ZombieController>().hp > 0)
                    return true;
            }
        }
        return false;
    }
    public FireController Fire(string firename){
        GameObject fire = Resources.Load<GameObject>("Prefabs\\" + firename);
        fire = Instantiate(fire,transform.localPosition + new Vector3(0.6f,0,0),Quaternion.identity);
        fire.SetActive(true);
        return fire.GetComponent<FireController>();
    }
    private void Update() {
        CDGained += Time.deltaTime;
        if(CDGained >= BuffCD){
            CDGained = 0;
            BuffProcess();
        }
    }
}
