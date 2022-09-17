using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Спавн и цикл врагов
/// </summary>
public class EnemuManager : MonoBehaviour
{
    [SerializeField] private Enemu[] allEnemies = null;

    public Enemu[] AllEnemies { get => allEnemies; }

    private void Awake()
    {
        allEnemies = FindObjectsOfType<Enemu>();
    }

    ///// <summary>
    ///// Создать сущность в пуле
    ///// </summary>
    //public Enemu CreateEnemu(GameObject sources)
    //{
    //    if (!sources)
    //    {

    //        Debug.LogWarning("EnemuManager: CreateEnemu() sources == null");
    //        return null;
    //    }
    //    Enemu result = sources.AddComponent<Enemu>();
    //    allEnemies.Add(result);
    //    return result;
    //}




    //[Header("Ручной пул")]
    //[SerializeField] private List<Enemu> batches;
    //[SerializeField] private List<Enemu> pursuers;
    //[Range(1f, 100f)]
    //[SerializeField] private float enemuToFramePersent;
    //[SerializeField] private List<Spawner> spawners;


    //public List<Enemu> Batches => batches;

    //private void Awake()
    //{
    //    batches.AddRange(pursuers);
    //    batches.Distinct();
    //    //foreach (Enemu e in batches)
    //    //{
    //    //    if (e)
    //    //        e.gameObject.SetActive(false);
    //    //}
    //    if (batches != null && batches.Count > 0)
    //    {
    //        int divides = 0;
    //        Debug.Log(Mathf.RoundToInt(batches.Count / 100f * enemuToFramePersent));
    //        for (int i = 1; i < Mathf.RoundToInt(batches.Count / 100f * enemuToFramePersent); i++)
    //        {
    //            if (batches.Count % i == 0f)
    //            {
    //                divides = i;
    //            }
    //        }
    //        Debug.Log(divides);
    //        float t = 0.0025f;
    //        int last = 0;
    //        int next = (batches.Count / divides);
    //        for (int i = 0; i < next; i++) // 0 3
    //        {
    //            StartCoroutine(EnemuLife(last, last + divides - 1, t));
    //            t += 0.0005f;
    //            last += divides;
    //        }
    //    }
    //}
    //public void SpawnPursueEnemu()
    //{
    //    foreach (Spawner spawner in spawners)
    //    {
    //        if (spawner && spawner.AllowSpawn)
    //        {
    //            foreach (Enemu e in pursuers)
    //            {
    //                if (e && !e.gameObject.activeInHierarchy)
    //                {
    //                    e.gameObject.SetActive(true);
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}
    //public void SpawnRandomEnemu()
    //{

    //}


    ////[SerializeField] private int enemuToFrame; // Количество индексов которые будут проходится в 1 фрейм
    ////[SerializeField] private int enemuLast = 0;
    ////[SerializeField] private int enemuCurrent = 0;
    //private IEnumerator EnemuLife(int start, int end, float time)
    //{
    //    int s = start;
    //    int e = end;
    //    int i = s;
    //    while (i < (e + 1))
    //    {
    //        batches[i].EnemuUpdate();
    //        batches[i].Check = !batches[i].Check;
    //        if (i >= e) break;
    //        yield return new WaitForSeconds(0.005f);
    //        i++;
    //    }
    //    yield return new WaitForSeconds(time);
    //    StartCoroutine(EnemuLife(start, end, time));
    //    yield break;
    //}


    //private void EnemuFor(int start, int end)
    //{
    //    for (int i = start; start < end; start++)
    //    {

    //    }
    //}

}
