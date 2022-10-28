using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrigger : Trigger
{
    [SerializeField] private bool customTypes = false;
    [SerializeField] private bool colliderMode = true;
    [SerializeField] private Entity sources;
    [SerializeField] private AITypes types;
    [SerializeField] private bool initialized = false;

    [SerializeField] private bool mark;

    private void Awake()
    {
        LevelManager.Instance.TriggerManager.Register(this);
    }

    [SerializeField] private List<Entity> targets;
    public override void CustomUpdate()
    {
        if (!initialized) return;

        mark = !mark;

        List<Entity> res = new List<Entity>();
        for (int i = 0; i < types.AiTypes.Count; i++)
        {
            res.AddRange(LevelManager.Instance.AiManager.GetAllEntityByAI(types.AiTypes[i].Type));
        }
        targets = res;

        foreach (Entity target in targets)
        {
            if (target && target != sources && target != this && target.gameObject.activeInHierarchy)
            {
                if (!colliderMode)
                {
                    if (Vector2.Distance(target.transform.position, transform.position) <= radius + 0.01f)
                    {
                        onStay.Invoke(target);
                        //Debug.Log("В моём радиусе цель: " + target.name);
                    }
                }
                else
                {
                    if (Vector2.Distance(target.Agent.transform.position, sources.Agent.transform.position) <= (sources.Agent.radius + target.Agent.radius + 0.01f))
                    {
                        onStay.Invoke(target);
                        //Debug.Log("В моём радиусе цель: " + target.name);
                    }
                }
            }

        }
    }

    public void SetAi(AI sources)
    {
        if (!sources) return;
        if (!customTypes)
            this.types = sources.TargetTypes;
        this.sources = sources.Entity;
        initialized = true;
    }
}
