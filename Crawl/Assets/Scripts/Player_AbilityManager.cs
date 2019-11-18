﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AbilityManager : Singleton<Player_AbilityManager>
{
    float realAttack;
    [SerializeField] [Range(0, 100)] float attack;
    [SerializeField] [Range(0, 100)] float defense;
    [SerializeField] [Range(0, 100)] float avoidance;
    [SerializeField] [Range(0, 100)] float critical_Percentage;
    [SerializeField] [Range(100, 1000)] float critical_Hit;
    [SerializeField] [Range(0, 100)] float attackRange;
    [SerializeField] float attackSpeed = 1;
    [SerializeField] [Range(0, 1000)] float moveSpeed;
    [SerializeField] [Range(0, 5)] int targetNum;
    [SerializeField] float reflectDamage;
    float drain;
    float maxDrain = 30;
    int maxTargetNum = 5;
    float maxAttackSpeed = 5;
    float maxAvoidance = 80.0f;
    [SerializeField] float HP;
    [SerializeField] float stamina;
    float maxHP = 1000;
    float maxStamina = 1000;
    float rebirthHP;
    float rebirth_hp_Percent = 0.2f;
    Player_Attack_Range player_Attack_Range;
    bool isCritical = false;
    bool isRebirth;
    Player_Booster player_Booster;
    Player_Shield player_Shield;
    Player_Invincibility player_Invincibility;
    private void Start()
    {
        player_Attack_Range = GetComponentInChildren<Player_Attack_Range>();
        player_Booster = GetComponent<Player_Booster>();
        player_Shield = GetComponent<Player_Shield>();
        player_Invincibility = GetComponent<Player_Invincibility>();
    }

    private void Update()
    {
        moveSpeed += Time.deltaTime;
        rebirthHP = maxHP * rebirth_hp_Percent;
    }
    public void Critical(float time)
    {
        StartCoroutine(CriticalHit(time));
    }
    IEnumerator CriticalHit(float time)
    {
        isCritical = true;
        yield return new WaitForSeconds(time);
        isCritical = false;
    }   
    public void Drain(float realAttack)
    {
        HP += realAttack * (drain / 100);
        if (HP > maxHP)
            HP = maxHP;
    }
    //Get함수
    public bool GetIsCritical()
    {
        return isCritical;
    }
    public float GetMaxHP()
    {
        return maxHP;
    }
    public float GetMaxStamina()
    {
        return maxStamina;
    }
    public float GetHP()
    {
        return HP;
    }
    public float GetStamina()
    {
        return stamina;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
    public float GetAttackRange()
    {
        return player_Attack_Range.GetAttackRange();
    }
    public float GetCritical_HIt()
    {
        return critical_Hit;
    }
    public float GetCritical_Percentage()
    {
        return critical_Percentage;
    }
    public float GetAvoidance()
    {
        return avoidance;
    }
    public float GetDefense()
    {
        return defense;
    }
    public float GetAttack()
    {
        return attack;
    }
    public int GetTargetNum()
    {
        return targetNum;
    }
    public float GetDrain()
    {
        return drain;
    }
    public float GetReflectDamage()
    {
        return reflectDamage;
    }
    //Set함수
    public void IncreaseMaxHP(float n)
    {
        HP += n;
        maxHP += n;
    }
    public void IncreaseHP(float n)
    {
        HP += n;
        if (HP >= maxHP)
            HP = maxHP;      
    }
    public void SetHP(float n)
    {
        HP = n;
        if (HP >= maxHP)
            HP = maxHP;
        else if (HP < 0)
            HP = 0;
    }
    public void SetStamina(float n)
    {
        stamina = n;
        if (stamina >= maxStamina)
            stamina = maxStamina;
        else if (stamina < 0)
            stamina = 0;
    }

    public float DecreaseHP(float n)
    {
        if (player_Booster.GetOnBooster())
            return 0;
        if (player_Invincibility.GetIsInvincible())
            return 0;
        if(player_Shield.GetShieldCount() > 0)
        {
            player_Shield.Shield();
            return 0;
        }
        float rand = Random.Range(0, 100);
        if(rand <= avoidance)
        {
            return 0; //빗나감
        }
        else
        {
            HP -= (n - defense);
            if (HP <= 0)
            {
                if (isRebirth)
                {
                    StartCoroutine(Rebirth());
                }
                else
                {
                    HP = 0;
                }
            }
            return n - defense;
        }
      
    }
    IEnumerator Rebirth()
    {
        yield return new WaitForSeconds(1.0f);
        HP = rebirthHP;
        isRebirth = false;
    }
    public void SetIsRebirth(bool temp)
    {
        isRebirth = temp;
    }
    public void Increase_RebirthHp_Percent(float n)
    {
        rebirth_hp_Percent += n;
        if (rebirth_hp_Percent >= 1)
            rebirth_hp_Percent = 1;
    }
    public void IncreaseMaxStamina(float n)
    {
        stamina += n;
        maxStamina += n;
    }
    public void IncreaseStamina(float n)
    {
        stamina += n;
        if (stamina >= maxStamina)
            stamina = maxStamina;
    }
    public void DecreseStamina(float n)
    {
        stamina -= n;
        if (stamina < 0)
            stamina = 0;
    }
    public void SetTargetNum(int n)
    {
        targetNum = n;
        if (targetNum > maxTargetNum)
            targetNum = maxTargetNum;
    }
    public void DecreaseMoveSpeed(float n)
    {
        moveSpeed -= n;
        if (moveSpeed <= 300.0f)
            moveSpeed = 300.0f;
    }
    public void IncreaseAttackSpeed(float n)
    {
        attackSpeed += n;
        if (attackSpeed > maxAttackSpeed)
            attackSpeed = maxAttackSpeed;
    }
    public void IncreaseAttackRange(float n)
    {
        player_Attack_Range.IncreaseAttackRange(n);
    }
    public void IncreaseCritical_HIt(float n)
    {
        critical_Hit += n;
    }
    public void IncreaseCritical_Percentage(float n)
    {
        critical_Percentage += n;
    }
    public void increaseAvoidance(float n)
    {
        avoidance += n;
        if (avoidance >= maxAvoidance)
            avoidance = maxAvoidance;
    }
    public void IncreaseDefense(float n)
    {
        defense += n;
    }
    public void IncreaseAttack(float n)
    {
        attack += n;
    }
    public void IncreaseTargetNum()
    {
        targetNum++;
        if (targetNum > maxTargetNum)
            targetNum = maxTargetNum;
    }
    public void IncreaseDrain(float n)
    {
        drain += n;
        if (drain > maxDrain)
            drain = maxDrain;
    }
    public void IncreaseReflectDamage(float n)
    {
        reflectDamage += n;
        if (reflectDamage >= 100)
            reflectDamage = 100.0f;
    }

    public void InitializeMoveSpeed()
    {
        moveSpeed = 400;
    }
}
