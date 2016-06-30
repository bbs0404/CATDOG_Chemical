using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIManager : SingletonBehaviour<GameUIManager> {

    public Canvas InGameCanvas = null;
    public Canvas MenuCanvas = null;
    public Canvas SkillBookCanvas = null;

    void Awake()
    {
        if (GameStateManager.Inst().getState() == State.INGAME)
        {
            InGameCanvas.gameObject.SetActive(true);
            MenuCanvas.gameObject.SetActive(false);
        }
        else
        {
            InGameCanvas.gameObject.SetActive(false);
            MenuCanvas.gameObject.SetActive(true);
        }
    }

    public void OnStateChanged(State state)
    {
        if (state == State.INGAME)
        {
            InGameCanvas.gameObject.SetActive(true);
            MenuCanvas.gameObject.SetActive(false);
        }
        else
        {
            InGameCanvas.gameObject.SetActive(false);
            MenuCanvas.gameObject.SetActive(true);
        }
    }
}
