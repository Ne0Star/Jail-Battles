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

    private void Start()
    {
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        yield return new WaitForSeconds(timeStep);  
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, offset.z), interpolator.Evaluate(speed * timeStep));
        StartCoroutine(Life());
        yield break;
    }

}
