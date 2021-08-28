using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedPackageController : MonoBehaviour
{
    public AudioSource sndPlayer;
    public AudioClip buzzerSnd, pointSnd;
    public AudioClip[] plantSnd;
    public Image Face;
    public string Plant;
    public long SunNeeded;
    public float CD;
    public Transform CDPanel;
    public GameObject BanPanel;
    public bool Avaliable;
    float CDGain = 0f;

    private void Awake() {
        transform.Find("SunNeeded").GetComponent<Text>().text = SunNeeded.ToString();
        Face.sprite = Resources.Load<Sprite>("Plants\\" + Plant);
    }
    private void Update() {
        if(CDGain < CD){
            CDGain += Time.deltaTime;
            if(CDGain > CD) CDGain = CD;
            CDPanel.localScale = new Vector3(1f,1f - CDGain / CD,1f);
        }
        Avaliable = Global.Sun >= SunNeeded && CDGain >= CD && !Global.Seeding;
        BanPanel.SetActive(!Avaliable);
        if(Input.GetKey(KeyCode.Z)) CDGain = CD;
        if(Global.TargetSeed == this && Global.Seeding){
            if(Input.GetMouseButtonUp(0)){
                Vector3 mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for(int i = 0;i < Global.SeedPoints.Count; i++){
                    Vector3 pos = Global.SeedPoints[i];
                    if(mpos.x >= pos.x - 0.65f && mpos.y >= pos.y - 0.84f && mpos.x <= pos.x + 0.65f && mpos.y <= pos.y + 0.84f){
                        if(Global.SeedPlanted[i]){
                            sndPlayer.clip = buzzerSnd;
                            sndPlayer.Play();
                        }else{
                            Global.SeedPlanted[i] = true;
                            Global.Seeding = false;
                            sndPlayer.clip = plantSnd[Random.Range(0,plantSnd.Length)];
                            sndPlayer.Play();
                            Global.DragSeed.SetActive(false);
                            GameObject Plant = Instantiate(Global.PlantPrefab, pos, Quaternion.identity);
                            Plant.GetComponent<SpriteRenderer>().sprite = Face.sprite;
                            PlantController plant = Plant.GetComponent<PlantController>();
                            plant.plantName = this.Plant;
                            plant.plantID = i;
                            if(this.Plant == "404"){
                                plant.HP = 10; plant.BuffCD = 2.0f; 
                            }
                            if(this.Plant == "vantong"){
                                plant.HP = 10; plant.BuffCD = 1.6f; 
                            }
                            if(this.Plant == "icelolly"){
                                plant.HP = 10; plant.BuffCD = 15.0f; 
                            }
                            if(this.Plant == "nanshenger"){
                                plant.HP = 10; plant.BuffCD = 2.0f; 
                            }
                            Plant.SetActive(true);
                            Global.Sun -= SunNeeded;
                            CDGain = 0;
                        }
                        break;
                    }
                }
            }
        }
    }
    public void OnClick() {
        if(Avaliable){
            Global.Seeding = true;
            Global.TargetSeed = this;
            Global.DragSeed.GetComponent<SpriteRenderer>().sprite = Face.sprite;
            Global.DragSeed.GetComponent<MouseFollow>().Fix();
            Global.DragSeed.SetActive(true);
            sndPlayer.clip = pointSnd;
            sndPlayer.Play();
        }else if(Global.TargetSeed == this && Global.Seeding){
            Global.Seeding = false;
            sndPlayer.clip = pointSnd;
            sndPlayer.Play();
            Global.DragSeed.SetActive(false);
        }else{
            sndPlayer.clip = buzzerSnd;
            sndPlayer.Play();
        }
    }
}
