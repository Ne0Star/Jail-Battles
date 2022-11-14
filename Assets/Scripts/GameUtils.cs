using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class RectTransformExtensions
{
    public static void SetDefaultScale(this RectTransform trans)
    {
        trans.localScale = new Vector3(1, 1, 1);
    }
    public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
    {
        trans.pivot = aVec;
        trans.anchorMin = aVec;
        trans.anchorMax = aVec;
    }

    public static Vector2 GetSize(this RectTransform trans)
    {
        return trans.rect.size;
    }
    public static float GetWidth(this RectTransform trans)
    {
        return trans.rect.width;
    }
    public static float GetHeight(this RectTransform trans)
    {
        return trans.rect.height;
    }

    public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
    }

    public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }
    public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }
    public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }
    public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }

    public static void SetSize(this RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }
    public static void SetWidth(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }
    public static void SetHeight(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }
}



public static class GameUtils
{
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> enumerable, System.Func<T, TKey> keySelector)
    {
        return enumerable.GroupBy(keySelector).Select(grp => grp.First());
    }

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
        Vector2 dir = new Vector2(where.y - whom.position.y, where.x - whom.position.x).normalized;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg ;
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle+ offset);
        whom.rotation = Quaternion.Lerp(whom.rotation, total, time);
        if (Vector2.Distance(new Vector2(Mathf.Abs(total.z), 0), new Vector2(Mathf.Abs(whom.rotation.z), 0)) <= distanceToComplete)
        {
            onComplete();
            return;
        }
    }
    public static void LookAt2DSmooth(Transform whom, Vector3 where, float offset, float time)
    {
        float angle = Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg ;
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle+ offset);
        whom.rotation = Quaternion.Lerp(whom.rotation, total, time);
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