using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{


    [SerializeField] private bool isBegin = false;
    [SerializeField] private bool isIdle = false;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool isHealth = false;
    public bool IsBegin { get => isBegin; }
    public bool IsAttack { get => isAttack; }
    public bool IsIdle { get => isIdle; }
    public bool IsHealth { get => isHealth; }


    public void SetFraction(Color color, string name)
    {
        StandartBar bar = (StandartBar)HitBar;
        bar.SetFractionColor(color);
    }

    private void Heal()
    {
        if (isHealth) return;
        if (Random.Range(0, 100) >= (100 - LevelManager.Instance.LevelData.HealChance))
        {
            isHealth = true;
            GetHealth health = new GetHealth(this);
            health.OnComplete?.AddListener((a) =>
            {
                isHealth = false;
            });
            SetAction(health);
        }
    }
    // 200 20
    protected override void Attacked(Entity attacker)
    {
        if (HitBar.Health <= HitBar.GetMaxHealth() / 2 && !isBegin)
        {
            isBegin = Random.Range(0, 100) >= ((100 - LevelManager.Instance.LevelData.BeginChance) + (HitBar.Health / 100));
            if (isBegin)
            {
                agent.speed = Mathf.Clamp(agent.speed + 1f, -MoveSpeed.MaxValue, MoveSpeed.MaxValue);
                BeginAction ba = new BeginAction(this, (Enemu)attacker, LevelManager.Instance.GetAreas(AreaType.Столовая), LevelManager.Instance.LevelData.ExitDistance);
                ba.OnComplete?.AddListener((a) =>
                {
                    isBegin = false;
                    Heal();
                });
                SetAction(ba);
            }
            return;
        }
        if(!isHealth)
        StartAttack(attacker);
    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        if (isBegin || isHealth) return;
        if (e != LevelManager.Instance.Player)
        {
            bool agro = Random.Range(0, 100) >= (100 - LevelManager.Instance.LevelData.Pursurehance);
            if (!agro) return;
        }
        StartAttack(e);
    }

    protected override void Disable()
    {
        currentAction = null;
        stackActions = new List<AIAction>();
        lifeActions = new List<AIAction>();
        target = null;
        isBegin = false;
        isIdle = false;
        isAttack = false;
        isHealth = false;
    }

    private void SetLife()
    {
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new IdleAction(this, Random.Range(0.5F,2F), LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
    }

    public override void Attack()
    {
        if (target && weapon)
        {
            YG.WeaponData weaponData = new YG.WeaponData();
            foreach (YG.WeaponData m in GameManager.Instance.EnemiesDefaultWeaponData)
            {
                if (m.weaponType == weapon.WeaponType)
                {
                    weaponData = m;
                    break;
                }
            }
            target.TakeDamage(this, weaponData.attackDamage.Value, () =>
            {

            });
            weapon.AnimateAttack();
        }
    }

    protected override void Enabled()
    {
        stackActions = new List<AIAction>();
        lifeActions = new List<AIAction>();
        Gun r = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Gun>();
        SetGun(r);
        Mele re = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Mele>();
        SetMele(re);

        SetLife();
    }

    private void StartAttack(Entity e)
    {
        if (isBegin || isIdle || this.target != null || this == e) return;

        if (e as Convict)
        {
            Convict enemu = (Convict)e;
            if (enemu.isBegin || (enemu.Target && (enemu.Target.gameObject.activeSelf && enemu.Target != this)) || enemu.isHealth)
            {
                return;
            }
        }

        isAttack = true;
        this.target = e;
        AttackTarget attackAction = new AttackTarget(this, ref target, true);
        attackAction.OnComplete?.AddListener((a) =>
        {
            target = null;
            isAttack = false;
            Heal();
        });
        SetAction(attackAction);
    }

    //protected override void OnCustomTriggerStay(Entity e)
    //{
    //    if (agroBlock) return;

    //    if (e && e as Convict)
    //    {
    //        Convict c = (Convict)e;
    //        Если у него есть цель
    //        if (c.Target && c.Target.gameObject.activeSelf)
    //        {
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //    e.TakeDamage(this, 0f, () => { });
    //    StartAttack(e);
    //}
}
