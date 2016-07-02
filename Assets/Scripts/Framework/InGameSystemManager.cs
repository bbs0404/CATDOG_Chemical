using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InGameSystemManager : SingletonBehaviour<InGameSystemManager>
{
    private float cost;
    private string Combination = null;

    void Start()
    {
        if (GameStateManager.Inst().getState() == State.PAUSE)
            GameStateManager.Inst().setState(State.INGAME);

    }

    public void addCombination(char c)
    {
        Combination = Combination + c;
    }

    public string getCombination()
    {
        return Combination;
    }

    public void setCost(float newCost)
    {
        cost = newCost;
    }

    public void useCost(float usage)
    {
        if (usage > cost)
        {
            Debug.Log("Usage cost is bigger than current cost");
            return;
        }
        cost -= usage;
    }

    public Skill checkCombination()
    {
        foreach (var item in SkillManager.Inst().getSkillList())
        {
            if (item.Combination.CompareTo(Combination) == 0)
            {
                Debug.Log("found skill : " + item);
                return item;
            }
        }
        return null;
    }

    public void Attack(ObjectMob mob)
    {
        Skill skill = checkCombination();
        if (skill == null)
        {
            for (int i=0; i<Combination.Length; ++i)
            {
                switch (Combination[i])
                {
                    case 'C':

                        break;
                }
            }
        }
        else
        {
            //mob's type vs skill type
            mob.GetDamaged(skill.damage);
        }
    }
}