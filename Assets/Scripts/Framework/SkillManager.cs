using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill
{
    public string Combination;
    public int damage;
    public Type type;
    public Skill(string str, int dmg, Type type)
    {
        this.Combination = str;
        this.damage = dmg;
        this.type = type;
    }
}

public class SkillManager : SingletonBehaviour<SkillManager> {

    void Start()
    {
        Inst();
    }

    private List<Skill> SkillList = new List<Skill>();

    public void addNewSkill(string str, int dmg, Type type)
    {
        Skill skill = new Skill(str, dmg, type);
        SkillList.Add(skill);
    }

    public List<Skill> getSkillList()
    {
        return SkillList;
    }
}
