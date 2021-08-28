using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float HP;
    public AudioSource sndPlayer;
    public Animator animator;
    public List<AudioClip> groans;
    public List<AudioClip> eat;
    public List<AudioClip> hurt;
    public AudioClip eatup;
    public float hp;
    public float Speed = 0.003f;
    private bool attack = false;
    private PlantController attackPlant;

    public void Die(){
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag == "Plants" && hp > 0){
            attackPlant = other.gameObject.GetComponent<PlantController>();
            animator.Play("AttackAni");
            attack = true;
        }
        if(other.transform.tag == "Fires" && hp > 0){
            FireController fire = other.gameObject.GetComponent<FireController>();
            if(!fire.Hurted.Contains(this.gameObject)){
                if(fire.sndHit != null) Global.PlaySnd(fire.sndHit);
                Global.PlaySnd(hurt[Random.Range(0,hurt.Count)]);
                hp -= fire.Hurt;
                if(hp <= 0){
                    Global.ZombieCount--;
                    if(fire.Hurt >= 10){
                        animator.Play("BoomDie");
                    }else{
                        animator.Play("DieAni");
                    }
                    attack = false;
                }else{
                    animator.Play("ZombieHurt", 1, 0.0f);
                }
                fire.Crash(this.gameObject);
                if(!fire.IsTrigger) Destroy(other.gameObject);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.transform.tag == "Plants" && hp > 0){
            animator.Play("NormalAni");
            attack = false;
        }
    }
    private void Awake() {
        hp = HP;
    }
    void Update()
    {
        if(hp <= 0) return;
        if(attack){
            if(!sndPlayer.isPlaying){
                attackPlant.HP -= 1;
                if(attackPlant.HP <= 0){
                    sndPlayer.clip = eatup;
                    sndPlayer.Play();
                    attackPlant.Die();
                    Global.SeedPlanted[attackPlant.plantID] = false;
                    Destroy(attackPlant.gameObject);
                }else{
                    sndPlayer.clip = eat[Random.Range(0,eat.Count)];
                    sndPlayer.Play();
                }
            }
            return;
        }
        transform.localPosition = new Vector3(transform.localPosition.x - Speed,transform.localPosition.y,1);
        if(transform.localPosition.x < -6.2f){
            Global.Zone.bgm.gameObject.SetActive(false);
            Global.GameOver.SetActive(true);
        }
        if(Random.Range(0,1000) == 66){
            Global.PlaySnd(groans[Random.Range(0,groans.Count)]);
        }
    }
}
