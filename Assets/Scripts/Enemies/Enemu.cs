using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemu : Entity
{
    private bool check = false;
    public bool Check { get => check; set => check = value; }
    public AIType AiType { get => aiType; }
    [SerializeField] private AIPressset aiPresset;
    [SerializeField] private AIType aiType;
    [SerializeField] protected AI ai;


    protected override void Enable()
    {
        if (agent)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        ChangeAi();
    }

    public virtual void EnemuUpdate()
    {
        current = (int)aiType;
        if (last != current)
        {

            ChangeAi();

            last = current;
        }
        if (ai)
        {
            if(ai.Free)
            ai.CustomUpdate();
        }
    }

    protected void ChangeAi()
    {
        if (ai != null)
            LevelManager.Instance.AiManager.ChangeAI(this, ref ai, aiPresset, aiType);
        else if (ai == null) ai = LevelManager.Instance.AiManager.AddAI(this, aiPresset, aiType);
    }

    int last, current;
}
