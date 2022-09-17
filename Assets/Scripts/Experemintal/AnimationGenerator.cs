using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит в себе спрайты в нужном порядке, название анимации
/// </summary>
public class AnimationStates
{
    public string stateName;
    public List<Sprite> spreties;
}
[System.Serializable]
public class AnimationLayerStats
{
    public int layer;
    public List<string> statesName;
}

/// <summary>
/// Делает раскадровку указанных анимаций
/// Отвечает за вопроизведение анимаций
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimationGenerator : MonoBehaviour
{
    [Header("Список анимаций которые нужно преобразовать в покадровые")]
    [SerializeField] private List<AnimationLayerStats> createStates;
    [SerializeField] private List<AnimatorClipInfo> asd;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera sreenCamera;

    private void Awake()
    {
        StartCoroutine(StartBaked());
    }
    private IEnumerator StartBaked()
    {
        for (int i = 0; i < createStates.Count; i++)
        {
            for (int j = 0; j < createStates[i].statesName.Count; j++)
            {
                var r = animator.GetCurrentAnimatorClipInfo(createStates[i].layer);
                animator.Play(createStates[i].statesName[j], createStates[i].layer);



                //yield return new WaitUntil(() => r.);
                Debug.Log("Ммм");
            }
        }
        yield break;
    }
}
