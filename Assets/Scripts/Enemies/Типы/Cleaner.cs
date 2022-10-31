using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : Enemu
{
    [SerializeField] private float cleaningTime;
    [SerializeField] private float exitDistance;
    [SerializeField] private Trash currentTrash;
    [SerializeField] private Entity target;

    private float currentTime = 0f;

    protected override void Enabled()
    {
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Кухня)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.Столовая)));
    }

    protected override void OnUpdate()
    {
        if (currentTrash != null)
        {

            return;
        }
        if (currentTime >= cleaningTime)
        {
            currentTrash = LevelManager.Instance.CleanersManager.GetRandomActiveTrash();
            if (currentTrash)
            {
                MoveToTarget action = new MoveToTarget(this, currentTrash.transform);
                action.OnComplete?.AddListener((a) =>
                {

                    Cleaning act = new Cleaning(this, currentTrash);
                    act.OnComplete?.AddListener((a) =>
                    {
                        currentTrash = null;
                    });
                    SetAction(act);
                });
                SetAction(action);
            }
            currentTime = 0f;
        }
        currentTime += Time.unscaledDeltaTime;
    }

    //protected override void OnCustomTriggerStay(Entity e)
    //{
    //    if (e == this || target == e || target) return;
    //    this.target = e;


    //    AttackTarget attackAction = new AttackTarget(this, target, weapon, exitDistance, false);
    //    attackAction.OnComplete?.AddListener((a) =>
    //    {
    //        target = null;
    //    });
    //    attackAction.OnBreak?.AddListener((a) =>
    //    {
    //        target = null;
    //    });
    //    SetAction(attackAction);
    //}
}
