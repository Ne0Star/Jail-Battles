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
            if (e.gameObject.activeSelf)
            {
                e.EnemuUpdate();
            }
            else
            {
                //e.gameObject.SetActive(true);
            }

        }

        yield return new WaitForSeconds(updateTime);
        StartCoroutine(Life());
    }
}
