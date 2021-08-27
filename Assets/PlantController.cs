using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public int HP = 10;
    public float BuffCD;
    public string plantName;
    private float CDGained = 0;
    public void BuffProcess(){
        if(plantName == "404"){
            Fire("Fire");
        }
    }
    public void Fire(string firename){
        GameObject fire = Resources.Load<GameObject>("Prefabs\\" + firename);
        fire = Instantiate(fire,transform.localPosition + new Vector3(0.6f,0,0),Quaternion.identity);
        fire.SetActive(true);
    }
    private void Update() {
        CDGained += Time.deltaTime;
        if(CDGained >= BuffCD){
            CDGained = 0;
            BuffProcess();
        }
    }
}
