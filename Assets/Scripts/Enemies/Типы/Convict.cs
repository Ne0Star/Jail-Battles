using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private float exitDistance;
    [SerializeField] private Entity target;
    [SerializeField] private WeaponType initialWeapon;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(this, 50f, () => {

            });
        }
    }

    protected override void Enabled()
    {
        Weapon w = LevelManager.Instance.WeaponManager.GetRandomWeaponByType(initialWeapon, false);
        SetWeapon(w);



        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
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
