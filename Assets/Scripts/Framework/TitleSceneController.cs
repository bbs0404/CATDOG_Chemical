using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneController : SingletonBehaviour<TitleSceneController> {

    public void onButtonClicked(Button button)
    {
        switch (button.gameObject.name)
        {
            case "StartGameButton":
                SaveHelper.Delete("/player_manager_data");
                SaveHelper.Delete("/inventory_manager_data_1");
                SaveHelper.Delete("/inventory_manager_data_2");
                SaveHelper.Delete("/ingame_system_manager_villagenum");
                PlayerManager.playerLevel = 0;
                PlayerManager.playerEXP = 0;
                InventoryManager.proton = 0;
                InventoryManager.neutron = 0;
                InventoryManager.electron = 0;
                PlayerManager.villageProgress = 0;
                InGameSystemManager.villageNum = 0;
                InventoryManager.Element.data = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
                InventoryManager.Raw.data = new bool[4] { true, false, false, false };
                SceneManager.LoadScene("Tutorial");
                //InGameSystemManager.villageNum = 0;
                break;
            case "LoadGameButton":
                //InGameSystemManager ingamesystem =  SaveHelper.Load<InGameSystemManager>("/gamedata");
                //PlayerManager playermanager = SaveHelper.Load<PlayerManager>("/gamedata");
                //InventoryManager inventorymanager = SaveHelper.Load<InventoryManager>("/gamedata");
                //InGameSystemManager.setInst(ingamesystem);
                //PlayerManager.setInst(playermanager);
                //InventoryManager.setInst(inventorymanager);
                SceneManager.LoadScene(1);
                break;
            case "OptionButton":
                break;
        }
    }
}
