using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Менеджер очередей
/// </summary>
public class InLineManager : MonoBehaviour
{
    [Header("Очереди должны присутствовать на сцене сразу-же")]
    [SerializeField] private InLine[] inlines;

    private void Awake()
    {
        inlines = FindObjectsOfType<InLine>();
    }

    public InLine GetInline(InlineType type)
    {
        InLine result = null;
        foreach(InLine line in inlines)
        {
            if(line.Type == type)
            {
                result = line;
                break;
            }
        }
        return result;
    }

}
