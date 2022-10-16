using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningAI : AI, IAI
{
    public void DebugCheck()
    {

    }
    //[SerializeField] private AIConsistency consistency;
    //public bool mark;
    protected override void UpdateAI()
    {
        //if (!free)
        //{
        //    return;
        //}
        //mark = !mark;
        //consistency = LevelManager.Instance.AiManager.GetConsistency(this, true);
        //if (consistency != null && consistency.Free)
        //{
        //    //consistency.Free = false;
        //    free = false;
        //    consistency.StartConsisstency(this, () => { free = true; });
        //}
        //else
        //{
        //    consistency = null;
        //}
    }
    public void Initial()
    {

    }
}
