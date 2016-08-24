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
                SceneManager.LoadScene(1);
                //PlayerManager.playerEXP = 0;
                //PlayerManager.playerLevel = 0;
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
