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
    public static void LookAt2DSmooth(Transform whom, Vector3 where, float offset, float time, float distanceToComplete, System.Action onComplete)
    {
        float angle = GameUtils.RoundToValue(Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg - offset, 0.05f);
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle);
        whom.rotation = Quaternion.Lerp(whom.rotation, total, time);
        if (GameUtils.RoundToValue(Vector2.Distance(new Vector2(total.z, 0), new Vector2(whom.rotation.z, 0)), 0.05f) <= distanceToComplete) onComplete();
    }
    public static float RoundToValue(float round, float value)
    {
        float result = 0f;
        if (round % value > (value / 2f))
            result = (int)(round / value) * value + value;
        else
            result = (int)(round / value) * value;
        return result;
    }
    public static Vector3 ClampMagnitude(Vector3 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }
}


public interface IAI
{
}