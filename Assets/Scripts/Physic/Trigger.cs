using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    [SerializeField] protected float triggerRadius = 0f;
    [SerializeField] protected EntityType type;

    public float TriggerRadius { get => triggerRadius; set => triggerRadius = value; }
    public void SetRange(float range) => triggerRadius = range;

    protected Entity[] GetAllEntities()
    {
        Entity[] result = null;
        switch (type)
        {
            case EntityType.Enemu:
                return null; // LevelManager.Instance.EnemuManager.Batches.ToArray();
        }
        return result;
    }

    ///// <summary>
    ///// Возвращает все сущности в радиусе
    ///// </summary>
    ///// <returns></returns>
    public virtual List<Entity> GetAllRadius()
    {
        List<Entity> result = new List<Entity>();
        //int t = 0;
        EnemuManager em = LevelManager.Instance.EnemuManager;
        for (int i = 0; i < GetAllEntities().Length; i++)
        {
            if (GetAllEntities()[i] && GetAllEntities()[i].gameObject.activeInHierarchy)
            {
                if (Vector2.Distance(transform.position, GetAllEntities()[i].transform.position) < triggerRadius)
                {
                    result.Add(GetAllEntities()[i]);
                }

                //if (t >= maxSearchCount)
                //{
                //    return result;
                //}
                //t++;
            }
        }
        return result;
    }

    /// <summary>
    /// Возвращает ближайшую сущность, из последних доавленных в массив, в пределах индекса
    /// </summary>
    /// <param name="serachCount"></param>
    /// <returns></returns>
    public virtual Entity GetOneNear(int searchCount, bool inverse)
    {
        float max = float.MaxValue;
        int t = 0;
        Entity result = null;

        EnemuManager em = LevelManager.Instance.EnemuManager;
        for (int i = inverse ? GetAllEntities().Length - 1 : 0; inverse ? (i > 0) : (i < GetAllEntities().Length); i = inverse ? (i - 1) : (i + 1))
        {
            if (GetAllEntities()[i] && GetAllEntities()[i].gameObject.activeInHierarchy)
            {
                float distance = Vector2.Distance(transform.position, GetAllEntities()[i].transform.position);
                if (distance < triggerRadius && distance < max)
                {
                    result = GetAllEntities()[i];
                    max = distance;
                }

                if (t >= searchCount)
                {
                    return result;
                }
                t++;
            }
        }
        return result;

    }


    /// <summary>
    /// Возвращает ближайшую сущность, из последних доавленных в массив, из половины юнитов
    /// </summary>
    /// <param name="serachCount"></param>
    /// <returns></returns>
    public virtual Entity GetOneNear(bool inverse)
    {
        float max = float.MaxValue;
        int t = 0;
        Entity result = null;
        EnemuManager em = LevelManager.Instance.EnemuManager;
        int searchCount = GetAllEntities().Length / 4;
        for (int i = inverse ? GetAllEntities().Length - 1 : 0; inverse ? (i > 0) : (i < GetAllEntities().Length); i = inverse ? (i - 1) : (i + 1))
        {
            if (GetAllEntities()[i] && GetAllEntities()[i].gameObject.activeInHierarchy)
            {
                float distance = Vector2.Distance(transform.position, GetAllEntities()[i].transform.position);
                if (distance < triggerRadius && distance < max)
                {
                    result = GetAllEntities()[i];
                    max = distance;
                }

                if (t >= searchCount)
                {
                    return result;
                }
                t++;
            }
        }
        return result;

    }

    protected Entity GetOneNearByArray(bool inverse, Entity[] array)
    {
        float max = float.MaxValue;
        int t = 0;
        Entity result = null;

        int searchCount = array.Length / 4;
        for (int i = inverse ? array.Length - 1 : 0; inverse ? (i > 0) : (i < array.Length); i = inverse ? (i - 1) : (i + 1))
        {
            if (array[i] && array[i].gameObject.activeInHierarchy)
            {
                float distance = Vector2.Distance(transform.position, array[i].transform.position);
                if (distance < triggerRadius && distance < max)
                {
                    result = array[i];
                    max = distance;
                }

                if (t >= searchCount)
                {
                    return result;
                }
                t++;
            }
        }
        return result;

    }


    ///// <summary>
    ///// Возвращает одну
    ///// </summary>
    ///// <returns></returns>
    public virtual Entity GetOneRandom()
    {
        Entity result = null;
        List<Entity> variants = new List<Entity>();
        //int t = 0;
        EnemuManager em = LevelManager.Instance.EnemuManager;
        for (int i = 0; i < GetAllEntities().Length; i++)
        {
            if (GetAllEntities()[i] && GetAllEntities()[i].gameObject.activeInHierarchy)
            {
                if (Vector2.Distance(transform.position, GetAllEntities()[i].transform.position) < triggerRadius)
                {
                    variants.Add(GetAllEntities()[i]);
                }
                //if (t >= maxSearchCount)
                //{
                //    return result;
                //}
                //t++;
            }
        }
        if(variants.Count > 0)
        result = variants[Random.Range(0, variants.Count - 1)];
        return result;
    }

    /// <summary>
    /// Возвращает true если сущность в радиусе
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual bool CheckRadius(Entity entity)
    {
        bool result = false;
        if (entity)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(entity.transform.position.x, entity.transform.position.y)) < triggerRadius)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        return result;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }

    public enum EntityType
    {
        Enemu,
    }
}
