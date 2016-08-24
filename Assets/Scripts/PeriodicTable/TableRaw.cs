using UnityEngine;
using System.Collections;

public class TableRaw : MonoBehaviour {
    public GameObject secondRaw;
    public GameObject thirdRaw;
    public GameObject fourthRaw;


    // Use this for initialization
    void Start () {
        if (PlayerManager.villageProgress > 0)
            secondRaw.SetActive(false);
        if (PlayerManager.villageProgress > 1)
            thirdRaw.SetActive(false);
        if (PlayerManager.villageProgress > 2)
            fourthRaw.SetActive(false);
        //if (InventoryManager.Inst().Raw[1]) secondRaw.SetActive(false);
        //if (InventoryManager.Inst().Raw[2]) thirdRaw.SetActive(false);
        //if (InventoryManager.Inst().Raw[3]) fourthRaw.SetActive(false);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
