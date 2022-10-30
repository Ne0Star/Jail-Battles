using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private Entity target;

    protected override void Enabled()
    {
        SetWeapon(null);
        animator.Play("�����");
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.��������)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.��������)));
    }
    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e == this || e == target) return;
        this.target = e;
        MoveToTarget moveToTarget = new MoveToTarget(this, e, 4f);
        moveToTarget.OnBreak?.AddListener((a) =>
        {
            this.target = null;
        });
        moveToTarget.OnComplete?.AddListener((a) =>
        {
            this.target = null;
        });
        SetAction(moveToTarget);
    }
}
