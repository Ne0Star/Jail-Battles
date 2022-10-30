using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EntityTrigger : Trigger
{
    public bool colliderMode;
    public List<EntityType> targetType;
    private void Awake()
    {
        if (targetType != null)
            targetType = targetType.DistinctBy(i => i.GetHashCode()).ToList();
    }

    public override void CustomUpdate()
    {

        List<Entity> searches = new List<Entity>();
        if (targetType != null)
            foreach (EntityType type in targetType)
            {
                searches.AddRange(LevelManager.Instance.EnemuManager.GetAllEntityByType(type));
            }

        foreach (Entity e in searches)
        {
            float distance = Vector2.Distance(e.Agent.transform.position, transform.position);
            if (colliderMode)
            {
                if (distance <= radius + e.Agent.radius)
                {
                    onStay?.Invoke(e);
                }
                if (Vector2.Distance(LevelManager.Instance.Player.Agent.transform.position, transform.position) <= radius + LevelManager.Instance.Player.Agent.radius)
                {
                    onStay?.Invoke(LevelManager.Instance.Player);
                }
            }
            else
            {
                if (distance <= radius)
                {
                    onStay?.Invoke(e);
                }
                if (Vector2.Distance(LevelManager.Instance.Player.Agent.transform.position, transform.position) <= radius)
                {
                    onStay?.Invoke(LevelManager.Instance.Player);
                }
            }
        }
    }
}
