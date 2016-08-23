using UnityEngine;
using System.Collections;

public class ElementTable : MonoBehaviour {
    public GameObject H;
    public GameObject He;
    public GameObject Li;
    public GameObject Be;
    public GameObject B;
    public GameObject C;
    public GameObject N;
    public GameObject O;
    public GameObject F;
    public GameObject Ne;
    public GameObject Na;
    public GameObject Mg;
    public GameObject Al;
    public GameObject Si;
    public GameObject P;
    public GameObject S;
    public GameObject Cl;
    public GameObject Ar;
    public GameObject K;
    public GameObject Ca;




    // Use this for initialization
    void Start () {
        if (!PlayerManager.Inst().Element[2]) Li.SetActive(false);
        if (!PlayerManager.Inst().Element[3]) Be.SetActive(false);
        if (!PlayerManager.Inst().Element[4]) B.SetActive(false);
        if (!PlayerManager.Inst().Element[5]) C.SetActive(false);
        if (!PlayerManager.Inst().Element[6]) N.SetActive(false);
        if (!PlayerManager.Inst().Element[7]) O.SetActive(false);
        if (!PlayerManager.Inst().Element[8]) F.SetActive(false);
        if (!PlayerManager.Inst().Element[9]) Ne.SetActive(false);
        if (!PlayerManager.Inst().Element[10]) Na.SetActive(false);
        if (!PlayerManager.Inst().Element[11]) Mg.SetActive(false);
        if (!PlayerManager.Inst().Element[12]) Al.SetActive(false);
        if (!PlayerManager.Inst().Element[13]) Si.SetActive(false);
        if (!PlayerManager.Inst().Element[14]) P.SetActive(false);
        if (!PlayerManager.Inst().Element[15]) S.SetActive(false);
        if (!PlayerManager.Inst().Element[16]) Cl.SetActive(false);
        if (!PlayerManager.Inst().Element[17]) Ar.SetActive(false);
        if (!PlayerManager.Inst().Element[18]) K.SetActive(false);
        if (!PlayerManager.Inst().Element[19]) Ca.SetActive(false);


    }

    // Update is called once per frame
    void Update () {
	
	}
}
