using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieZone : MonoBehaviour
{
    public AudioSource sndPlayer;
    public AudioClip FirstWave;
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
                GameObject Zombie = Instantiate(zombie,new Vector3(8.3f,Global.SeedPoints[Random.Range(0,Global.SeedPoints.Count)].y + 0.1f,1),Quaternion.identity);
                Zombie.SetActive(true);
            }
            if(waveId == 0){
                sndPlayer.clip = FirstWave;
                sndPlayer.Play();
            }
            waveId++;
        }
    }
}
