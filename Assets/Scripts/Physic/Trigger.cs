using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour, ICustomListItem
{

    [SerializeField] protected UnityEvent<Entity> onStay;
    [SerializeField] protected float radius;
    /// <summary>
    /// Те кого нашёл триггер
    /// </summary>
    [SerializeField] private List<Entity> visitors;

    public UnityEvent<Entity> OnStay { get => onStay; }
    public float Radius { get => radius; set => radius = value; }

    public abstract void CustomUpdate();


    private void OnEnable()
    {
        LevelManager.Instance.TriggerManager.Register(this);
    }

    private void OnDisable()
    {
        LevelManager.Instance.TriggerManager.UnRegister(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
