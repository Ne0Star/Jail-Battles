using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляет анимациями
/// </summary>
public class AnimationController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mainRenderer;
    [SerializeField] protected List<AnimationState> animations;
    [SerializeField] private bool isPlay = false;
    [SerializeField] private bool initialize = false;
    [SerializeField] private float animationSpeed = 1f;
    [SerializeField] private float animationsDutationsMultipler;
    public float AnimationsDutationsMultipler { get => animationsDutationsMultipler; set => animationsDutationsMultipler = value; }

    public void SetAnimations(List<AnimationState> animations)
    {
        this.animations = animations;
        if (!initialize)
        {
            Play(animations[Random.Range(0, animations.Count)].Name);
            initialize = true;
        }
    }
    private void Awake()
    {

    }

    private IEnumerator AnimationPlaying(string name)
    {
        List<Sprite> sp = animations.Find((s) => s.Name == name).Sprites;
        if (sp != null)
            foreach (Sprite s in sp)
            {
                mainRenderer.sprite = s;

                yield return new WaitForSeconds(animationSpeed * animationsDutationsMultipler);
            }
        else
        {
            Debug.LogWarning("Несуществует анимации " + name);
            yield break;
        }
        StartCoroutine(AnimationPlaying(name));
    }

    public void Play(string name)
    {
        if (isPlay) StopAllCoroutines();
        StartCoroutine(AnimationPlaying(name));
    }
}
