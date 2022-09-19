using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIType
{
    /// <summary>
    /// Появляется в закрытой зоне
    /// Весь цикл жизни прследует игрока
    /// </summary>
    Преследователь,

    /// <summary>
    /// Двигается по карте и никого не трогает, но в случае нападения на него -
    /// начинает цикл жестоких убийств после окончания цикла снова становится спокойным
    /// </summary>
    Псих,

    /// <summary>
    /// С момента появления сразу находит себе цель, и дерётся до самой смерти
    /// </summary>
    Бычара,

    /// <summary>
    /// Избегает драк, в случае опасности убегает, в состоянии покоя ищет свободный угол что бы там спрятаться
    /// </summary>
    Трус,

    /// <summary>
    /// Появляется только в скромных местах карты, не уходоит далеко от точки спавна
    /// Когда кто либо проходит рядом он вырубает его и уносит к точке появления, там издевается над жертвой до её смерти
    /// </summary>
    Маньяк,

    /// <summary>
    /// Статичный AI, ходит по карте, оставляет лужи с водой
    /// </summary>
    Уборщица,

    /// <summary>
    /// Готовит еду, никого не трогает, не отходит далеко от главной поварихи
    /// </summary>
    Повариха,

    /// <summary>
    /// Ходит по кухне берёт вилки ложки ножи, подходит к окну выдачи и кидается в тех кто дерётся перед столовой
    /// </summary>
    ГлавнаяПовариха,

    /// <summary>
    /// Ходит по карте и никого не трогает в случае обнаружения драк вступает в бой и вызывает подкрепление ближайших уборщиц
    /// </summary>
    ГлавнаяУборщица,

    /// <summary>
    /// Не отходит далеко от спавна, убивает всех кто близко подходит
    /// </summary>
    Охранник,

    /// <summary>
    /// Бегают по карте группой, если голодны сьедают зека
    /// </summary>
    Крыса

}


[Flags]
public enum AnimationTypes
{
    Idle,
    Move,
    Run,
    Attack,
    Special
}

[Flags]
public enum AITypes
{
    Преследователь,
    Псих,
    Бычара,
    Трус,
    Маньяк,
    Уборщица,
    Повариха,
    ГлавнаяПовариха,
    ГлавнаяУборщица,
    Охранник,
    Крыса

}
[System.Serializable]
public struct AIPressset
{
    [SerializeField] private AITypes targetTypes;
    [SerializeField] private AITypes ignoreTypes;
    [SerializeField] private CustomCircleCollider trigger;

    public AITypes TargetTypes { get => targetTypes; }
    public AITypes IgnoreTypes { get => ignoreTypes; }
}
public class AI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected AIPressset presset;


    public void SetPresset(AIPressset presset)
    {
        this.presset = presset;
    }
}
