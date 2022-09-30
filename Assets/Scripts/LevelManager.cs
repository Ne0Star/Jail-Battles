using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : OneSingleton<LevelManager>
{
    [SerializeField] private Player player;
    [SerializeField] private EnemuManager enemuManager;
    [SerializeField] private AIManager aiManager;

    public Player Player { get => player; }
    public EnemuManager EnemuManager { get => enemuManager; }
    public AIManager AiManager { get => aiManager; }

    private void Awake()
    {
        LevelManager.Instance = this;
        if (!enemuManager) enemuManager = FindObjectOfType<EnemuManager>();
        if (!aiManager) aiManager = FindObjectOfType<AIManager>();
    }

}
