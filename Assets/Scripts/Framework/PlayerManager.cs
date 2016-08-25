using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : SingletonBehaviour<PlayerManager> {

    [SerializeField]
    private ObjectPlayer player;

    private static int _playerLevel = 0;
    private static int _playerEXP = 0;
    private static int _villageProgress = 0; //플레이어의 최대 마을거리

    public static int playerLevel {
        get { return _playerLevel; }
        set {
            if (_playerLevel != value) {
                _playerLevel = value;
                Save();
            }
        }
    }
    public static int playerEXP {
        get { return _playerEXP; }
        set {
            if (_playerEXP != value) {
                _playerEXP = value;
                Save();
            }
        }
    }
    public static int villageProgress {
        get { return _villageProgress; }
        set {
            if (_villageProgress != value) {
                _villageProgress = value;
                Save();
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "InGame(Laziu)" && player == null)
        {
            player = GameObject.FindObjectOfType<ObjectPlayer>();
            PlayerStatUpdate();
        }
    }

    void Awake() 
    {
        Load();
    }

    void Start()
    {
        SetStatic();
        player = GameObject.FindObjectOfType<ObjectPlayer>();
        //Load Data from Save
        PlayerStatUpdate();
    }

    public void PlayerStatUpdate()
    {
        if (player != null)
        {
            int hp = 500, atk = 300, def = 300, exp = 100;
            for (int i = 0; i < playerLevel; ++i)
            {
                hp += (int)(i * 7 + player.getDefend() * 0.1);
                atk += (int)(player.getAttack() * 0.1 + i * 15.7);
                def += (int)(i * 13 + player.getDefend() * 0.07);
                exp += (int)(i * 25.7 + 53);
            }
            player.setMaxHP(hp);
            player.setHP(hp);
            player.setAttack(atk);
            player.setDefend(def);
            player.setEXPtoLevelUP(exp);
            player.setLevel(playerLevel);
            player.setEXP(playerEXP);
            player.getLVtext().text = "LV." + (player.getLevel() + 1).ToString();
        }
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

    static void Save() {
        Debug.Log("PlayerManager.Save called");
        var data = new PlayerManagerData();
        data.playerLevel = playerLevel;
        data.playerExp = playerEXP;
        data.villageProgress = villageProgress;
        SaveHelper.Save(data, "/player_manager_data");
    }

    public static void Load() {
        Debug.Log("PlayerManager.Load called");
        var data = SaveHelper.Load<PlayerManagerData>("/player_manager_data");
        if (data != null) {
            playerLevel = data.playerLevel;
            playerEXP = data.playerExp;
            villageProgress = data.villageProgress;
        }
    }
}


[System.Serializable]
class PlayerManagerData
{
    public int playerLevel;
    public int playerExp;
    public int villageProgress;
}