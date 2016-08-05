using UnityEngine;
using System.Collections;

public class ObjectUnit : MonoBehaviour {
    [SerializeField]
    private int MaxHP, CurrentHP;
    [SerializeField]
    private int Level, Attack, Defend, EXP;

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
}