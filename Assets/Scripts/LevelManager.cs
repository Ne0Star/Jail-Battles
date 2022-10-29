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


public class LevelManager : OneSingleton<LevelManager>
{

    public List<RangeRange> ranges;

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



    [SerializeField] private float customTime;

    public float CustomTime { get => customTime; }

    [SerializeField] private LevelData levelData;

    [SerializeField] private Player player;
    [SerializeField] private EnemuManager enemuManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private TriggerManager triggerManager;
    [SerializeField] private LevelPresset levelPresset;

    public Player Player { get => player; }
    public EnemuManager EnemuManager { get => enemuManager; }
    public AudioManager AudioManager { get => audioManager; }
    public TriggerManager TriggerManager { get => triggerManager; }
    public LevelPresset LevelPresset { get => levelPresset; }
    public LevelData LevelData { get => levelData; }
    public WeaponManager WeaponManager { get => weaponManager; }

    public Entity[] GetAllEntites()
    {
        List<Entity> result = new List<Entity>();
        result.AddRange(enemuManager.AllEnemies.Values);
        result.Add(Player);
        return result.ToArray();
    }

    private void Awake()
    {
        LevelManager.Instance = this;
        if (!enemuManager) enemuManager = FindObjectOfType<EnemuManager>();
        if (!audioManager) audioManager = FindObjectOfType<AudioManager>();
        if (!triggerManager) triggerManager = FindObjectOfType<TriggerManager>();
        if (!weaponManager) weaponManager = FindObjectOfType<WeaponManager>();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        for (int i = 0; i < 180; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        Time.timeScale = 0;
    }

}
