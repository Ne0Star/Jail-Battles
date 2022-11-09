using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteColor
{
    [SerializeField] private Color color;
    [SerializeField] private Sprite sprite;

    public Color Color { get => color; set => color = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
}

[System.Serializable]
public struct TrashData
{
    [SerializeField] private bool autoDestroy;
    [SerializeField] private TrashType trashType;
    [SerializeField] private List<SpriteColor> spriteVariants;

    public TrashType TrashType { get => trashType; }
    public List<SpriteColor> SpriteVariants { get => spriteVariants; }
    public bool AutoDestroy { get => autoDestroy; }
}

public class TrashManager : MonoBehaviour
{

    public event System.Action<Trash> onCreatedTrash;

    [SerializeField] private List<TrashData> trashDatas;

    [SerializeField] private int batchCount;

    [SerializeField] private Trash trashPrefab;
    [SerializeField] private List<Trash> allTrash;


    private void Awake()
    {
        for (int i = 0; i < batchCount; i++)
        {

            Trash t = Instantiate(trashPrefab, transform);
            GameObject trashOBJ = t.gameObject;
            trashOBJ.name = "Trash: " + i;
            trashOBJ.transform.parent = transform;
            trashOBJ.gameObject.SetActive(false);
            allTrash.Add(t);
        }
    }

    //private void Start()
    //{
    //    foreach (Entity e in LevelManager.Instance.GetAllEntites())
    //    {
    //        e.OnDied?.AddListener((entity) =>
    //        {
    //            CreateTrash(entity.DiedVFX, entity.transform.position);
    //        });
    //    }
    //}

    /// <summary>
    /// Создаёт мусор указанного типа  на сцене
    /// </summary>
    public void CreateTrash(TrashType type, Vector3 worldPosition)
    {
        Trash t = GetFreeTrash();
        if (!t)
        {
            Debug.LogError("Попытка создать мусор на сцене неудачна: Закончился свободный мусор");
            return;
        }
        t.Initial(GetDataByType(type));
        t.gameObject.SetActive(true);
        t.gameObject.transform.position = worldPosition;
        onCreatedTrash?.Invoke(t);
    }

    public TrashData GetDataByType(TrashType type)
    {
        foreach (TrashData data in trashDatas)
        {
            if (data.TrashType == type)
            {
                return data;
            }
        }
        throw new System.Exception("Не удалось получить данные мусора по типу " + type)
        {

        };
    }

    /// <summary>
    /// Возвращает активный мусор любого типа
    /// </summary>
    /// <returns></returns>
    private Trash GetFreeTrash()
    {
        Trash result = null;
        foreach (Trash t in allTrash)
        {
            if (t && !t.gameObject.activeSelf)
            {
                result = t;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// Возвращает активный мусор любого типа
    /// </summary>
    /// <returns></returns>
    public Trash GetFreeActiveTrash()
    {
        Trash result = null;
        foreach (Trash t in allTrash)
        {
            if (t && t.gameObject.activeSelf && t.IsFree)
            {
                result = t;
                break;
            }
        }
        return result;
    }

}
