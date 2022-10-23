using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour
{

    [SerializeField] protected UnityEvent<Entity> onStay;
    [SerializeField] protected float radius;
    /// <summary>
    /// Те кого нашёл триггер
    /// </summary>
    [SerializeField] private List<Entity> visitors;

    public UnityEvent<Entity> OnStay { get => onStay; }
    protected float Radius { get => radius; }

    public abstract void CustomUpdate();

    //private void Awake()
    //{
    //    LevelManager.Instance.ColliderManager.Register(this);
    //    onEnter?.AddListener(() =>
    //    {
    //        Debug.Log("Кто то вошёл");
    //    });
    //}
    //[SerializeField] private List<Entity> targets;
    //public override void CustomUpdate()
    //{
    //    List<Entity> res = new List<Entity>();
    //    for (int i = 0; i < searchTypes.AiTypes.Count; i++)
    //    {
    //        res.AddRange(LevelManager.Instance.AiManager.GetAllEntityByAI(searchTypes.AiTypes[i].Type));
    //    }
    //    targets = res;

    //    foreach(Entity target in targets)
    //    {
    //        if(target && target != Entit)
    //        if(Vector2.Distance(target.transform.position, transform.position) <= radius)
    //        {
    //            Debug.Log("В моём радиусе цель: " + target.name);
    //        }
    //    }

    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
