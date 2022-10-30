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
        if(activeTrash == null || activeTrash.Count <= 0) return null;
        return activeTrash[Random.Range(0, activeTrash.Count - 1)];
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

    private void Start()
    {
        foreach (Entity e in LevelManager.Instance.GetAllEntites())
        {
            e.onDied += CreateTrash;
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
        t.transform.position = target.transform.position;
        t.gameObject.SetActive(true);

        t.onComplete += RemoveTrash;

        //t.onComplete += (tt) =>
        //{
        //    tt.onComplete -= ;
        //    activeTrash.Remove(tt);
        //};
        activeTrash.Add(t);
    }


}
