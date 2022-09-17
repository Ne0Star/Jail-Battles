using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Враг
/// </summary>
public class Prisoner : Enemu
{
    //[SerializeField] private float moveDistance;
    //[SerializeField] private float searchTime;
    //[SerializeField] private float rotateOffset;
    //[SerializeField] private Player player;

    //private Vector3 moveVector;
    ///// <summary>
    ///// Двигатся в случайном направлении от себя
    ///// Если нашёл игрока преследовать его до тех пор пока кто то не умрёт
    ///// </summary>
    //private void OnEnable()
    //{

    //    StartCoroutine(WaitSearch());
    //}
    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //    player = null;
    //}
    //private IEnumerator WaitSearch()
    //{
    //    if (!player)
    //        player = FindObjectOfType<Player>();
    //    yield return new WaitForSeconds(searchTime);

    //    if (!player)
    //    {
    //        moveVector = transform.position + new Vector3(Random.Range(-moveDistance, moveDistance), Random.Range(-moveDistance, moveDistance), 0);
    //        agent.SetDestination(moveVector);
    //    }
    //    else
    //    {
    //        moveVector = player.transform.position;
    //    }


    //    StartCoroutine(WaitSearch());
    //}
    //private void FixedUpdate()
    //{
    //    if (player)
    //        agent.SetDestination(player.transform.position);


    //}
    //private void Update()
    //{
    //    GameUtils.LookAt2D(transform, transform.position + agent.velocity, rotateOffset);
    //}
}
