using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Спавн и цикл врагов
/// </summary>
public class EnemuManager : MonoBehaviour
{
    [SerializeField] private float updateTime;
    [SerializeField] private int stepCount;
    [SerializeField] private CustomList<Enemu> allEnemies;

    public CustomList<Enemu> AllEnemies { get => allEnemies; }


    [SerializeField] private List<Convict> allConvicts;
    [SerializeField] private List<Cleaner> allCleaners;
    /// <summary>
    /// Все зеки на карте
    /// </summary>
    public List<Convict> AllConvicts { get => allConvicts; }
    /// <summary>
    /// Все уборщицы на карте
    /// </summary>
    public List<Cleaner> AllCleaners { get => allCleaners; }


    public T GetEntityByType<T>() where T : Enemu
    {
        T result = null;
        foreach (Entity entity in allEnemies)
        {
            if (entity as T)
            {
                result = (T)entity;
                break;
            }
        }
        return result;
    }

    public List<Entity> GetAllEntityByType(EntityType type)
    {
        List<Entity> r = new List<Entity>();
        switch (type)
        {
            case EntityType.Зек:
                r.AddRange(allConvicts);
                break;
            case EntityType.Уборщик:
                r.AddRange(allCleaners);
                break;
            case EntityType.Повар:
                break;
            case EntityType.Охранник:
                break;
            case EntityType.Игрок:
                r.Add(LevelManager.Instance.Player);
                break;
        }
        return r;
    }
    private void OnEnable()
    {

        allEnemies = new CustomList<Enemu>(updateTime, stepCount);
        allEnemies.RegisterRange(FindObjectsOfType<Enemu>(true));

        StartCoroutine(Enabled());

        allEnemies.StartLife(this);

    }

    private IEnumerator Enabled()
    {
        foreach (Enemu e in allEnemies)
        {
            if (e as Convict)
            {
                allConvicts.Add((Convict)e);
            }
            if (e as Cleaner)
            {
                allCleaners.Add((Cleaner)e);
            }


            e.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.001f);
        }
    }

    public Enemu[] GetAllEnemu(Enemu ignore)
    {
        List<Enemu> result = new List<Enemu>();
        foreach (Enemu en in allEnemies)
        {
            if (en && en != ignore)
            {
                result.Add(en);
            }
        }
        return result.ToArray();
    }
}
