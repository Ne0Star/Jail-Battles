
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class HitBar : MonoBehaviour
{
    [SerializeField] private UnityEvent<Entity, float> onDamaged, onHealth;

    private void Awake()
    {
        onDamaged = new UnityEvent<Entity, float>();
        onHealth = new UnityEvent<Entity, float>();
        UpdateData();
    }

    [SerializeField]
    protected float minDamage;
    [SerializeField]
    protected float maxDamage;

    [SerializeField]
    protected float totalHealthDamage;

    [SerializeField]
    protected float health;

    [SerializeField]
    protected float maxHealth;

    public float TotalHealthDamage { get => Mathf.Clamp(totalHealthDamage, 0, maxHealth); }

    public float Health { get => GetHalth(); }
    public UnityEvent<Entity, float> OnDamaged { get => onDamaged; }
    public UnityEvent<Entity, float> OnHealth { get => onHealth; }

    public float GetHalth()
    {
        return health;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void UpdateHealth(float value)
    {
        maxHealth += value;
        health += value;
        UpdateData();
    }
    public void SetHealth(float val)
    {
        maxHealth = val;
        health = Mathf.Clamp(val, 0, val);

        UpdateData();
    }

    /// <summary>
    /// Срабатывает при любых изменениях показателей
    /// </summary>
    public abstract void UpdateData();


    /// <summary>
    /// false если фулл хп
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool AddHealth(float value)
    {
        bool result = health < maxHealth ? true : false;
        if (result)
        {
            health = Mathf.Clamp(health + value, 0, maxHealth);
        }
        UpdateData();
        return result;
    }


    public bool isFull()
    {
        bool result = true;
        if (health < maxHealth) result = false;
        return result;
    }

    public void SetFull()
    {
        health = maxHealth;
        totalHealthDamage = maxHealth - health;
        UpdateData();
    }

    /// <summary>
    /// false если фулл хп
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public void TakeHealth(Entity target, float value, float duration)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);
        onHealth?.Invoke(target, value);
        UpdateData();
    }

    // Добавить 50 здоровья, имеется 25 здоровья, максимум 25 здоровья и 50 прочности

    public void TakeDamage(Entity target, float damage, System.Action onDied)
    {
        if (GetHalth() > 0)
        {
            totalHealthDamage += damage;
            health = Mathf.Clamp(maxHealth - totalHealthDamage, 0, maxHealth);
        }
        if (GetHalth() <= 0)
            onDied();
        onDamaged?.Invoke(target, damage);
        UpdateData();
    }
    //protected void FixedUpdate()
    //{
    //    UpdateData();
    //}

    protected void OnDrawGizmos()
    {
        totalHealthDamage = maxHealth - health;
    }

}