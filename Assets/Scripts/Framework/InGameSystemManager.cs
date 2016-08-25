using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

enum GameTurn
{
    PLAYER,
    ENEMY
}

[System.Serializable]
public class InGameSystemManager : SingletonBehaviour<InGameSystemManager>
{
    private const float burnDamage = 1.25f;
    private const float freezeDamage = 0.75f;

    private float cost;
    private float maxCost;
    [SerializeField]
    private string Combination = "";
    [SerializeField]
    public static int villageNum = 0; //현재 플레이어가 있는 마을의 넘버
    private int progress = 0; //진행한 미터
    [SerializeField]
    private int distance = 5; //마을간의 거리
    private float waitSecond = 0;
    private int mob_number;

    [SerializeField]
    private float battleTimer = 2.5f;

    private GameTurn currentTurn = GameTurn.PLAYER;
    [SerializeField]
    private bool AttackReady = false;
    private bool InBattle = false;
    [SerializeField]
    private AudioSource attack_sfx;

    [SerializeField]
    private List<GameObject> mobPrefab;
    private ObjectMob[] enemies;

    [SerializeField]
    private Sprite[] StatusSprites = new Sprite[3];
    void Start()
    {
        distance = 5 + villageNum * 3;
        progress = 0;
        if (GameStateManager.Inst().getState() == State.PAUSE)
            GameStateManager.Inst().setState(State.INGAME);
        InGameUIManager.Inst().progressUpdate(progress);
        InGameUIManager.Inst().combinationTextUpdate();
        InGameUIManager.Inst().HPbarUpdate();
    }

    void Update()
    {
        if (battleTimer > 0 && !InBattle)
        {
            battleTimer -= GameTime.deltaTime;
            return;
        }
        else if (battleTimer <= 0)
        {
            battleTimer = 1;
            if (progress >= distance) {
                if (villageNum == PlayerManager.villageProgress)
                {
                    villageNum = ++PlayerManager.villageProgress;
                }
                GameStateManager.Inst().setState(State.END);
                InGameUIManager.Inst().resultTextUpdate();
                InGameUIManager.Inst().OnStateChanged(State.END);
            }
            else
                battleStart();
        }
        if (waitSecond > 0)
        {
            waitSecond -= GameTime.deltaTime;
            return;
        }
        if (currentTurn == GameTurn.PLAYER)
        {
            playerTurn();
        }
        else
        {
            enemyTurn();
        }
    }

    public void addCombination(char c)
    {
        if (currentTurn == GameTurn.PLAYER)
        {
            Combination = Combination + c;
        }
    }

    public string getCombination()
    {
        return Combination;
    }

    public float getCost()
    {
        return cost;
    }

    public void setCost(float newCost)
    {
        cost = newCost;
    }

    public float getMaxCost()
    {
        return maxCost;
    }

    public void setMaxCost(float cost)
    {
        maxCost = cost;
    }

    public int getProgress()
    {
        return progress;
    }

    public int getDistance()
    {
        return distance;
    }

    public Sprite[] getStatusSprites()
    {
        return StatusSprites;
    }

    public void useCost(float usage)
    {
        if (usage > cost)
        {
            Debug.Log("Usage cost is bigger than current cost");
            return;
        }
        cost -= usage;
    }

