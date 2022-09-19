using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    /// <summary>
    /// whom поворачивается в сторону where, с смещением offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static void LookAt2D(Transform whom, Vector2 where, float offset)
    {
        float angle = Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg;
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle + offset);

        whom.rotation = total;
    }
}


public interface IAI
{
}