using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ObjectMob : ObjectUnit
{
    [SerializeField]
    private StateType curType;

    void Start() {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener( _ => OnMouseDown() );
        GetComponent<EventTrigger>().triggers.Add(entry);
    }

    public void setType(StateType type)
    {
        curType = type;
    }

    public StateType getType()
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