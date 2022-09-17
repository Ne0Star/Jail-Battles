using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
//[CreateAssetMenu(fileName = "Animation DATA")]
//public class AnimationData : ScriptableObject
//{
//    [SerializeField] private List<Sprite> sprites;

//    public List<Sprite> Sprites { get => sprites; set => sprites = value; }
//}

public class AnimationUtils : MonoBehaviour
{
    //[SerializeField] private bool block = false;
    //[SerializeField] private Camera screenCamera;
    //[SerializeField] private List<Sprite> results = new List<Sprite>();
    //[SerializeField] private string stateName;
    //[SerializeField] private Animator animator;
    //[SerializeField] private AnimationData saveTo; // Данные для сохранения результатов

    //private void Awake()
    //{
    //    animator = gameObject.GetComponent<Animator>();

    //}

    //private IEnumerator Start()
    //{
    //    results.Clear();
    //    saveTo.Sprites.Clear();
    //    yield return new WaitForSeconds(2f);
    //    animator.Play(stateName);
    //}
    //[Range(0.02f, 1f)]
    //[SerializeField] private float frameStep;
    //[SerializeField] private bool isRecording = false;

    //private IEnumerator Recording()
    //{
    //    while (isRecording)
    //    {
    //        StartCoroutine(CreateFrame());

    //        yield return new WaitForSeconds(frameStep);
    //    }
    //    saveTo.Sprites = results;
    //}

    //private IEnumerator CreateFrame()
    //{
    //    StartCoroutine(ToTexture2D(screenCamera, (s) => results.Add(s)));
    //    //Sprite result = ToTexture2D(screenCamera, "Frame" + results.Count);
    //    //yield return new WaitUntil(() => result);
    //    //results.Add(result);
    //    yield break;
    //}

    //public void StopRecord()
    //{
    //    isRecording = false;

    //    StartCoroutine(SaveResult());

    //}

    //private IEnumerator SaveResult()
    //{
    //    for (int i = 0; i < results.Count; i++)
    //    {
    //        Sprite s = results[i];
    //        if (s && s != null)
    //        {
    //            results[i] = SaveSpriteToEditorPath(s, "Animations/Editor/" + stateName + "" + i + "" + name + ".png");
    //            yield return new WaitForEndOfFrame();
    //        }
    //    }
    //    saveTo.Sprites = results;
    //}

    //public void StartRecord()
    //{
    //    if (block) return;
    //    isRecording = true;
    //    StartCoroutine(Recording());
    //}


    //private IEnumerator ToTexture2D(Camera cam, System.Action<Sprite> onComplete)
    //{
    //    RenderTexture rTex = cam.targetTexture;
    //    RenderTexture currentActiveRT = RenderTexture.active;
    //    RenderTexture.active = rTex;
    //    cam.Render();
    //    Rect rect = new Rect(0, 0, rTex.width, rTex.height);
    //    Texture2D tex = new Texture2D(rTex.width, rTex.height);
    //    tex.ReadPixels(rect, 0, 0);
    //    tex.Apply();
    //    RenderTexture.active = currentActiveRT;
    //    Sprite s = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
    //    yield return new WaitUntil(() => s);
    //    onComplete(s);
    //    //return s;// SaveSpriteToEditorPath(s, "Animations/Editor/" + stateName + "_" + name + ".png");
    //}

    //Sprite SaveSpriteToEditorPath(Sprite sprite, string proj_path)
    //{
    //    var abs_path = Path.Combine(Application.dataPath, proj_path);
    //    proj_path = Path.Combine("Assets", proj_path);

    //    Directory.CreateDirectory(Path.GetDirectoryName(abs_path));
    //    File.WriteAllBytes(abs_path, ImageConversion.EncodeToPNG(sprite.texture));

    //    AssetDatabase.Refresh();

    //    var ti = AssetImporter.GetAtPath(proj_path) as TextureImporter;
    //    Debug.Log(sprite);
    //    ti.spritePixelsPerUnit = sprite.pixelsPerUnit;
    //    ti.mipmapEnabled = false;
    //    ti.textureType = TextureImporterType.Sprite;

    //    //EditorUtility.SetDirty(ti);
    //    //ti.SaveAndReimport();

    //    var r = AssetDatabase.LoadAssetAtPath<Sprite>(proj_path);
    //    return r;
    //}


    //private void OnDrawGizmos()
    //{
    //    if (!screenCamera) screenCamera = GameObject.FindGameObjectWithTag("ScreenCamera").GetComponent<Camera>();
    //}

}
