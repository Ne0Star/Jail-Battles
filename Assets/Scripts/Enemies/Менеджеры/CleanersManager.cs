using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TrashData
{
    public TrashType type;
    public List<Sprite> sprites;
}

public class CleanersManager : MonoBehaviour
{

    public TrashData GetTrashData(TrashType type)
    {
        TrashData result = new TrashData();
        foreach(TrashData data in trashDatas)
        {
            if(data.type == type)
            {
                result = data;
                break;
            }
        }
        return result;
    }

    [SerializeField] private List<TrashData> trashDatas;

    [SerializeField] private int maxTrash;
    [SerializeField] private Trash trashPrefab;

    [SerializeField] private List<Trash> allTrash = new List<Trash>();
    [SerializeField] private List<Trash> activeTrash = new List<Trash>();

    public List<Trash> ActiveTrash { get => activeTrash; }
    public List<TrashData> TrashDatas { get => trashDatas; }

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

    private void OnEnable()
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
            if (trash && !trash.block)
            {
                result = trash;
                break;
            }
        }
        return result;
    }

    //private void RemoveTrash(Trash t)
    //{
    //    activeTrash.Remove(t);
    //    //t.onComplete.AddListener((t) => RemoveTrash(t));
    //}

    private void CreateTrash(Entity target)
    {
        Trash t = GetFreeTrash();
        if (t)
        {
            t.transform.position = target.Agent.transform.position;
            t.gameObject.SetActive(true);
            activeTrash.Add(t);
            t.onComplete.AddListener((tt) => activeTrash.Remove(t));

        }
        else { Debug.Log("Мусор закончился"); }
    }


}
