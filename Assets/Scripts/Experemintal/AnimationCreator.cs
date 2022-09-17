using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public struct EntitySkin
//{
//    [SerializeField] private List<EntitySkinData> datas;
//    public EntitySkin(List<EntitySkinData> datas)
//    {
//        this.datas = datas;
//    }
//    public List<EntitySkinData> Datas { get => datas; }
//    public void SetSkin(EntitySkin newSkin)
//    {
//        for (int i = 0; i < newSkin.datas.Count; i++)
//        {
//            if (i <= datas.Count - 1)
//                datas[i].renderer.sprite = newSkin.datas[i].sprite;
//        }
//    }
//}
[System.Serializable]
public struct AnimationState
{
    [SerializeField]
    private string name;
    [SerializeField]
    private List<Sprite> sprites;

    public AnimationState(string name, List<Sprite> sprites)
    {
        this.name = name;
        this.sprites = sprites;
    }

    public string Name { get => name; }
    public List<Sprite> Sprites { get => sprites; }
}

public class AnimationCreator : MonoBehaviour
{
    // Манекен
    [SerializeField] private Mannequin mannequin;
    // Камера для скриншотов
    [SerializeField] private Camera cam;

    [SerializeField] private bool record;

    public void AnimationComplete()
    {
        record = false;
    }

    public void CreateAnimations(Entity e, List<string> animations, System.Action OnComplete)
    {
        if (!e || animations == null) return;
        StartCoroutine(StartRecoording(e, animations, OnComplete));
    }

    private IEnumerator StartRecoording(Entity e, List<string> states, System.Action OnComplete)
    {
        List<AnimationState> animations = new List<AnimationState>();
        //Debug.Log(e.name + " " + transform.parent.name);
        for (int i = 0; i < states.Count; i++)
        {
            record = true;
            string stateName = states[i];


            mannequin.Animator.Play(stateName);
            List<Sprite> animation = new List<Sprite>();
            while (record)
            {
                bool complete = false;
                Sprite result = null;

                StartCoroutine(CreateTempSprite(cam, (s) =>
                {

                    complete = true;
                    result = s;
                    animation.Add(result);
                }));

                //yield return new WaitUntil(() => complete);
                //yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(e.AnimationController.AnimationsDutationsMultipler);
                //yield return new WaitUntil(() => complete);
                //test.Add(result);
            }

            animations.Add(new AnimationState(stateName, animation));
            for(int t =0; t < 2; t++)
            {
            yield return new WaitForEndOfFrame();
            }

        }
        e.AnimationController.SetAnimations(animations);
        //Debug.Log("фывфыв");
        OnComplete();

    }

    private IEnumerator CreateTempSprite(Camera cam, System.Action<Sprite> onComplete)
    {
        RenderTexture rTex = cam.targetTexture;
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = rTex;
        cam.Render();
        Rect rect = new Rect(0, 0, rTex.width, rTex.height);
        Texture2D tex = new Texture2D(rTex.width, rTex.height);
        tex.ReadPixels(rect, 0, 0);
        tex.Apply();
        RenderTexture.active = currentActiveRT;
        Sprite s = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
        yield return new WaitUntil(() => s);
        onComplete(s);
        //return s;// SaveSpriteToEditorPath(s, "Animations/Editor/" + stateName + "_" + name + ".png");
    }
}
