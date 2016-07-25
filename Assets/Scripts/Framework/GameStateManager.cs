using UnityEngine;
using System.Collections;

public enum State
{
    INGAME,
    PAUSE,
    END
}

public class GameStateManager : SingletonBehaviour<GameStateManager> {

    [SerializeField]
    private State currentState;

    void Awake()
    {
        currentState = State.INGAME;
    }

    public State getState()
    {
        return currentState;
    }
    
    public void setState(State changeState)
    {
        currentState = changeState;
    }
}