using UnityEngine;
using System.Collections;

public enum ArmorType
{
    HAT,
    TOP,
    BOTTOMS,
    GLOVES,
    SHOES,
    WEAPON
}

public class ObjectArmor : MonoBehaviour {
    
    [SerializeField]    
    private ArmorType armor;
    [SerializeField]
    private int HP, ATK, DEF;
	
    public int getHP()
    {
        return HP;
    }

    public int getATK()
    {
        return ATK;
    }

    public int getDEF()
    {
        return DEF;
    }

    public ArmorType getArmorType()
    {
        return armor;
    }
}
