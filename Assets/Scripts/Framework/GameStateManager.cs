using UnityEngine;
using System.Collections;

public enum State
{
    INGAME,
    PAUSE
}

public class GameStateManager : SingletonBehaviour<GameStateManager> {

    private State currentState;

    public State getState()
    {
        return currentState;
    }
    
    public void setState(State changeState)
    {
        currentState = changeState;
    }
}
