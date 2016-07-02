using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InGameSceneController : SingletonBehaviour<InGameSceneController> {

    GameStateManager stateM = null;
    GameUIManager gameUI = null;

    void Awake()
    {
        stateM = GameStateManager.Inst();
        gameUI = GameUIManager.Inst();
    }

    public void OnButtonClicked(GameObject button)
    {
        switch(button.name)
        {
            case "PauseButton":
                {
                    stateM.setState(State.PAUSE);
                    gameUI.OnStateChanged(State.PAUSE);
                    break;
                }
            case "ResumeButton":
                {
                    stateM.setState(State.INGAME);
                    gameUI.OnStateChanged(State.INGAME);
                    break;
                }
            case "MainMenuButton":
                {
                    SceneManager.LoadScene("Title");
                    break;
                }
            case "CButton":
            case "SButton":
            case "HButton":
            case "OButton":
            case "PButton":
                {
                    InGameSystemManager.Inst().addCombination(button.name[0]);
                    //switch (button.name[0])
                    //{
                    //    case 'C':
                    //        {
                    //            InGameSystemManager.Inst().useCost(1.0f);
                    //            break;
                    //        }
                    //}
                    Debug.Log("current combination : " + InGameSystemManager.Inst().getCombination());
                    break;
                }
            case "SkillBookButton":
                {

                    break;
                }
        }
    }
}
