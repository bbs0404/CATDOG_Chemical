using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class PlayerManager : SingletonBehaviour<PlayerManager> {

    [SerializeField]
    private ObjectPlayer player;

    public int playerLevel = 0;
    public int playerEXP = 0;

    public bool[] Raw = new bool[4] { true, false, false, false };
    public bool[] Element = new bool[20] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

    void Start()
    {
        //Load Data from Save
        int hp = 500, atk = 300, def = 300, exp = 100;
        for (int i = 0; i < playerLevel; ++i)
        {
            hp += (int)(player.getLevel() * 7 + player.getDefend() * 0.1);
            atk += (int)(player.getAttack() * 0.1 + player.getLevel() * 15.7);
            def += (int)(player.getLevel() * 13 + player.getDefend() * 0.07);
            exp += (int)(player.getLevel() * 25.7 + 53);
        }
        player.setMaxHP(hp);
        player.setHP(hp);
        player.setAttack(atk);
        player.setDefend(def);
        player.setEXPtoLevelUP(exp);
        player.setLevel(playerLevel);
        player.setEXP(playerEXP);
    }

    public ObjectPlayer getPlayer()
    {
        return player;
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
        player.getLVtext().text = "LV." + (player.getLevel() + 1).ToString();
        playerLevel = player.getLevel();
        playerEXP = player.getEXP();
    }
}
