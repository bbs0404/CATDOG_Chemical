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
   
    void Start()
    {
        Inst();
    }

	public float GetInGameDeltaTime()
    {
        if (GameStateManager.Inst().getState() == State.INGAME)
        {
            return Time.deltaTime;
        }
        else
        {
            return 0;
        }
    }
}