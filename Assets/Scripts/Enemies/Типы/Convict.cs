using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private float gunChance = 10f, machineChance = 5f, meleChance = 20f, noneChance = 50f;

    [SerializeField] private float exitDistance;
    [SerializeField] private Entity target;
    [SerializeField] public Entity pursuer;
    //[SerializeField] private WeaponType initialWeapon;


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (weapon)
    //        {
    //            weapon.gameObject.SetActive(false);
    //            weapon = null;
    //        }
    //        Enabled();
    //        //TakeDamage(this, 50f, () =>
    //        //{

    //        //});
    //    }
    //}

    private void SetLife()
    {
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
    }

    public override void Attack()
    {
        if (target && weapon)
        {
            target.TakeDamage(this, weapon.AttackDamage, () =>
            {

            });
            weapon.AnimateAttack();
        }
    }

    protected override void Disable()
    {
        stackActions.Clear();
        currentAction = null;
        lifeActions.Clear();
        target = null;
    }

    protected override void Enabled()
    {
        bool da = false;
        //Weapon w = null;


        //da = Random.Range(0, 100) >= 100 - machineChance;
        //if (da)
        //{
        //    w = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(WeaponType.Machine, false);
        //    SetWeapon(w);
        //    SetLife();
        //    return;
        //}
        da = Random.Range(0, 100) >= 100 - gunChance;
        if (da)
        {
            Gun result = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Gun>();
            SetGun(result);
            SetLife();
        }
        da = Random.Range(0, 100) >= 100 - meleChance;
        if (da)
        {
            Mele result = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Mele>();
            SetMele(result);
            SetLife();
        }
        //da = Random.Range(0, 100) >= 100 - noneChance;
        //if (da)
        //{
        //    w = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(WeaponType.None, false);
        //    SetWeapon(w);
        //    SetLife();
        //    return;
        //}





    }

    private bool attack = false;

    public Entity Target { get => target; }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(agent.transform.position, exitDistance);
    //    if (weapon)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(weapon.transform.position, weapon.AttackDistance);
    //    }
    //}

    protected override void Attacked(Entity attacker)
    {
        OnCustomTriggerStay(attacker);
    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        //if (e == this || target) return;

        //if (e && e as Convict)
        //{
        //    Convict c = (Convict)e;
        //    if (c.Target && c.Target.gameObject.activeSelf) return;
        //    if (c.pursuer && c.pursuer.gameObject.activeSelf) return;
        //    c.pursuer = this;
        //}
        if (e == this || target == e || (target && target.gameObject.activeInHierarchy) || !weapon) return;


        //if (target && target.gameObject.activeSelf && target as Convict)
        //{
        //    if (((Convict)target).Target && ((Convict)target).Target != this && ((Convict)target).Target.gameObject.activeSelf) return;
        //}
 //  || target
        this.target = e;

        AttackTarget attackAction = new AttackTarget(this, ref target, exitDistance, true);
        attack = true;
        target.TakeDamage(this, 0f, () => { });
        attackAction.OnComplete?.AddListener((a) =>
        {
            target = null;
            attack = false;
        });
        attackAction.OnBreak?.AddListener((a) =>
        {
            target = null;
            attack = false;
        });
        SetAction(attackAction);
    }
}
