using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : Enemu
{
    [SerializeField] private float cleaningDuration;
    [SerializeField] private float cleaningTime;
    [SerializeField] private float exitDistance;
    [SerializeField] private Trash currentTrash;

    [SerializeField] private float time = 0f;

    protected override void Create()
    {

    }
    protected override void Enabled()
    {
        List<AIArea> areas = LevelManager.Instance.GetAreas(AreaType.Кухня);
        areas.AddRange(LevelManager.Instance.GetAreas(AreaType.Столовая));
        AddAction(new MoveFromArea(this, areas));
        AddAction(new MoveFromArea(this, areas));
        //AddAction(new MoveFromArea(this, ));
    }

    protected override void Disable()
    {


    }

    protected override void OnUpdate()
    {
        if (time >= cleaningTime)
        {
            if (currentTrash == null)
            {
                currentTrash = LevelManager.Instance.TrashManager.GetFreeActiveTrash();
                if (currentTrash)
                {
                    Cleaning cl = new Cleaning(this, currentTrash, cleaningTime);
                    cl.OnComplete.AddListener((a) =>
                    {
                        currentTrash = null;
                    });
                    currentTrash.SetFree(false);
                    SetAction(cl);
                }
            }

            ////currentTrash = LevelManager.Instance.CleanersManager.GetRandomActiveTrash();
            //if (currentTrash)
            //{
            //    MoveToTarget action = new MoveToTarget(this, currentTrash.transform);
            //    action.OnComplete?.AddListener((a) =>
            //    {
            //        Cleaning act = new Cleaning(this, currentTrash, cleaningDuration);
            //        act.OnComplete?.AddListener((a) =>
            //        {
            //            currentTrash = null;
            //        });
            //        SetAction(act);
            //    });
            //    SetAction(action);
            //} else
            //{
            //    currentTrash = LevelManager.Instance.TrashManager.GetFreeTrash();
            //}
            time = 0f;
        }
        time += Time.unscaledDeltaTime;
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
