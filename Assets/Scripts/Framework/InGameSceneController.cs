using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InGameSceneController : SingletonBehaviour<InGameSceneController> {

    GameStateManager stateM = null;
    InGameUIManager gameUI = null;

    void Awake()
    {
        stateM = GameStateManager.Inst();
        gameUI = InGameUIManager.Inst();
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
                    if (!InGameSystemManager.Inst().isInBattle())
                        return;
                    if (InGameSystemManager.Inst().getCombination().Length == 6)
                    {
                        Debug.Log("Combination length is max");
                        return;
                    }
                    switch (button.name[0])
                    {
                        case 'C':
                        case 'H':
                        case 'O':
                            if (InGameSystemManager.Inst().getCost() < 0.5)
                            {
                                Debug.Log("Not enough cost");
                                return;
                            }
                            InGameSystemManager.Inst().useCost(0.5f);
                            break;
                        case 'S':
                        case 'P':
                            if (InGameSystemManager.Inst().getCost() < 1f)
                            {
                                Debug.Log("Not enough cost");
                                return;
                            }
                            InGameSystemManager.Inst().useCost(1f);
                            break;
                    }
                    InGameUIManager.Inst().costTextUpdate();
                    InGameSystemManager.Inst().addCombination(button.name[0]);
                    InGameUIManager.Inst().combinationTextUpdate();
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
