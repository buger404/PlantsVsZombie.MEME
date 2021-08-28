using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZone : MonoBehaviour
{
    public AudioSource sndPlayer;
    public AudioClip FirstWave;
    public List<AudioClip> groans;
    [System.Serializable]
    public struct ZombieWave{
        public bool IsHugeWave;
        public List<GameObject> Zombies;
        public float spanTime;
    }
    public List<ZombieWave> AttackWaves;

    private int waveId = 0;
    private float CDGained = 0;

    void Update()
    {
        if(waveId >= AttackWaves.Count) return;

        CDGained += Time.deltaTime;
        if(CDGained >= AttackWaves[waveId].spanTime){
            CDGained = 0;
            foreach(GameObject zombie in AttackWaves[waveId].Zombies){
                int i = Random.Range(0,Global.SeedPoints.Count);
                GameObject Zombie = Instantiate(zombie,new Vector3(8.3f + Random.Range(0.0f,2.0f),Global.SeedPoints[i].y + 0.1f,1),Quaternion.identity);
                Zombie.GetComponent<SpriteRenderer>().sortingOrder = 4 + (int)Mathf.Floor(i / 9);
                Zombie.SetActive(true);
            }
            if(waveId == 0){
                sndPlayer.clip = FirstWave;
                sndPlayer.Play();
            }else{
                sndPlayer.clip = groans[Random.Range(0,groans.Count)];
                sndPlayer.Play();
            }
            waveId++;
        }
    }
}
