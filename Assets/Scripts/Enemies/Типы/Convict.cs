using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private float gunChance = 10f, machineChance = 5f, meleChance = 20f, noneChance = 50f;

    [SerializeField] private float exitDistance;
    [SerializeField] private Entity target;
    //[SerializeField] private WeaponType initialWeapon;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Enabled();
            //TakeDamage(this, 50f, () =>
            //{

            //});
        }
    }

    private void SetLife()
    {
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
    }

    public override void Attack()
    {
        target.TakeDamage(this, weapon.AttackDamage, () =>
        {

        });
    }

    protected override void Enabled()
    {
        bool da = false;
        Weapon w = null;
        da = Random.Range(0, 100) >= 100 - noneChance;
        if (da)
        {
            w = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(WeaponType.None, false);
            SetWeapon(w);
            SetLife();
            return;
        }
        da = Random.Range(0, 100) >= 100 - meleChance;
        if (da)
        {
            w = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(WeaponType.Mele, false);
            SetWeapon(w);
            SetLife();
            return;
        }
        da = Random.Range(0, 100) >= 100 - gunChance;
        if (da)
        {
            w = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(WeaponType.Gun, false);
            SetWeapon(w);
            SetLife();
            return;
        }
        da = Random.Range(0, 100) >= 100 - machineChance;
        if (da)
        {
            w = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(WeaponType.Machine, false);
            SetWeapon(w);
            SetLife();
            return;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(agent.transform.position, exitDistance);
        if (weapon)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(weapon.transform.position, weapon.AttackDistance);
        }
    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e == this || target == e || target || !weapon) return;
        this.target = e;


        AttackTarget attackAction = new AttackTarget(this, target, exitDistance, false);
        attackAction.OnComplete?.AddListener((a) =>
        {
            target = null;
        });
        attackAction.OnBreak?.AddListener((a) =>
        {
            target = null;
        });
        SetAction(attackAction);
    }
}
