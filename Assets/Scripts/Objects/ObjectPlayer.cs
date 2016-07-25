using UnityEngine;
using System.Collections;

public class ObjectPlayer : ObjectUnit {
    private Animator animator;

    public Animator getAnimator() {
        if ( animator == null )
            animator = GetComponent<Animator>();
        return animator;
    }

    void Update() {
        if (getHP() <= 0) {
            getAnimator().SetBool("dead", true);
            GameStateManager.Inst().setState(State.END);
            InGameUIManager.Inst().resultTextUpdate();
            InGameUIManager.Inst().OnStateChanged(State.END);
        }
    }
}