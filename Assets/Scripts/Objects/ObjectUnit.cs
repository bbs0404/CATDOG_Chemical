using UnityEngine;
using System.Collections;

public class ObjectUnit : MonoBehaviour {
    [SerializeField]
    private float HP;
    [SerializeField]
    private float EXP;
    [SerializeField]
    private int level;

    public float getHP()
    {
        return HP;
    }

    public void setHP(float newHP)
    {
        HP = newHP;
    }

    public void GetDamaged(float damage)
    {
        HP -= damage;
    }

    public float getEXP()
    {
        return EXP;
    }

    public int getLevel()
    {
        return level;
    }
}