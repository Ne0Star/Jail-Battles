using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

/// <summary>
/// ���������� ������ � ����
/// </summary>
public class PlayerView : MonoBehaviour
{


    private void Awake()
    {

    }
    private void Start()
    {
        UpdateSkins();
    }

    public void UpdateSkins()
    {
        SpriteLibrary lib = GetComponent<SpriteLibrary>();
        SpriteResolver[] resolvers = GetComponentsInChildren<SpriteResolver>(true);

        foreach (SpriteResolver resolver in resolvers)
        {
            string category = "";
            string label = "";
            foreach (string s in lib.spriteLibraryAsset.GetCategoryLabelNames(resolver.GetCategory()))
            {
                label = s;
                category = resolver.GetCategory();
                if (category == "���� � ����")
                {
                    if(s == YG.YandexGame.savesData.body)
                    {
                        label = s;
                        break;
                    }
                }
                else if (category == "������")
                {
                    if (s == YG.YandexGame.savesData.head)
                    {
                        label = s;
                        break;
                    }
                }
                else if (category == "����")
                {
                    if (s == YG.YandexGame.savesData.leggs)
                    {
                        label = s;
                        break;
                    }
                }
            }
            resolver.SetCategoryAndLabel(category, label);
        }
    }

}
