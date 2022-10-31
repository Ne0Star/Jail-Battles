using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum EntityType
{
    Зек,
    Уборщик,
    Повар,
    Охранник,
    Игрок
}

[System.Serializable]
public struct EntityAnimationData
{
    [SerializeField] private EntityType entityType;
    [SerializeField] private AudioSource source;
    [SerializeField] private Animator animator;
    [SerializeField] private List<EntityAnimation> animations;

    public void Play(string statName)
    {
        foreach (EntityAnimation animation in animations)
        {
            if (statName == animation.statName)
            {
                animator.speed = animation.animationSpeed;
                source.volume = animation.clipVolume;
                source.loop = animation.clipLoop;
                source.pitch = animation.clipPitch;
                source.clip = LevelManager.Instance.GetClip(animation.clipName);
                source.Play();
                animator.Play(animation.animName);
            }
        }
    }

    public AudioSource Source { get => source; }
    public Animator Animator { get => animator; }
    public EntityType EntityType { get => entityType; }
}

/// <summary>
/// Главный класс всех сущностей в игре
/// Каждый враг и игрок имеет анимации действий, каждая анимация имеет звуки
/// </summary>
public abstract class Entity : MonoBehaviour
{
    private HitBar hitBar;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected EntityAnimationData animator;

    [SerializeField] private UnityEvent<Entity> onDied;

    private void Awake()
    {
        if (!hitBar)
        {
            hitBar = GetComponentInChildren<HitBar>(true);
        }
        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
            if (!agent)
            {
                agent = GetComponent<NavMeshAgent>();
            }
        }
    }

    public NavMeshAgent Agent { get => agent; }
    public HitBar HitBar { get => hitBar; }
    public EntityAnimationData Animator { get => animator; }
    public UnityEvent<Entity> OnDied { get => onDied; }

    private void OnEnable()
    {
        if (!agent)
            agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        Enable();
    }
    private void OnDisable()
    {
        Disable();
    }

    public void DisableAgent()
    {
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }
    public void EnableAgent()
    {
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        if (agent)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    protected virtual void Enable()
    {

    }
    protected virtual void Disable()
    {

    }





    public void TakeDamage(Entity source, float damage, System.Action onKill)
    {
        hitBar.TakeDamage(source, damage, () =>
        {
           
                onDied?.Invoke(this);
            onKill();
            gameObject.SetActive(false);
            
        });
    }

}
