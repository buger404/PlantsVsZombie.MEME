using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int HP;
    public AudioSource sndPlayer;
    public Animator animator;
    public List<AudioClip> groans;
    public List<AudioClip> eat;
    public AudioClip eatup;
    private int hp;
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
            sndPlayer.clip = fire.sndHit;
            sndPlayer.Play();
            hp -= fire.Hurt;
            if(hp <= 0){
                animator.Play("DieAni");
                attack = false;
            }
            Destroy(other.gameObject);
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
        if(attack){
            if(!sndPlayer.isPlaying){
                attackPlant.HP -= 1;
                if(attackPlant.HP <= 0){
                    sndPlayer.clip = eatup;
                    sndPlayer.Play();
                    Destroy(attackPlant.gameObject);
                }else{
                    sndPlayer.clip = eat[Random.Range(0,eat.Count)];
                    sndPlayer.Play();
                }
            }
            return;
        }
        // 给我爬
        transform.localPosition = new Vector3(transform.localPosition.x - 0.003f,transform.localPosition.y,1);
        // 给我嘤嘤叫
        if(Random.Range(0,1000) == 66){
            sndPlayer.clip = groans[Random.Range(0,groans.Count)];
            sndPlayer.Play();
        }
    }
}
