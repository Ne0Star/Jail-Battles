using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Presset", menuName = "AnimationPresset/Presset", order = 1)]
public class AnimationStatesPresset : ScriptableObject
{
    [SerializeField] private AnimationPresset animationPressetStart;
    [SerializeField] private SoundPresset soundPressetStart;
    [SerializeField] private AnimationPresset animationPressetComplete;
    [SerializeField] private SoundPresset soundPressetComplete;
    public AnimationPresset AStart { get => animationPressetStart; }
    public SoundPresset SStart { get => soundPressetStart; }
    public AnimationPresset AComplete { get => animationPressetComplete; }
    public SoundPresset SComplete { get => soundPressetComplete; }
}
