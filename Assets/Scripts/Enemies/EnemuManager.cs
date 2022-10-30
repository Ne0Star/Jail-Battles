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

    private void Start()
    {
        allEnemies = new CustomList<Enemu>(updateTime, stepCount);
        allEnemies.RegisterRange(FindObjectsOfType<Enemu>());
        allEnemies.StartLife(this);
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

    //private IEnumerator Life()
    //{
    //    foreach (Enemu e in allEnemies)
    //    {
    //        if (e.gameObject.activeSelf)
    //        {
    //            e.EnemuUpdate();
    //        }
    //        else
    //        {
    //            //e.gameObject.SetActive(true);
    //        }

    //    }

    //    yield return new WaitForSeconds(updateTime);
    //    StartCoroutine(Life());
    //}
}
