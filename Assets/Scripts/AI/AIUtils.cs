using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



/// <summary>
/// Содержит вспомогательные и универсальные методы для AI
/// </summary>
public static class AIUtils
{

    public static bool Collision(NavMeshAgent one, NavMeshAgent two)
    {
        if (Vector2.Distance(one.transform.position, two.transform.position) <= (one.radius + two.radius + 0.01f))
        {
            return true;
        } else
        {
            return false;
        }
    }

    //public static void MoveCircle(Vector3 center, AI[] array, float pose, float scalePersent)
    //{
    //    if (array == null)
    //        return;
    //    int length = array.Length;
    //    if (length <= 0)
    //        return;
    //    //float pose = 0.01465452f + Random.Range(0.001f, 0.01f);// Mathf.Sin(Random.Range(0.00f, 360.00f)) * Mathf.Cos(Random.Range(0.00f, 360.00f));
    //    for (int i = 0; i < length; i++)
    //    {
    //        AI entity = array[i];
    //        if (entity)
    //        {
    //            float theta = i * Mathf.Rad2Deg * (pose % 5f);//Poses[Random.Range(0, Poses.Length)];
    //            float r = scalePersent * Mathf.Sqrt(i);
    //            Vector3 result = center + new Vector3(Mathf.Cos(theta) * r, Mathf.Sin(theta) * r, 0);
    //            entity.transform.position = new Vector3(result.x, result.y, 0);
    //            // entity.currentMovePosition = result;
    //        }

    //    }
    //}

    //public static void Move(AI ai, Vector3 position, System.Action onReach)
    //{
    //    ai.StartCoroutine(_Move(ai, position, onReach));
    //}

    //private static IEnumerator _Move(AI ai, Vector3 position, System.Action onReach)
    //{



    //    float distance = float.MaxValue;
    //    ai.Agent.SetDestination(position);
    //    do
    //    {
    //        if (ai.Agent.radius >= Vector2.Distance(ai.Agent.transform.position, position)) break;
    //    }
    //    while (distance > ai.Agent.radius);
    //    {
    //        distance = Vector2.Distance(ai.Agent.transform.position, position);
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //    onReach();
    //}
}
