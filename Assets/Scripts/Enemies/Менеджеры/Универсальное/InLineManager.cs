using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// �������� ��������
/// </summary>
public class InLineManager : MonoBehaviour
{
    [Header("������� ������ �������������� �� ����� �����-��")]
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
