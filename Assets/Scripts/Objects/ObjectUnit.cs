﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectUnit : MonoBehaviour {
    [SerializeField]
    private int MaxHP, CurrentHP;
    [SerializeField]
    private int Level, Attack, Defend, EXP;
    [SerializeField]
    private StatusEffect curState =StatusEffect.None;
    private int statusRemainTurn = 0;
    private bool moveable = true;
    [SerializeField]
    private Text LVtext;
    void Start()
    {
        LVtext.text = "LV." + (Level + 1).ToString();
    }

    public int getHP()
    {
        return CurrentHP;
    }

    public void setHP(int newHP)
    {
        CurrentHP = newHP;
    }

    public void setMaxHP(int max)
    {
        MaxHP = max;
    }

    public int getMaxHP()
    {
        return MaxHP;
    }

    public void GetDamaged(int damage)
    {
        CurrentHP -= damage;
    }

    public int getEXP()
    {
        return EXP;
    }

    public void setEXP(int exp)
    {
        EXP = exp;
    }

    public void setLevel(int lev)
    {
        Level = lev;
    }

    public int getLevel()
    {
        return Level;
    }

    public void setAttack(int atk)
    {
        Attack = atk;
    }

    public int getAttack()
    {
        return Attack;
    }

    public void setDefend(int def)
    {
        Defend = def;
    }

    public int getDefend()
    {
        return Defend;
    }
    public StatusEffect getStatusEffect()
    {
        return curState;
    }
    public void setStatusEffect(StatusEffect newState)
    {
        curState = newState;
    }
    public int getStatusRemainTurn()
    {
        return statusRemainTurn;
    }
    public void setStatusRemainTurn(int turn)
    {
        statusRemainTurn = turn;
    }
    public bool isMoveable()
    {
        return moveable;
    }
    public void setMoveable(bool able)
    {
        moveable = able;
    }
    public Text getLVtext()
    {
        return LVtext;
    }
}