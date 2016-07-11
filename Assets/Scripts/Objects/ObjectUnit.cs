using UnityEngine;
using System.Collections;

public class ObjectUnit : MonoBehaviour {
    [SerializeField]
    private float MaxHP, CurrentHP;
    [SerializeField]
    private int Level, Attack, Defend, EXP;

    public float getHP()
    {
        return CurrentHP;
    }

    public void setHP(float newHP)
    {
        CurrentHP = newHP;
    }

    public void setMaxHP(float max)
    {
        MaxHP = max;
    }

    public float getMaxHP()
    {
        return MaxHP;
    }

    public void GetDamaged(float damage)
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