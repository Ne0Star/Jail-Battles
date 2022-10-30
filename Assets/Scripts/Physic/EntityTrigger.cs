using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTrigger : Trigger
{
    [SerializeField] private bool colliderMode;




    public override void CustomUpdate()
    {
        foreach (Enemu e in LevelManager.Instance.EnemuManager.AllEnemies)
        {
            float distance = Vector2.Distance(e.Agent.transform.position, transform.position);
            if (colliderMode)
            {
                if (distance <= radius + e.Agent.radius)
                {
                    onStay?.Invoke(e);
                }
                if(Vector2.Distance(LevelManager.Instance.Player.Agent.transform.position, transform.position) <= radius + LevelManager.Instance.Player.Agent.radius)
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
