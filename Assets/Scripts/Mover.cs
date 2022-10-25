using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private AnimationCurve interpolator;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float timeStep;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, offset.z), interpolator.Evaluate(speed * timeStep));
    }

}
