using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Содержит анимации приндлежащие живым сущностям
/// </summary>
[System.Serializable]
public struct EntityAnimationPresset
{
    [Header("Покой")]
    [SerializeField] private AnimationClip idle;
    [Header("Ходьба")]
    [SerializeField] private AnimationClip walk;
    [Header("Бег")]
    [SerializeField] private AnimationClip run;
    [Header("Боеваая стойка")]
    [SerializeField] private AnimationClip fightingStance;


    [Header("Быстрая атака")]
    [SerializeField] private List<Attack> fastAttacks;
    [Header("Сильная атака")]
    [SerializeField] private List<Attack> powerAttacks;

    [Header("Быстрая атака при ходьбе")]
    [SerializeField] private List<Attack> walkFastAttacks;
    [Header("Сильная атака при ходьбе")]
    [SerializeField] private List<Attack> walkPowerAttacks;

    /// <summary>
    /// Анимации быстрых атак
    /// </summary>
    public List<Attack> FastAttacks { get => fastAttacks; set => fastAttacks = value; }
    /// <summary>
    /// Анимации стандартных атак
    /// </summary>
    public List<Attack> PowerAttacks { get => powerAttacks; set => powerAttacks = value; }
    /// <summary>
    /// Анимации быстрых атак в движении
    /// </summary>
    public List<Attack> WalkFastAttacks { get => walkFastAttacks; set => walkFastAttacks = value; }
    /// <summary>
    /// Анимации мощных атак в движении
    /// </summary>
    public List<Attack> WalkPowerAttacks { get => walkPowerAttacks; set => walkPowerAttacks = value; }
    public AnimationClip FightingStance { get => fightingStance; set => fightingStance = value; }
    public AnimationClip Run { get => run; set => run = value; }
    public AnimationClip Walk { get => walk; set => walk = value; }
    public AnimationClip Idle { get => idle; set => idle = value; }
}

/// <summary>
/// Главный класс всех сущностей в игре
/// Каждый враг и игрок имеет анимации действий, каждая анимация имеет звуки
/// </summary>
public abstract class Entity : MonoBehaviour
{

    public AnimationController AnimationController { get => animationController; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; }
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private float health;


    [System.Serializable]
    public class OnDied : UnityEngine.Events.UnityEvent<Entity> { }

    [SerializeField] private OnDied onDied;

    //[SerializeField] protected EntityAnimationPresset animationPresset;
    //[SerializeField] protected Animator animator;


    protected virtual void TakeDamage(float damage)
    {

    }
    /// <summary>
    /// Нанесёт урон указанной сущности
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    protected virtual void DealDamage(Entity target, float damage)
    {
        target.TakeDamage(damage);
    }
}
