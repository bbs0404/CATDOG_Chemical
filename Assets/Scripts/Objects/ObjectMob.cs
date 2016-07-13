using UnityEngine;
using System.Collections;

public class ObjectMob : ObjectUnit
{
    [SerializeField]
    private Type curType;

    public void setType(Type type)
    {
        curType = type;
    }

    public Type getType()
    {
        return curType;
    }

    public void mobDead()
    {
        if (this.getHP() <= 0)
        {
            PlayerManager.Inst().getPlayer().setEXP(PlayerManager.Inst().getPlayer().getEXP() + this.getEXP());
            InGameSystemManager.Inst().setMobNum(InGameSystemManager.Inst().getMobNum() - 1);
            Destroy(this.gameObject);
        }
    }

    void OnMouseDown()
    {
        if (InGameSystemManager.Inst().isAttackReady() && GameStateManager.Inst().getState() == State.INGAME)
        {
            InGameSystemManager.Inst().Attack(this);
        }
    }
}