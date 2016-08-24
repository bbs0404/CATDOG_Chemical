using UnityEngine;
using System.Collections;

public class ElementTable : MonoBehaviour {
    public GameObject[] ElementsObect = new GameObject[20];

    void Start () {
        if (PlayerManager.villageProgress > 0)
        {
            for (int i=2; i<10; ++i)
            {
                if (!InventoryManager.Inst().Element[i])
                    ElementsObect[i].SetActive(true);
            }
        }
        if (PlayerManager.villageProgress > 1)
        {
            for (int i=10; i<18; ++i)
            {
                if (!InventoryManager.Inst().Element[i])
                    ElementsObect[i].SetActive(true);
            }
        }
        if (PlayerManager.villageProgress> 2)
        {
            for (int i = 18; i < 20; ++i)
            {
                if (!InventoryManager.Inst().Element[i])
                    ElementsObect[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
