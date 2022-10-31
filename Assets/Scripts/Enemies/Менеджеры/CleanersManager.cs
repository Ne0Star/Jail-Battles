using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanersManager : MonoBehaviour
{
    [SerializeField] private int maxTrash;
    [SerializeField] private Trash trashPrefab;

    [SerializeField] private List<Trash> allTrash = new List<Trash>();
    [SerializeField] private List<Trash> activeTrash = new List<Trash>();

    public List<Trash> ActiveTrash { get => activeTrash; }


    public Trash GetRandomActiveTrash()
    {
        if (activeTrash == null || activeTrash.Count <= 0) return null;

        Trash result = activeTrash[Random.Range(0, activeTrash.Count - 1)];

        if (!result.block)
        {
            result.block = true;
            return result;
        }
        else
        {
            return null;
        }
        return result;
    }

    private void Awake()
    {
        for (int i = 0; i < maxTrash; i++)
        {
            Trash trash = Instantiate(trashPrefab, transform);
            trash.gameObject.SetActive(false);
            allTrash.Add(trash);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Entity e in LevelManager.Instance.GetAllEntites())
        {
            e.OnDied?.AddListener((v) => CreateTrash(e));
        }
    }

    private Trash GetFreeTrash()
    {
        Trash result = null;
        foreach (Trash trash in allTrash)
        {
            if (trash && !trash.gameObject.activeSelf)
            {
                result = trash;
                break;
            }
        }
        return result;
    }

    private void RemoveTrash(Trash t)
    {
        t.onComplete -= RemoveTrash;
    }

    private void CreateTrash(Entity target)
    {
        Trash t = GetFreeTrash();
        if (t)
        {
            t.transform.position = target.transform.position;
            t.gameObject.SetActive(true);
            t.onComplete += RemoveTrash;
            activeTrash.Add(t);
        }
        else { Debug.Log("Мусор закончился"); }
    }


}
