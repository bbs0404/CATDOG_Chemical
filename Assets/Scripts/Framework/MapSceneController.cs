using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MapSceneController : SingletonBehaviour<MapSceneController>{

    [SerializeField]
    private int tmpVillageNum;
    private bool Village = true;

    void Start()
    {
        tmpVillageNum = InGameSystemManager.villageNum;
        Village = true;
    }

    public void OnVillageButtonClicked(int villageNum)
    {
        tmpVillageNum = villageNum;
        Village = true;
        MapSceneUIManager.Inst().updateVillages();
    }

    public void OnRoadButtonClicked(int roadNum)
    {
        tmpVillageNum = roadNum;
        Village = false;
        MapSceneUIManager.Inst().updateVillages();

    }
    public void OnGoToVillageButtonClicked()
    {
        InGameSystemManager.villageNum = tmpVillageNum;
        SceneManager.LoadScene("Village");
    }

    public void OnGoToAdventureButtonClicked()
    {
        InGameSystemManager.villageNum = tmpVillageNum;
        SceneManager.LoadScene("InGame(Laziu)");
    }

    public bool isVillage()
    {
        return Village;
    }

    public int getTmpVillageNum()
    {
        return tmpVillageNum;
    }
}
