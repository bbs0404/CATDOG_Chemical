using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapSceneUIManager : SingletonBehaviour<MapSceneUIManager> {

    [SerializeField]
    private Sprite lockedVillageSprite;
    [SerializeField]
    private Button[] VillageButtons = new Button[4];
    [SerializeField]
    private Button[] RoadButtons = new Button[3];
    [SerializeField]
    private Image arrow;
    [SerializeField]
    private Button GoToVillageButton, GoToAdventureButton;
    [SerializeField]
    private string[] Names = new string[7];
    [SerializeField]
    private string[] Description = new string[8];
    [SerializeField]
    private Text NameText, DescriptionText;
	
	public void updateVillages()
    {
        for (int i = PlayerManager.villageProgress + 1; i < 4; ++i)
        {
            VillageButtons[i].image.sprite = lockedVillageSprite;
            VillageButtons[i].interactable = false;
        }
        for (int i = PlayerManager.villageProgress +1;i<3; ++i)
        {
            RoadButtons[i].interactable = false;
        }
        if (MapSceneController.Inst().isVillage())
        {
            GoToVillageButton.gameObject.SetActive(true);
            GoToAdventureButton.gameObject.SetActive(false);
            arrow.rectTransform.localPosition = new Vector3(0, 50, 0) + VillageButtons[MapSceneController.Inst().getTmpVillageNum()].GetComponent<RectTransform>().localPosition;
            NameText.text = Names[MapSceneController.Inst().getTmpVillageNum()];
            DescriptionText.text = Description[MapSceneController.Inst().getTmpVillageNum()];
        }
        else
        {
            GoToVillageButton.gameObject.SetActive(false);
            GoToAdventureButton.gameObject.SetActive(true);
            arrow.rectTransform.localPosition = new Vector3(0, 50, 0) + new Vector3((VillageButtons[MapSceneController.Inst().getTmpVillageNum()].GetComponent<RectTransform>().localPosition.x + VillageButtons[MapSceneController.Inst().getTmpVillageNum() + 1].GetComponent<RectTransform>().localPosition.x)/2, VillageButtons[MapSceneController.Inst().getTmpVillageNum()].GetComponent<RectTransform>().localPosition.y);
            NameText.text = Names[MapSceneController.Inst().getTmpVillageNum() + 4];
            DescriptionText.text = Description[MapSceneController.Inst().getTmpVillageNum() + 4];
        }
    }
}
