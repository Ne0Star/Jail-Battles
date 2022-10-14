using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningAI : AI, IAI
{

    [SerializeField] private Transform bucket;

    public void DebugCheck()
    {

    }
    [SerializeField] private AIConsistency consistency;
    public bool mark;
    public override void CustomUpdate()
    {
        if (!free)
        {
            return;
        }
        mark = !mark;
        consistency = LevelManager.Instance.AiManager.GetConsistency(this, true);
        if (consistency != null && consistency.Free)
        {
            //consistency.Free = false;
            free = false;
            consistency.StartConsisstency(this, () => { free = true; });
        }
        else
        {
            consistency = null;
        }
    }
    public void Initial()
    {

    }
}
