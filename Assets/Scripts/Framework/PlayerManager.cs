using UnityEngine;
using System.Collections;

public class PlayerManager : SingletonBehaviour<PlayerManager> {

    [SerializeField]
    private ObjectPlayer player;
    [SerializeField]
    private ObjectArmor hat = null;
    [SerializeField]
    private ObjectArmor top = null;
    [SerializeField]
    private ObjectArmor bottoms = null;
    [SerializeField]
    private ObjectArmor gloves = null;
    [SerializeField]
    private ObjectArmor shoes = null;
    [SerializeField]
    private ObjectArmor weapon = null;

    void Start()
    {
        //Load Data from Save
    }

    public ObjectPlayer getPlayer()
    {
        return player;
    }

    public void setHat(ObjectArmor armor)
    {
        if (armor.getArmorType() != ArmorType.HAT)
        {
            Debug.Log("incorrect armortype");
            return;
        }
        changeStatByArmor(hat, armor);
        hat = armor;
    }

    public void setTop(ObjectArmor armor)
    {
        if (armor.getArmorType() != ArmorType.TOP)
        {
            Debug.Log("incorrect armortype");
            return;
        }
        changeStatByArmor(top, armor);
        top = armor;
    }

    public void setBottoms(ObjectArmor armor)
    {
        if (armor.getArmorType() != ArmorType.BOTTOMS)
        {
            Debug.Log("incorrect armortype");
            return;
        }
        changeStatByArmor(bottoms, armor);
        bottoms = armor;
    }

    public void setGloves(ObjectArmor armor)
    {
        if (armor.getArmorType() != ArmorType.GLOVES)
        {
            Debug.Log("incorrect armortype");
            return;
        }
        changeStatByArmor(gloves, armor);
        gloves = armor;
    }

    public void setShoes(ObjectArmor armor)
    {
        if (armor.getArmorType() != ArmorType.SHOES)
        {
            Debug.Log("incorrect armortype");
            return;
        }
        changeStatByArmor(shoes, armor);
        shoes = armor;
    }

    public void setWeapon(ObjectArmor armor)
    {
        if (armor.getArmorType() != ArmorType.WEAPON)
        {
            Debug.Log("incorrect armortype");
            return;
        }
        changeStatByArmor(weapon, armor);
        weapon = armor;
    }

    public void changeStatByArmor(ObjectArmor prev, ObjectArmor now)
    {
        if (prev.getArmorType() != now.getArmorType())
        {
            Debug.Log("incorrect armortype : changeStatByArmor()");
            return;
        }
        if (prev == null)
        {
            player.setAttack(player.getAttack() + now.getATK());
            player.setDefend(player.getDefend() + now.getDEF());
            player.setHP(player.getHP() + now.getHP());
        }
        else if (now == null)
        {
            player.setAttack(player.getAttack() - prev.getATK());
            player.setDefend(player.getDefend() - prev.getDEF());
            player.setHP(player.getHP() - prev.getHP());
        }
        else
        {
            player.setAttack(player.getAttack() - prev.getATK() + now.getATK());
            player.setDefend(player.getDefend() - prev.getDEF() + now.getDEF());
            player.setHP(player.getHP() - prev.getHP() + now.getHP());
        }
    }

    public void levelUp()
    {
        int atk = player.getAttack(), def = player.getDefend(), hp = player.getMaxHP();
        player.setLevel(player.getLevel() + 1);
        player.setAttack(Mathf.RoundToInt((float)(atk + player.getLevel() * 19 + atk * 0.1)));
        player.setMaxHP(Mathf.RoundToInt((float)(hp + player.getLevel() * 7 + def * 0.1)));
        player.setHP(player.getMaxHP());
        player.setDefend(Mathf.RoundToInt((float)(def + player.getLevel() * 17 + def * 0.13)));
        player.setEXP(player.getEXP() - player.getEXPtoLevelUP());
        player.setEXPtoLevelUP(player.getEXPtoLevelUP() + (int)(player.getLevel() * 25.7) + 53);
    }
}
