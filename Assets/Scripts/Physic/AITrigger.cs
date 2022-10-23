using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrigger : Trigger
{
    [SerializeField] private AI sources;
    [SerializeField] private AITypes types;
    [SerializeField] private bool initialized = false;



    private void Awake()
    {
        LevelManager.Instance.TriggerManager.Register(this);
    }

    [SerializeField] private List<Entity> targets;
    public override void CustomUpdate()
    {
        if (!initialized) return;

        List<Entity> res = new List<Entity>();
        for (int i = 0; i < types.AiTypes.Count; i++)
        {
            res.AddRange(LevelManager.Instance.AiManager.GetAllEntityByAI(types.AiTypes[i].Type));
        }
        targets = res;

        foreach (Entity target in targets)
        {
            if (target  && target != sources.Entity && target != this && target.gameObject.activeInHierarchy)
                if (Vector2.Distance(target.transform.position, transform.position) <= radius)
                {
                    onStay.Invoke(target);
                    //Debug.Log("В моём радиусе цель: " + target.name);
                }
        }
    }

    public void SetAi(AI sources)
    {
        if (!sources) return;
        this.types = sources.TargetTypes;
        this.sources = sources;
        initialized = true;
    }
}
