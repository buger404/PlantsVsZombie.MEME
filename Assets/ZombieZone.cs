using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZone : MonoBehaviour
{
    public AudioSource sndPlayer,bgm;
    public AudioClip FirstWave,HugeWave,Siren;
    public Animator WaveAnimator;
    public GameObject GameOver;
    public List<AudioClip> groans;
    [System.Serializable]
    public struct ZombieWave{
        public bool IsHugeWave;
        public List<GameObject> Zombies;
        public float spanTime;
        public bool Informed;
    }
    public List<ZombieWave> AttackWaves;

    private int waveId = 0;
    private float CDGained = 0;

    private void Awake() {
        Global.GameOver = GameOver;
        Global.Zone = this;
    }

    void Update()
    {
        if(waveId >= AttackWaves.Count) return;

        CDGained += Time.deltaTime;
        if(AttackWaves[waveId].spanTime - CDGained <= 5f && AttackWaves[waveId].IsHugeWave && !AttackWaves[waveId].Informed){
            if(waveId == AttackWaves.Count - 1){
                WaveAnimator.Play("FinalWave",0,0.0f);
            }else{
                WaveAnimator.Play("Wave",0,0.0f);
            }
            ZombieWave wave = AttackWaves[waveId];
            wave.Informed = true;
            AttackWaves[waveId] = wave;
            Global.PlaySnd(HugeWave);
        }
        if(Global.ZombieCount == 0 && AttackWaves[waveId].spanTime - CDGained > 7f && waveId > 0){
            CDGained = AttackWaves[waveId].spanTime - 6.8f;
        }
        if(CDGained >= AttackWaves[waveId].spanTime){
            CDGained = 0;
            foreach(GameObject zombie in AttackWaves[waveId].Zombies){
                int i = Random.Range(0,Global.SeedPoints.Count);
                GameObject Zombie = Instantiate(zombie,new Vector3(8.3f + Random.Range(0.0f,2.0f),Global.SeedPoints[i].y + 0.1f,1),Quaternion.identity);
                Zombie.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)Mathf.Floor(i / 9);
                Zombie.SetActive(true);
                Global.ZombieCount++;
            }
            if(waveId == 0){
                sndPlayer.clip = FirstWave;
                sndPlayer.Play();
            }else if(AttackWaves[waveId].IsHugeWave){
                sndPlayer.clip = Siren;
                sndPlayer.Play();
                bgm.pitch = 1.25f;
            }else{
                sndPlayer.clip = groans[Random.Range(0,groans.Count)];
                sndPlayer.Play();
                bgm.pitch = 1.0f;
            }
            waveId++;
        }
    }
}
