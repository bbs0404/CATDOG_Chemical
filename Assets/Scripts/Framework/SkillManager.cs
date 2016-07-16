using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill
{
    public string Combination;
    public int damage;
    public Type type;
	public bool unlocked;

    public Skill(string str, int dmg, Type type)
    {
        this.Combination = str;
        this.damage = dmg;
        this.type = type;
		this.unlocked = false;
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

	public List<Skill> getSkillListAll() {
		return SkillList;
	}

	public List<Skill> getSkillListUnlocked() {
		var res = new List<Skill> ();
		foreach (var skill in SkillList) {
			if (skill.unlocked) {
				res.Add (skill);
			}
		}
		return res;
	}

    public List<Skill> getSkillList()
    {
		return getSkillListUnlocked();
    }
}
