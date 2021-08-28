using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public bool Enabled = true;

    public void Fix(){
        Update();
    }

    void Update()
    {
        if(!Enabled) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 1;
        transform.localPosition = pos;
    }
}
