using UnityEngine;

[CreateAssetMenu(fileName = "Entity Animation", menuName = "Entity Animation", order = 1)]
public class EntityAnimation : ScriptableObject
{
    public string statName;
    public string clipName;
    public string animName;


    public float animationSpeed;
    public float clipPitch;
    public float clipVolume;
    public bool clipLoop;
}
