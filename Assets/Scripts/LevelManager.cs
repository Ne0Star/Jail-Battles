using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelData
{

}

[System.Serializable]
public struct RangeRange
{
    public string name;
    public Color color;
    public int min, max;
    public float respawnTime;
}

[System.Serializable]
public struct UpdateData
{
    [SerializeField] private bool addtive;
    [SerializeField] float currentValue;
    [SerializeField] float min, max;

    public float CurrentValue { get => currentValue; }

    public void Update()
    {
        if (addtive)
        {
            currentValue += Random.Range(min, max);
        }
        else
        {
            currentValue -= Random.Range(min, max);
        }

    }

}

public enum AreaType
{
    Кухня,
    Столовая
}

[System.Serializable]
public struct AIAreas
{
    [SerializeField] private AreaType areaType;
    [SerializeField] private List<AIArea> areas;

    public AreaType AreaType { get => areaType; }
    public List<AIArea> Areas { get => areas; }
}

public class LevelManager : OneSingleton<LevelManager>
{
    [SerializeField] private List<AIAreas> areas;
    [SerializeField] private List<RangeRange> ranges;
    [SerializeField] private float customTime;
    [SerializeField] private LevelData levelData;
    [SerializeField] private Player player;
    [SerializeField] private EnemuManager enemuManager;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private TriggerManager triggerManager;
    [SerializeField] private CleanersManager cleanersManager;
    [SerializeField] private LevelPresset levelPresset;

    public Player Player { get => player; }
    public EnemuManager EnemuManager { get => enemuManager; }
    public TriggerManager TriggerManager { get => triggerManager; }

    public LevelPresset LevelPresset { get => levelPresset; }
    public LevelData LevelData { get => levelData; }
    public WeaponManager WeaponManager { get => weaponManager; }
    public float CustomTime { get => customTime; }
    public CleanersManager CleanersManager { get => cleanersManager; }

    public List<AIArea> GetAreas(AreaType type)
    {
        foreach (AIAreas area in areas)
        {
            if (area.AreaType == type)
            {
                return area.Areas;
            }
        }
        return null;
    }

    public RangeRange GetColorByRange(int range)
    {
        RangeRange result = new RangeRange();
        foreach (RangeRange r in ranges)
        {
            if (range <= r.max && range >= r.min)
            {
                result = r;
                return result;
            }
        }
        return result;
    }


    public Entity[] GetAllEntites()
    {
        List<Entity> result = new List<Entity>();
        result.AddRange(enemuManager.AllEnemies.Values);
        result.Add(Player);
        return result.ToArray();
    }
    [SerializeField] private List<AudioClip> findClips = new List<AudioClip>();
    [SerializeField] private Dictionary<string, AudioClip> allClips = new Dictionary<string, AudioClip>();


    public AudioClip GetClip(string name)
    {
        AudioClip result = null;
        allClips.TryGetValue(name, out result);
        return result;
    }

    private void Awake()
    {
        LevelManager.Instance = this;
        foreach (AudioClip clip in findClips)
        {
            if (clip != null)
            {
                allClips.Add(clip.name, clip);
            }
        }
        findClips = null;
        if (!enemuManager) enemuManager = FindObjectOfType<EnemuManager>(true);
        if (!triggerManager) triggerManager = FindObjectOfType<TriggerManager>(true);
        if (!weaponManager) weaponManager = FindObjectOfType<WeaponManager>(true);
        if (!cleanersManager) cleanersManager = FindObjectOfType<CleanersManager>(true);
        StartCoroutine(Wait());
    }

    private IEnumerator Start()
    {
        triggerManager.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.1f);
        weaponManager.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        cleanersManager.gameObject.SetActive(true);
        enemuManager.gameObject.SetActive(true);
        yield return null;
    }

    private IEnumerator Wait()
    {
        for (int i = 0; i < 180; i++)
        {
            //Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        Time.timeScale = 0;
    }
}
