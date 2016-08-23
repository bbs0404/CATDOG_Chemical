using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Skill
{
    public string       Combination;
    public int          damage;
    public StateType    type;
    public bool         global;
    public StatusEffect statusEffect;
	public bool         unlocked;

    public Skill(string str, int dmg, StateType type, bool globalAttack, StatusEffect statusEffect, bool unlock = false)
    {
        this.Combination  = str;
        this.damage       = dmg;
        this.type         = type;
        this.global       = globalAttack;
        this.statusEffect = statusEffect;
		this.unlocked     = unlock;
    }

    public Skill UnlockedSkill {
        get {
            return new Skill(Combination, damage, type, global, statusEffect, true);
        }
    }
}

public class SkillManager : SingletonBehaviour<SkillManager> {

    void Start() {
        Inst();

        addNewSkill(new Skill("CC"   , 50, StateType.Solid , false, StatusEffect.None     , true));
        addNewSkill(new Skill("HH"   , 25, StateType.Gas   , false, StatusEffect.Burn     , true));
        addNewSkill(new Skill("OO"   , 30, StateType.Gas   , false, StatusEffect.None     , true));
        addNewSkill(new Skill("COO"  , 45, StateType.Solid , false, StatusEffect.Frostbite, true));
        addNewSkill(new Skill("HHO"  , 20, StateType.Solid , true , StatusEffect.Frostbite, true));
        addNewSkill(new Skill("HHOO" , 45, StateType.Liquid, false, StatusEffect.Corrosion, true));
        addNewSkill(new Skill("COOHH", 75, StateType.Liquid, false, StatusEffect.Corrosion, true));
    }

    private List<Skill> SkillList = new List<Skill>();

    public void addNewSkill(Skill skill) {
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

    public Skill? findSkill(string combination) {
        foreach ( var item in getSkillListUnlocked() ) {
            if ( string.Equals(item.Combination, combination) ) {
                Debug.Log("found skill : " + item);
                return item;
            }
        }
        return null;
    }

    public void Unlock(string combination) {
        for ( int i = 0; i < SkillList.Count; ++i ) {
            if ( string.Equals(SkillList[i].Combination, combination) ) {
                SkillList[i] = SkillList[i].UnlockedSkill;
            }
        }
    }
}
