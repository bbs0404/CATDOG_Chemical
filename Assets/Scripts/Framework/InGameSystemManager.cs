using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

enum GameTurn
{
    PLAYER,
    ENEMY
}

public class InGameSystemManager : SingletonBehaviour<InGameSystemManager>
{
    private float cost;
    private float maxCost;
    [SerializeField]
    private string Combination = "";
    private int villageNum; //마을의 넘버
    private int progress = 0; //진행한 미터
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

    void Start()
    {
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
            if (progress >= 5) {
                GameStateManager.Inst().setState(State.END);
                InGameUIManager.Inst().resultTextUpdate();
                InGameUIManager.Inst().OnStateChanged(State.END);
            }
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
        Skill? skill = SkillManager.Inst().findSkill(Combination);

        int damage = 0;
        for (int i = 0; i < Combination.Length; ++i)
        {
            switch (Combination[i])
            {
                case 'C':
                case 'O':
                case 'H':
                    damage += 10;
                    break;
                case 'S':
                case 'P':
                    damage += 20;
                    break;
            }
        }
        if (skill.HasValue)
        {
            if (skill.Value.global) {
                var mobs = FindObjectsOfType<ObjectMob>();
                foreach ( var i in mobs ) {
                    i.GetDamaged(skill.Value.damage);
                    Debug.Log(i.name + " is attacked and get " + skill.Value.damage.ToString() + " damages");
                }
                mob.GetDamaged(damage);
                Debug.Log(mob.name + " is attacked and get " + damage.ToString() + " damages");
                foreach ( var i in mobs ) {
                    i.mobDead();
                }
            }
            else {
                mob.GetDamaged(skill.Value.damage + damage);
                Debug.Log(mob.name + " is attacked and get " + (skill.Value.damage + damage).ToString() + " damages");
                mob.mobDead();
            }
        }
        else
        {
            mob.GetDamaged(damage);
            Debug.Log(mob.name + " is attacked and get " + damage.ToString() + " damages");
            mob.mobDead();
        }
        SoundManager.Inst().playAudio(attack_sfx);
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
            mob.name = "monster" + i.ToString();
            enemies[i] = mob.GetComponent<ObjectMob>();            
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
        if (Combination.CompareTo("") != 0)
            AttackReady = true;
    }

    public void enemyTurn()
    {
        for (int i = 0; i < enemies.Length; ++i) {
            if (enemies[i].getAttack() - PlayerManager.Inst().getPlayer().getDefend() > 0)
                PlayerManager.Inst().getPlayer().GetDamaged(enemies[i].getAttack() - PlayerManager.Inst().getPlayer().getDefend());
            else
                PlayerManager.Inst().getPlayer().GetDamaged(10);
            waitSecond = 1.0f;
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
            currentTurn = GameTurn.ENEMY;
            cost = ++maxCost;
            if (maxCost > 10)
                cost = maxCost = 10;
            InGameUIManager.Inst().costTextUpdate();
        }
        else
            currentTurn = GameTurn.PLAYER;
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