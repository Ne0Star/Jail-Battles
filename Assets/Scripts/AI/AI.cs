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

//[Flags]
//public enum AITypes
//{
//    Преследователь,
//    Псих,
//    Бычара,
//    Трус,
//    Маньяк,
//    Уборщица,
//    Повариха,
//    ГлавнаяПовариха,
//    ГлавнаяУборщица,
//    Охранник,
//    Крыса

//}

[System.Serializable]
public struct AITB
{
    //[SerializeField] private bool mark;
    [SerializeField] private AIType type;

    //public bool Mark { get => mark; }
    public AIType Type { get => type; }
}
[System.Serializable]
public struct AITypes
{
    [SerializeField]
    private List<AITB> aiTypes;

    //public List<AITB> AiTypes { get => aiTypes; }

    /// <summary>
    /// true если содержит переданный тип
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool Contains(AIType type)
    {
        bool result = false;
        if (aiTypes != null)
            for (int i = 0; i < aiTypes.Count; i++)
            {
                if (type == aiTypes[i].Type)
                {
                    result = true;
                    break;
                }
            }
        return result;
    }

}


[System.Serializable]
public struct AIPressset
{
    [SerializeField] private Transform rotateParent;
    [SerializeField] private float rotateOffset;
    [SerializeField] private AITypes targetTypes;
    [SerializeField] private AITypes ignoreTypes;
    [SerializeField] private CustomCircleCollider trigger;

    public AITypes Targets { get => targetTypes; }
    public AITypes Ignores { get => ignoreTypes; }
    public Transform RotateParent { get => rotateParent; }
    public float RotateOffset { get => rotateOffset; }
}
public class AI : MonoBehaviour
{
    [SerializeField] private AIType type;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private AIPressset presset;


    [SerializeField] protected bool free = true;
    /// <summary>
    /// Свободен ли данный AI в текущий момент
    /// </summary>
    public bool Free { get => free; }
    public NavMeshAgent Agent { get => agent; }
    public AIPressset Presset { get => presset; }
    public AIType Type { get => type; }

    public virtual void CustomUpdate()
    {

    }

    public void SetPresset(AIPressset presset, NavMeshAgent agent, AIType type)
    {
        this.presset = presset;
        this.agent = agent;
        this.type = type;
    }
}
