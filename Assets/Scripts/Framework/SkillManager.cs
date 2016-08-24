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
    public Sprite       sprite;

    public Skill(string str, float dmg, StateType type, bool globalAttack, StatusEffect statusEffect, Sprite sprite, bool unlock = false)
    {
        this.Combination  = str;
        this.damage       = dmg;
        this.type         = type;
        this.global       = globalAttack;
        this.statusEffect = statusEffect;
		this.unlocked     = unlock;
        this.sprite = sprite;
    }

    public Skill UnlockedSkill {
        get {
            return new Skill(Combination, damage, type, global, statusEffect, sprite, true);
        }
    }
}

public class SkillManager : SingletonBehaviour<SkillManager> {

    [SerializeField]
    private Sprite[] skillSpirtes = new Sprite[7];

    void Start() {
        Inst();

        addNewSkill(new Skill("CC", 1.4f, StateType.Solid, false, StatusEffect.None, skillSpirtes[0], true));
        addNewSkill(new Skill("HH"   , 0.95f, StateType.Gas   , false, StatusEffect.Burn , skillSpirtes[1], true));
        addNewSkill(new Skill("OO"   , 1, StateType.Gas   , false, StatusEffect.None, skillSpirtes[2], true));
        addNewSkill(new Skill("COO"  , 1.35f, StateType.Solid , false, StatusEffect.Frostbite, skillSpirtes[3], true));
        addNewSkill(new Skill("HHO"  , 0.75f, StateType.Solid , true , StatusEffect.Frostbite, skillSpirtes[4],true));
        addNewSkill(new Skill("HHOO" , 1.35f, StateType.Liquid, false, StatusEffect.Corrosion, skillSpirtes[5], true));
        addNewSkill(new Skill("COOHH", 1.5f, StateType.Liquid, false, StatusEffect.Corrosion, skillSpirtes[6], true));
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
