using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Skill
{
    public string       Combination;
    public float          damage;
    public StateType    type;
    public bool         global;
    public StatusEffect statusEffect;
	public bool         unlocked;
    public int          imageNumber;

    public Skill(string str, float dmg, StateType type, bool globalAttack, StatusEffect statusEffect, bool unlock = false, int imageNumber=0)
    {
        this.Combination  = str;
        this.damage       = dmg;
        this.type         = type;
        this.global       = globalAttack;
        this.statusEffect = statusEffect;
		this.unlocked     = unlock;
        this.imageNumber  = imageNumber;
    }

    public Skill UnlockedSkill {
        get {
            return new Skill(Combination, damage, type, global, statusEffect, true, imageNumber);
        }
    }
}

public class SkillManager : SingletonBehaviour<SkillManager> {
    
    void Start() {
        Inst();

        addNewSkill(new Skill("CC"   , 1.4f, StateType.Solid , false, StatusEffect.None     , true, 0));
        addNewSkill(new Skill("HH"   , 0.95f, StateType.Gas   , false, StatusEffect.Burn     , true, 1));
        addNewSkill(new Skill("OO"   , 1, StateType.Gas   , false, StatusEffect.None     , true, 2));
        addNewSkill(new Skill("COO"  , 1.35f, StateType.Solid , false, StatusEffect.Frostbite, true, 3));
        addNewSkill(new Skill("HHO"  , 0.75f, StateType.Solid , true , StatusEffect.Frostbite, true, 4));
        addNewSkill(new Skill("HHOO" , 1.35f, StateType.Liquid, false, StatusEffect.Corrosion, true, 5));
        addNewSkill(new Skill("COOHH", 1.5f, StateType.Liquid, false, StatusEffect.Corrosion, true, 6));
        
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
