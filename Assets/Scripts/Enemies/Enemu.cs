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

    private void OnEnable()
    {
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
    }

    protected void ChangeAi()
    {
        if (ai != null)
            LevelManager.Instance.AiManager.ChangeAI(ref ai, aiPresset, aiType);
        else if (ai == null) ai = LevelManager.Instance.AiManager.AddAI(transform, aiPresset, aiType);
    }

    int last, current;
    [SerializeField] private int on, tw;

    [SerializeField] private bool oneContainsTwo = false;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        on = (int)aiType + 1;
        tw = (int)aiPresset.TargetTypes;

        if (aiPresset.TargetTypes.HasFlag((AITypes)System.Enum.Parse(typeof(AITypes), aiType.ToString())))
        {
            oneContainsTwo = true;
        }

    }
#endif
}
