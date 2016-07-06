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
                    switch (button.name[0])
                    {
                        case 'C':
                        case 'H':
                        case 'O':
                            if (InGameSystemManager.Inst().getCost() < 1)
                            {
                                Debug.Log("Not enough cost");
                                return;
                            }
                            InGameSystemManager.Inst().useCost(1.0f);
                            break;
                        case 'S':
                        case 'P':
                            if (InGameSystemManager.Inst().getCost() < 1.5f)
                            {
                                Debug.Log("Not enough cost");
                                return;
                            }
                            InGameSystemManager.Inst().useCost(1.5f);
                            break;
                    }
                    InGameSystemManager.Inst().costTextUpdate();
                    InGameSystemManager.Inst().addCombination(button.name[0]);
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
