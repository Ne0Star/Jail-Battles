using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : OneSingleton<LevelManager>
{
    [SerializeField] private Player player;
    [SerializeField] private EnemuManager enemuManager;
    [SerializeField] private AIManager aiManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ColliderManager colliderManager;
    public Player Player { get => player; }
    public EnemuManager EnemuManager { get => enemuManager; }
    public AIManager AiManager { get => aiManager; }
    public AudioManager AudioManager { get => audioManager; }
    public ColliderManager ColliderManager { get => colliderManager; }

    public Entity[] GetAllEntites()
    {
        List<Entity> result = new List<Entity>();
        result.AddRange(enemuManager.AllEnemies);
        result.Add(Player);
        return result.ToArray();
    }

    private void Awake()
    {
        LevelManager.Instance = this;
        if (!enemuManager) enemuManager = FindObjectOfType<EnemuManager>();
        if (!aiManager) aiManager = FindObjectOfType<AIManager>();
        if(!audioManager) audioManager = FindObjectOfType<AudioManager>();
        if (!colliderManager) colliderManager = FindObjectOfType<ColliderManager>();
    }

}
