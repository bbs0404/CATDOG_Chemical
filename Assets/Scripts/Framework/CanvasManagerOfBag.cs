using UnityEngine;
using System.Collections;

public class CanvasManagerOfBag : MonoBehaviour {

    public GameObject[] Canvas = new GameObject[4];
    
    void Start()
    {
        
        Canvas[0] = GameObject.Find("Whole");
        Canvas[1] = GameObject.Find("Weapon");
        Canvas[2] = GameObject.Find("Consume");
        Canvas[3] = GameObject.Find("ETC");
   
    }

    public void WholeSetActive()
    {
            Canvas[0].SetActive(true);
            Canvas[1].SetActive(false);
            Canvas[2].SetActive(false);
            Canvas[3].SetActive(false);
    }
    
    public void WeaponSetActive()
    {

        Canvas[0].SetActive(false);
        Canvas[1].SetActive(true);
        Canvas[2].SetActive(false);
        Canvas[3].SetActive(false);
    }
    public void ConsumeSetActive()
    {

        Canvas[0].SetActive(false);
        Canvas[1].SetActive(false);
        Canvas[2].SetActive(true);
        Canvas[3].SetActive(false);
    }
    public void ETCSetActive()
    {

        Canvas[0].SetActive(false);
        Canvas[1].SetActive(false);
        Canvas[2].SetActive(false);
        Canvas[3].SetActive(true);
    }

}
