using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InGameSystemManager : SingletonBehaviour<InGameSystemManager>
{
    private float cost;
    private string Combination = null;

    void Start()
    {
        Inst();
    }

    public void addCombination(char c)
    {
        Combination = Combination + 'c';
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
                return item;
            }
        }
        return null;
    }
}