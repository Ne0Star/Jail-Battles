using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemu : Entity, ICustomListItem
{
    [SerializeField] private AI ai;
    [SerializeField] private float currentTime;
    public AI Ai { get => ai; }

    public override void MarkTarget(Entity source)
    {
        ai.MarkTarget(source);
    }

    protected override void Enable()
    {
        if (agent)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        ai.SetPresset(this);

        //ChangeAi();
    }

    public void CustomUpdate()
    {
        if (!gameObject.activeSelf)
        {
            if (currentTime >= LevelManager.Instance.GetColorByRange(ai.UpdateCount).respawnTime)
            {
                gameObject.SetActive(true);
                currentTime = 0f;
            }
            currentTime += 0.02f;
            return;
        }
        ai.CustomUpdate();
    }

    //public AIType AiType { get => aiType; }


    //[SerializeField] private AIUniversalData data;
    //[SerializeField] private AITypes targetTypes;
    //[SerializeField] private AIType aiType;

    //protected void ChangeAi()
    //{
    //    if (ai != null)
    //        LevelManager.Instance.AiManager.ChangeAI(this, ref ai, data, aiType, targetTypes);
    //    else if (ai == null) ai = LevelManager.Instance.AiManager.AddAI(this, data, aiType, targetTypes);
    //}

    //public virtual void CustomUpdate()
    //{
    //    current = (int)aiType;
    //    if (last != current)
    //    {

    //        ChangeAi();

    //        last = current;
    //    }
    //    if (ai)
    //    {
    //        ai.CustomUpdate();
    //    }
    //}

    //int last, current;
}
