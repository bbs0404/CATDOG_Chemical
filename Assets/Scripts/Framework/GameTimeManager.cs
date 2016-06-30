using UnityEngine;
using System.Collections;

public class GameTime
{
    public static float deltaTime
    {
        get { return GameTimeManager.Inst().GetInGameDeltaTime(); }
    }
}

public class GameTimeManager : SingletonBehaviour<GameTimeManager> {
    [SerializeField]
    private GameStateManager stateManager;
    
    void Start()
    {
        Inst();
    }

	public float GetInGameDeltaTime()
    {
        if (stateManager.getState() == State.INGAME)
        {
            return Time.deltaTime;
        }
        else
        {
            return 0;
        }
    }
}