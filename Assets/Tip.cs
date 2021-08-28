using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public AudioClip FinalWaveSnd;
    public void FinalWave(){
        Global.PlaySnd(FinalWaveSnd);
    }
}