    public void Attack(ObjectMob mob)
    {
        ObjectPlayer player = PlayerManager.Inst().getPlayer();
        Skill? skill = SkillManager.Inst().findSkill(Combination);

        int damage = 0;
        for (int i = 0; i < Combination.Length; ++i)
        {
            switch (Combination[i])
            {
                case 'C':
                    damage += (int)(player.getAttack() * 0.5f);
                    break;
                case 'O':
                case 'H':
                    damage += (int)(player.getAttack() * 0.25f);
                    break;
                case 'S':
                case 'P':
                    damage += (int)(player.getAttack() * 0.75f);
                    break;
            }
        }
        if (skill.HasValue)
        {
            if (skill.Value.global) {
                var mobs = FindObjectsOfType<ObjectMob>();
                foreach ( var i in mobs ) {
                    if (Random.Range(0, 10) < mob.getLevel() - PlayerManager.Inst().getPlayer().getLevel())
                    {
                        Debug.Log(i.name + "does not take any damage");
                        continue; //회피
                    }
                    if ((int)(i.getType() + 1) % 3 == (int)skill.Value.type)
                    {
                        if (i.getStatusEffect() == StatusEffect.Burn)
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 1.5 * burnDamage));
                        else if (i.getStatusEffect() == StatusEffect.Frostbite)
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 1.5 * freezeDamage));
                        else
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 1.5));
                        Debug.Log(i.name + " is attacked and get " + (int)((skill.Value.damage + damage) * 1.5) + " damages");
                    }
                    else if ((int)(i.getType() + 2) % 3 == (int)skill.Value.type)
                    {
                        if (i.getStatusEffect() == StatusEffect.Burn)
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 0.5 * burnDamage));
                        else if (i.getStatusEffect() == StatusEffect.Frostbite)
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 0.5 * freezeDamage));
                        else
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 0.5));
                        Debug.Log(i.name + " is attacked and get " + (int)((skill.Value.damage + damage) * 0.5) + " damages");
                    }
                    else
                    {
                        if (i.getStatusEffect() == StatusEffect.Burn)
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * burnDamage));
                        else if (i.getStatusEffect() == StatusEffect.Frostbite)
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * freezeDamage));
                        else
                            i.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage)));
                    }
                    if (skill.Value.statusEffect != StatusEffect.None)
                    {
                        if (Random.Range(0, 20) < 1)
                        {
                            i.setStatusEffect(skill.Value.statusEffect);
                            i.setStatusRemainTurn(Random.Range(4, 8));
                            Debug.Log(i.name + " get " + skill.Value.statusEffect.ToString());
                        }
                    }
                }
                Debug.Log(mob.name + " is attacked and get " + damage.ToString() + " damages");
                foreach ( var i in mobs ) {
                    i.mobDead();
                }
            }
            else {
                if (Random.Range(0, 10) < mob.getLevel() - PlayerManager.Inst().getPlayer().getLevel())
                {
                    Debug.Log(mob.name + "does not take any damage");
                }
                else
                {
                    if ((int)(mob.getType() + 1) % 3 == (int)skill.Value.type)
                    {
                        if (mob.getStatusEffect() == StatusEffect.Burn)
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 1.5 * burnDamage));
                        else if (mob.getStatusEffect() == StatusEffect.Frostbite)
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 1.5 * freezeDamage));
                        else
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 1.5));
                        Debug.Log(mob.name + " is attacked and get " + (int)((skill.Value.damage + damage) * 1.5) + " damages");
                    }
                    else if ((int)(mob.getType() + 2) % 3 == (int)skill.Value.type)
                    {
                        if (mob.getStatusEffect() == StatusEffect.Burn)
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 0.5 * burnDamage));
                        else if (mob.getStatusEffect() == StatusEffect.Frostbite)
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 0.5 * freezeDamage));
                        else
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * 0.5));
                        Debug.Log(mob.name + " is attacked and get " + (int)((skill.Value.damage + damage) * 0.5) + " damages");
                    }
                    else
                    {
                        if (mob.getStatusEffect() == StatusEffect.Burn)
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * burnDamage));
                        else if (mob.getStatusEffect() == StatusEffect.Frostbite)
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage) * freezeDamage));
                        else
                            mob.GetDamaged((int)((player.getAttack() * skill.Value.damage + damage)));
                    }
                    if (skill.Value.statusEffect != StatusEffect.None)
                    {
                        if (Random.Range(0, 10) < 1)
                        {
                            mob.setStatusEffect(skill.Value.statusEffect);
                            mob.setStatusRemainTurn(Random.Range(4, 8));
                            Debug.Log(mob.name + " get " + skill.Value.statusEffect.ToString());
                        }
                    }
                    mob.mobDead();
                }
            }
        }
        else
        {
            if (Random.Range(0, 10) < mob.getLevel() - PlayerManager.Inst().getPlayer().getLevel())
            {
                Debug.Log(mob.name + "does not take any damage");
            }
            else
            {
                mob.GetDamaged(damage);
                Debug.Log(mob.name + " is attacked and get " + damage + " damages");
                mob.mobDead();
            }
        }
        SoundManager.Inst().playAudio(attack_sfx);
        while (PlayerManager.Inst().getPlayer().getEXP() >= PlayerManager.Inst().getPlayer().getEXPtoLevelUP())
        {
            PlayerManager.Inst().levelUp();
        }
        AttackReady = false;
        checkBattleState();
        if (!InBattle)
        {
            endBattle();
        }
        else {
            FindObjectOfType<ObjectPlayer>().getAnimator().SetTrigger("attack");
            turnOver();
        }
    }

    public void battleStart()
    {
        mob_number = Random.Range(1, 4);
        int mob_seed;

        InBattle = true;

        AttackReady = false;
        maxCost = cost = 1;

        InGameUIManager.Inst().costTextUpdate();

        enemies = new ObjectMob[mob_number];
        for (int i = 0; i < mob_number; ++i)
        {
            mob_seed = Random.Range(0, 2);
            GameObject mob = InGameUIManager.Inst().GenerateMob(mobPrefab[mob_seed], i);
            mob.GetComponent<ObjectMob>().setmobSeed(mob_seed);
            mob.name = "monster" + i.ToString();
            enemies[i] = mob.GetComponent<ObjectMob>();
            enemies[i].setLevel(Random.Range(villageNum * 10, villageNum * 10 + 10));
            enemies[i].getLVtext().text = "LV." + (enemies[i].getLevel()+1).ToString();
            int hp = 0, atk = 0, exp = 0;
            switch (mob_seed)
            {
                case 0:
                    hp = 1000;
                    atk = 100;
                    exp = 50;
                    break;
                case 1:
                    hp = 1250;
                    atk = 100;
                    exp = 55;
                    break;
            }
            for (int k = 0; k < enemies[i].getLevel(); ++k)
            {
                hp += (int)(enemies[i].getLevel() * 13 + enemies[i].getMaxHP() * 0.17 + 17);
                atk += (int)(enemies[i].getLevel() * 2.77 + enemies[i].getAttack() * 0.1);
                exp += (int)(enemies[i].getLevel() * 2.3);
            }
            enemies[i].setMaxHP(hp);
            enemies[i].setHP(hp);
            enemies[i].setAttack(atk);
            enemies[i].setEXP(exp);
        }

        InGameUIManager.Inst().HPbarUpdate();
    }

    public void endBattle()
    {
        Combination = "";
        battleTimer = 5.0f;
        Debug.Log("Battle is ended");
        ++progress;
        InGameUIManager.Inst().progressUpdate(progress, battleTimer);
        InGameUIManager.Inst().combinationTextUpdate();
        InGameUIManager.Inst().HPbarUpdate();
    }

    public void checkBattleState()
    {
        if (currentTurn == GameTurn.PLAYER)
        {
            if (mob_number <= 0)
                InBattle = false;
            else
                InBattle = true;
        }
        else
        {

        }
    }

    public void playerTurn()
    {
        if (!PlayerManager.Inst().getPlayer().isMoveable())
        {
            turnOver();
            return;
        }
        if (Combination.CompareTo("") != 0)
            AttackReady = true;
    }

    public void enemyTurn()
    {
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (!enemies[i].isMoveable())
                continue;
            if (enemies[i].getAttack() - PlayerManager.Inst().getPlayer().getDefend() > 0)
            {
                PlayerManager.Inst().getPlayer().GetDamaged(enemies[i].getAttack() - PlayerManager.Inst().getPlayer().getDefend());
                Debug.Log("Player get damaged");
            }
            else
            {
                PlayerManager.Inst().getPlayer().GetDamaged(10);
                Debug.Log("Player get damaged");
            }
            if (enemies[i].getStatus() != StatusEffect.None)
            {
                if (Random.Range(0, 20) < 1)
                {
                    PlayerManager.Inst().getPlayer().setStatusEffect(enemies[i].getStatus());
                    PlayerManager.Inst().getPlayer().setStatusRemainTurn(Random.Range(4, 8));
                }
            }
        }
        Combination = "";
        InGameUIManager.Inst().combinationTextUpdate();
        turnOver();
    }

    public bool isAttackReady()
    {
        return AttackReady;
    }

    public void turnOver()
    {
        if (currentTurn == GameTurn.PLAYER)
        {
            for (int i = 0; i < enemies.Length; ++i)
            {
                if (enemies[i].getStatusEffect() == StatusEffect.Corrosion)
                {
                    enemies[i].GetDamaged((int)(enemies[i].getMaxHP() * 0.05f));
                }
                if (enemies[i].getStatusEffect() == StatusEffect.Frostbite)
                {
                    enemies[i].setMoveable(!enemies[i].isMoveable());
                }
                if (enemies[i].getStatusEffect() != StatusEffect.None)
                {
                    enemies[i].setStatusRemainTurn(enemies[i].getStatusRemainTurn() - 1);
                    if (enemies[i].getStatusRemainTurn() <= 0)
                    {
                        enemies[i].setStatusEffect(StatusEffect.None);
                        enemies[i].setMoveable(true);
                    }
                }
                enemies[i].mobDead();
            }
            currentTurn = GameTurn.ENEMY;
            cost = ++maxCost;
            if (maxCost > 10)
                cost = maxCost = 10;
            InGameUIManager.Inst().costTextUpdate();
        }
        else
        {
            if (PlayerManager.Inst().getPlayer().getStatusEffect() == StatusEffect.Frostbite)
            {
                PlayerManager.Inst().getPlayer().setMoveable(!PlayerManager.Inst().getPlayer().isMoveable());
            }
            if (PlayerManager.Inst().getPlayer().getStatusEffect() != StatusEffect.None)
            {
                PlayerManager.Inst().getPlayer().setStatusRemainTurn(PlayerManager.Inst().getPlayer().getStatusRemainTurn() - 1);
                if (PlayerManager.Inst().getPlayer().getStatusRemainTurn() <= 0)
                {
                    PlayerManager.Inst().getPlayer().setStatusEffect(StatusEffect.None);
                    PlayerManager.Inst().getPlayer().setMoveable(true);
                }
            }
            currentTurn = GameTurn.PLAYER;
        }
        InGameUIManager.Inst().HPbarUpdate();
    }

    public void setMobNum(int num)
    {
        mob_number = num; ;
    }

    public int getMobNum()
    {
        return mob_number;
    }

    public bool isInBattle()
    {
        return InBattle;
    }
}