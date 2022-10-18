
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
# if UNITY_EDITOR
using System.Threading.Tasks;
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "Animation DATA")]
public class AnimationData : ScriptableObject
{
    [SerializeField] private string relativePath;
    [SerializeField] private List<Sprite> sprites;
    public List<Sprite> Sprites { get => sprites; set => sprites = value; }
}
public class AnimationUtils : MonoBehaviour
{

    public bool createSreenShoot = false;
    private bool startRecord = false;
    private bool block = false;
    public Camera screenCamera;
    public List<Sprite> results = new List<Sprite>();

    [Header("Префикс названия .Png")]
    public string fileName;
    [Header("Путь относительно папки Assets, если указана несуществующая папка она будет создана")]
    public string relativePath;
    [Header("Анимация для раскадровки: ")]
    [Space(1)]
    [Header("Название анимации в Animator")]
    public string stateName;
    [Header("Animator")]
    public Animator animator;
    [Range(0.02f, 1f)]
    public float frameStep;
    private bool isRecording = false;
#if UNITY_EDITOR
    private void Awake()
    {
        if (!animator)
            animator = gameObject.GetComponent<Animator>();

    }

    private IEnumerator Start()
    {
        results.Clear();
        yield return new WaitForSeconds(1f);
        animator.Play(stateName);
        StartRecord();
        yield return new WaitForEndOfFrame();
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("empty"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("empty"));
        StopRecord();
    }


    private IEnumerator Recording()
    {
        while (isRecording)
        {
            StartCoroutine(CreateFrame());

            yield return new WaitForSeconds(frameStep);
        }
    }

    private IEnumerator CreateFrame()
    {
        StartCoroutine(ToTexture2D(screenCamera, (s) => results.Add(s)));
        //Sprite result = ToTexture2D(screenCamera, "Frame" + results.Count);
        //yield return new WaitUntil(() => result);
        //results.Add(result);
        yield break;
    }

    public void StopRecord()
    {
        isRecording = false;

        StartCoroutine(SaveResult());

    }

    private IEnumerator SaveResult()
    {
        for (int i = 0; i < results.Count; i++)
        {
            Sprite s = results[i];
            if (s && s != null)
            {
                results[i] = SaveSpriteToEditorPath(s, relativePath + "/" + fileName + "_" + i + ".png");
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void StartRecord()
    {
        if (block) return;
        isRecording = true;
        StartCoroutine(Recording());
    }

    private IEnumerator ToTexture2D(Camera cam, System.Action<Sprite> onComplete)
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

    Sprite SaveSpriteToEditorPath(Sprite sprite, string proj_path)
    {
        var abs_path = Path.Combine(Application.dataPath, proj_path);
        proj_path = Path.Combine("Assets", proj_path);

        Directory.CreateDirectory(Path.GetDirectoryName(abs_path));
        File.WriteAllBytes(abs_path, ImageConversion.EncodeToPNG(sprite.texture));

        AssetDatabase.Refresh();

        var ti = AssetImporter.GetAtPath(proj_path) as TextureImporter;
        Debug.Log(sprite);
        ti.spritePixelsPerUnit = sprite.pixelsPerUnit;
        ti.mipmapEnabled = false;
        ti.textureType = TextureImporterType.Sprite;

        //EditorUtility.SetDirty(ti);
        //ti.SaveAndReimport();

        var r = AssetDatabase.LoadAssetAtPath<Sprite>(proj_path);
        return r;
    }

    private void OnDrawGizmos()
    {
        if (!screenCamera) screenCamera = GameObject.FindGameObjectWithTag("ScreenCamera").GetComponent<Camera>();
        if (createSreenShoot)
        {
            StartCoroutine(ToTexture2D(screenCamera, (s) => SaveSpriteToEditorPath(s, relativePath + "/" + fileName + "_" + "screenshot" + ".png")));
            createSreenShoot = false;
        }
    }

}
public class EditorCoroutine
{
    public static EditorCoroutine start(IEnumerator _routine)
    {
        EditorCoroutine coroutine = new EditorCoroutine(_routine);
        coroutine.start();
        return coroutine;
    }

    readonly IEnumerator routine;
    EditorCoroutine(IEnumerator _routine)
    {
        routine = _routine;
    }

    void start()
    {
        //Debug.Log("start");
        EditorApplication.update += update;
    }
    public void stop()
    {
        //Debug.Log("stop");
        EditorApplication.update -= update;
    }

    void update()
    {
        /* NOTE: no need to try/catch MoveNext,
         * if an IEnumerator throws its next iteration returns false.
         * Also, Unity probably catches when calling EditorApplication.update.
         */

        //Debug.Log("update");
        if (!routine.MoveNext())
        {
            stop();
        }
    }
#endif
}
