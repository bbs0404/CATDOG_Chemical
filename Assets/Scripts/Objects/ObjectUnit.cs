using UnityEngine;
using System.Collections;

public class ObjectUnit : MonoBehaviour {
    [SerializeField]
    float MaxHP, CurrentHP;
    [SerializeField]
    float EXP;
    [SerializeField]
    private int Level, Attack, Defend;

    public float getHP()
    {
        return CurrentHP;
    }

    public void setHP(float newHP)
    {
        CurrentHP = newHP;
    }

    public void GetDamaged(float damage)
    {
        CurrentHP -= damage;
    }

    public float getEXP()
    {
        return EXP;
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