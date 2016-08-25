using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ObjectMob : ObjectUnit
{
    [SerializeField]
    private StateType curType;
    [SerializeField]
    private int mob_seed;
    [SerializeField]
    private StatusEffect status;

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
            PlayerManager.playerEXP = PlayerManager.Inst().getPlayer().getEXP();
            InGameSystemManager.Inst().setMobNum(InGameSystemManager.Inst().getMobNum() - 1);
            switch (mob_seed)
            {
                case 0:
                    InventoryManager.Inst().setProton(InventoryManager.Inst().getProton() + Random.Range(6, 11));
                    InventoryManager.Inst().setNeutron(InventoryManager.Inst().getNeutron() + Random.Range(2, 7));
                    InventoryManager.Inst().setElectron(InventoryManager.Inst().getElectron() + Random.Range(4, 9) * getLevel());
                    break;
                case 1:
                    InventoryManager.Inst().setProton(InventoryManager.Inst().getProton() + Random.Range(2, 7));
                    InventoryManager.Inst().setNeutron(InventoryManager.Inst().getNeutron() + Random.Range(4, 9));
                    InventoryManager.Inst().setElectron(InventoryManager.Inst().getElectron() + Random.Range(6, 11) * getLevel());
                    break;
            }
            gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (InGameSystemManager.Inst().isAttackReady() && GameStateManager.Inst().getState() == State.INGAME)
        {
            InGameSystemManager.Inst().Attack(this);
        }
    }

    public int getmobSeed()
    {
        return mob_seed;
    }

    public void setmobSeed(int seed)
    {
        mob_seed = seed;
    }

    public StatusEffect getStatus()
    {
        return status;
    }
}