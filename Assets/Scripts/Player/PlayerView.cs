using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

/// <summary>
/// Отображает игрока в меню
/// </summary>
public class PlayerView : MonoBehaviour
{
    [SerializeField] SpriteLibrary lib;
    [SerializeField] SpriteResolver[] resolvers;

    private void Awake()
    {
        if (!lib) lib = GetComponent<SpriteLibrary>();
        resolvers = GetComponentsInChildren<SpriteResolver>(true);
    }
    private void Start()
    {
        UpdateSkins();
    }

    private void OnEnable()
    {
        UpdateSkins();
    }

    public void SetPreview(string category, string label)
    {
        foreach(SpriteResolver resolver in resolvers)
        {
            if(resolver.GetCategory() == category)
            {
                
                resolver.SetCategoryAndLabel(category,label);
            }
        }
    }

    public void UpdateSkins()
    {
        foreach (SpriteResolver resolver in resolvers)
        {
            string category = "";
            string label = "";
            foreach (string s in lib.spriteLibraryAsset.GetCategoryLabelNames(resolver.GetCategory()))
            {
                label = s;
                category = resolver.GetCategory();
                if (category == "Тело и руки")
                {
                    if (s == YG.YandexGame.savesData.body)
                    {
                        label = s;
                        break;
                    }
                }
                else if (category == "Голова")
                {
                    if (s == YG.YandexGame.savesData.head)
                    {
                        label = s;
                        break;
                    }
                }
                else if (category == "Ноги")
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
