using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global{
    public static long Sun = 500;
    public static bool Seeding = false;
    public static SeedPackageController TargetSeed;
    public static GameObject DragSeed;
    public static GameObject PlantPrefab;
    public static List<Vector3> SeedPoints;
    public static List<bool> SeedPlanted;
}

public class SeedBankController : MonoBehaviour
{
    public Text sunDisplay;
    public GameObject DragSeed;

    private void Awake() {
        Global.DragSeed = DragSeed;
        Global.SeedPoints = new List<Vector3>();
        Global.SeedPlanted = new List<bool>();
        foreach(Transform seed in GameObject.Find("SeedPlaces").transform){
            if(seed.tag == "SeedDot"){
                Global.SeedPoints.Add(seed.localPosition);
                Global.SeedPlanted.Add(false);
                Destroy(seed.gameObject);
            }
        }
        Global.PlantPrefab = Resources.Load<GameObject>("Prefabs\\Plant");
    }

    void Update()
    {
        sunDisplay.text = Global.Sun.ToString();
    }
}
