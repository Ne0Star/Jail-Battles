using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EntitySkinData
{
    public List<SpriteRenderer> renderers;
    public List<Sprite> sprites;
}

public class EntitySkin : MonoBehaviour
{
    public List<EntitySkinData> datas;
    public void SetSkin(EntitySkin skin)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            for (int j = 0; j < datas[i].renderers.Count; j++)
            {
                datas[i].renderers[j].sprite = skin.datas[i].sprites[j];
            }
        }
    }
}
