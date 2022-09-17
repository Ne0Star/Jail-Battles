using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CustomCollider : MonoBehaviour
{
    /// <summary>
    /// true если эта точка в радиусе коллайдера
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public abstract bool CheckPoint(Vector3 worldPos);
}



public class SegmentTrigger : MonoBehaviour
{


    //[SerializeField] private int countStep; // Количество индексов которые будут проходится в 1 фрейм
    //[SerializeField] private int lastCount = 0;
    //[SerializeField] private int currentEnd = 0;

    public Vector3Int SegmentCount;
    public Vector3 Size;

    public SegmentCollider[] Triggers;

    //public int SearchCount;

    //public float JobDuration;
    //public float SearchColdown;


    //[SerializeField] private bool share;


    //private void Life_(int start, int end)
    //{
    //    for (int i = start; i < end; i++)
    //    {
    //        Enemu e = LevelManager.Instance.EnemuManager.GetAllEnemies()[i];
    //        for (int t = 0; t < Triggers.Length; t++)
    //        {
    //            Triggers[t].CheckEnemu(e);
    //        }
    //    }

    //}

    //private IEnumerator Life()
    //{
    //    CompleteSearch = false;
    //    List<Enemu> targets = LevelManager.Instance.EnemuManager.GetAllEnemies();
    //    int t = lastCount;
    //    int r = Mathf.Clamp(t + countStep, 0, targets.Count);
    //    for (int i = t; i < r; i++)
    //    {
    //        Enemu e = targets[i];
    //        for (int j = 0; j < Triggers.Length; j++)
    //        {
    //            Triggers[j].CheckEnemu(e);

    //        }
    //        yield return new WaitForFixedUpdate();
    //        t++;
    //    }
    //    lastCount = t;
    //    if (lastCount >= targets.Count - 1)
    //    {
    //        lastCount = 0;
    //    }
    //    yield return new WaitForSeconds(SearchColdown);
    //    CompleteSearch = true;
    //}

    public Vector3 clickPos;

    [SerializeField] private bool CompleteSearch = true;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos = worldPos;
            SegmentCollider segment = GetSegmentByPoint(worldPos);
            if (segment && segment.Collider)
            {
                GeneratorController gc = FindObjectOfType<GeneratorController>();
                //StartCoroutine(gc.GenerateBlock(segment));
            }
            //Debug.Log("Клик по: " + segment + " в позиции: " + segment.Position + " сущностей в блоке: ");
        }
    }

    /// <summary>
    /// Находит все сущности в радиусе коллайдера
    /// </summary>


    /// <summary>
    /// Возвращает сегмент если он существует
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public SegmentCollider GetSegmentByPoint(Vector3 position)
    {
        SegmentCollider result = null;
        for (int i = 0; i < Triggers.Length; i++)
        {
            if (Triggers[i])
            {
                if (Triggers[i].Collider.OverlapPoint(position))
                {
                    result = Triggers[i];
                    return result;
                }
            }
        }
        return result;
    }
    /// <summary>
    /// true если точка пересекает любой из сегментов
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool CheckPoint(Vector3 position)
    {
        bool result = false;
        for (int i = 0; i < Triggers.Length; i++)
        {
            if (Triggers[i])
            {
                if (Triggers[i].Collider.OverlapPoint(position))
                {
                    result = true;
                    return result;
                }
            }
        }
        return result;
    }


    /// <summary>
    /// true если точка пересекает любой из сегментов
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public BoxCollider2D[] CheckPointAABB(BoxCollider2D collider)
    {
        List<BoxCollider2D> result = new List<BoxCollider2D>();
        for (int i = 0; i < Triggers.Length; i++)
        {
            if (Triggers[i])
            {
                Rect current = new Rect((Vector2)Triggers[i].Collider.transform.position - (Triggers[i].Collider.size / 2), Triggers[i].Collider.size);
                Rect checker = new Rect((Vector2)collider.transform.position - (collider.size / 2), collider.size);
                if (current.Overlaps(checker))
                {
                    result.Add(Triggers[i].Collider);
                }
            }
        }
        return result.ToArray();
    }

    private void OnDrawGizmos()
    {
        int ind = Mathf.RoundToInt(SegmentCount.x * SegmentCount.y);
        if (Triggers == null || Triggers.Length < ind || Triggers.Length > ind)
        {
            if (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
            Triggers = new SegmentCollider[ind];

            GameObject parent = new GameObject("Triggers Parent");
            parent.transform.parent = transform;

            for (int i = 0; i < Triggers.Length; i++)
            {
                if (Triggers[i] == null)
                {

                    GameObject trigger = new GameObject("Trigger: " + i);
                    trigger.transform.parent = parent.transform;

                    Triggers[i] = trigger.AddComponent<SegmentCollider>();
                    Triggers[i].Collider = trigger.AddComponent<BoxCollider2D>();
                }
            }
        }

        float widthPerSegment = Size.x / SegmentCount.x;
        float heightPerSegment = Size.y / SegmentCount.y;

        int it = -1;
        for (int i = 0; i < SegmentCount.x; i++)
        {
            for (int j = 0; j < SegmentCount.y; j++)
            {
                it++;
                int index = Mathf.Clamp(it, 0, Triggers.Length - 1);
                Vector3 offset = new Vector3(Size.x * SegmentCount.x / 2 - (Size.x / 2), Size.y * SegmentCount.y / 2 - (Size.y / 2), 0);
                if (Triggers[index])
                {
                    Triggers[index].Collider.size = new Vector3(Size.x, Size.y);
                    Triggers[index].Position = index;
                    Triggers[index].transform.position = transform.position + (new Vector3(Size.x * i, Size.y * j) - offset);
                }
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(clickPos.x, clickPos.y, 0), 0.1f);
    }
}
