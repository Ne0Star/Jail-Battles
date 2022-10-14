using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Спавн и цикл врагов
/// </summary>
public class EnemuManager : MonoBehaviour
{
    [SerializeField] private float enemuLifeTime;

    [SerializeField] private Enemu[] allEnemies = null;

    public Enemu[] AllEnemies { get => allEnemies; }

    private void Awake()
    {
        allEnemies = FindObjectsOfType<Enemu>();

    }
    private void Start()
    {
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        foreach (Enemu e in allEnemies)
        {
            e.EnemuUpdate();
        }

        yield return new WaitForSeconds(enemuLifeTime);
        StartCoroutine(Life());
    }
}
