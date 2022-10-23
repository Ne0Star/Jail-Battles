﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemu : Entity
{
    private bool check = false;
    public bool Check { get => check; set => check = value; }
    public AIType AiType { get => aiType; }
    [SerializeField] private AIUniversalData data;
    [SerializeField] private AITypes targetTypes;
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
            ai.CustomUpdate();
        }
    }

    protected void ChangeAi()
    {
        if (ai != null)
            LevelManager.Instance.AiManager.ChangeAI(this, ref ai, data, aiType, targetTypes);
        else if (ai == null) ai = LevelManager.Instance.AiManager.AddAI(this, data, aiType, targetTypes);
    }

    int last, current;
}
