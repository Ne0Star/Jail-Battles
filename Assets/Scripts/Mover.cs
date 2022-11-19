using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    [SerializeField] private AnimationCurve interpolator;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float timeStep;

    public Vector3 Offset { get => offset; set => offset = value; }

    private void Start()
    {
        StartCoroutine(Life());
    }

    private IEnumerator Life()
    {
        offset = LevelManager.Instance.Player.controller.Direction;
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, 0), interpolator.Evaluate(speed * timeStep));
        yield return new WaitForSeconds(0.004f);
        StartCoroutine(Life());
    }
}
