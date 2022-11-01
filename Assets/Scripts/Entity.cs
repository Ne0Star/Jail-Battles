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

    public void SetMachine()
    {
        animator.SetBool("none", false);
        animator.SetBool("gun", false);
        animator.SetBool("machine", true);
        animator.SetBool("mele", false);
    }
    public void SetNone()
    {
        animator.SetBool("none", true);
        animator.SetBool("gun", false);
        animator.SetBool("machine", false);
        animator.SetBool("mele", false);
    }
    public void SetMele()
    {
        animator.SetBool("none", false);
        animator.SetBool("gun", false);
        animator.SetBool("machine", false);
        animator.SetBool("mele", true);
    }
    public void SetGun()
    {
        animator.SetBool("none", false);
        animator.SetBool("gun", true);
        animator.SetBool("machine", false);
        animator.SetBool("mele", false);
    }

    public void Play(string statName)
    {
        bool stat = false;
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
                stat = true;
            }
        }
        if(!stat)
        {
            Debug.LogWarning("У сущности нету данных анимации с названием: " + statName);
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
        Create();
    }

    /// <summary>
    /// Awake
    /// </summary>
    protected virtual void Create()
    {

    }
    protected virtual void Attacked(Entity attacker)
    {

    }
    public NavMeshAgent Agent { get => agent; }
    public HitBar HitBar { get => hitBar; }
    public EntityAnimationData Animator { get => animator; }
    public UnityEvent<Entity> OnDied { get => onDied; }

    /// <summary>
    /// OneNAB;E
    /// </summary>
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
    /// <summary>
    /// OnEnable
    /// </summary>
    protected virtual void Enable()
    {

    }
    /// <summary>
    /// OnDisable
    /// </summary>
    protected virtual void Disable()
    {

    }





    public void TakeDamage(Entity source, float damage, System.Action onKill)
    {
        Attacked(source);
        hitBar.TakeDamage(source, damage, () =>
        {
           
                onDied?.Invoke(this);
            onKill();
            gameObject.SetActive(false);
            
        });
    }

}
