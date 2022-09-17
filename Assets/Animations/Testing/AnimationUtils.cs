using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(fileName = "Animation DATA")]
public class AnimationData : ScriptableObject
{
    [SerializeField] private string relativePath;



    [SerializeField] private List<Sprite> sprites;
    public List<Sprite> Sprites { get => sprites; set => sprites = value; }
}


[CustomEditor(typeof(AnimationUtils))]
public class AnimationEditor : Editor
{
    AnimationUtils tt;
    Camera cam;
    private void OnEnable()
    {
        tt = (AnimationUtils)target;
    }

    public override void OnInspectorGUI()
    {
        var FileName = serializedObject.FindProperty("fileName");
        var FilePath = serializedObject.FindProperty("relativePath");
        var StateName = serializedObject.FindProperty("stateName");
        var Animator = serializedObject.FindProperty("animator");
        var Camera = serializedObject.FindProperty("screenCamera");
        EditorGUILayout.PropertyField(FileName);
        EditorGUILayout.PropertyField(FilePath);
        EditorGUILayout.PropertyField(StateName);
        EditorGUILayout.PropertyField(Animator);
        EditorGUILayout.PropertyField(Camera);
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Начать запись"))
        {

            DelayUseAsync(250, (Camera)Camera.GetValue(), (string)FilePath.GetValue());
            // StartCoroutine(Test());
        }
    }
    async static void DelayUseAsync(int timeStep, Camera cam, string path)
    {
        for (int i = 0; i < 5; i++)
        {
            Sprite s = ToTexture2D(cam);
            SaveSpriteToEditorPath(s, path);
            await Task.Delay(timeStep);

        }

    }
    public static Sprite ToTexture2D(Camera cam)
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
        return s;
    }

    public static void SaveSpriteToEditorPath(Sprite sprite, string proj_path)
    {
        var abs_path = Path.Combine(Application.dataPath, proj_path);
        proj_path = Path.Combine("Assets", proj_path);

        Directory.CreateDirectory(Path.GetDirectoryName(abs_path));
        File.WriteAllBytes(abs_path, ImageConversion.EncodeToPNG(sprite.texture));

        AssetDatabase.Refresh();

        var ti = AssetImporter.GetAtPath(proj_path) as TextureImporter;
        ti.spritePixelsPerUnit = sprite.pixelsPerUnit;
        ti.mipmapEnabled = false;
        ti.textureType = TextureImporterType.Sprite;

        //EditorUtility.SetDirty(ti);
        //ti.SaveAndReimport();


        //var r = AssetDatabase.LoadAssetAtPath<Sprite>(proj_path);
        //return r;
    }
}

public class AnimationUtils : MonoBehaviour
{
    [SerializeField] private bool startRecord = false;
    [SerializeField] private bool block = false;
    [SerializeField] private Camera screenCamera;
    [SerializeField] private List<Sprite> results = new List<Sprite>();

    [Header("Префикс названия .Png")]
    [SerializeField] private string fileName;
    [Header("Путь относительно папки Assets, если указана несуществующая папка она будет создана")]
    [SerializeField] private string relativePath;


    [Header("Анимация для раскадровки: ")]
    [Space(2)]
    [Header("Название анимации в Animator")]
    [SerializeField] private string stateName;
    [Header("Animator")]
    [SerializeField] private Animator animator;

    //private void Awake()
    //{
    //    animator = gameObject.GetComponent<Animator>();

    //}

    //private IEnumerator Start()
    //{
    //    results.Clear();
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
    //            results[i] = SaveSpriteToEditorPath(s, relativePath + "/" + fileName + "_" + i + ".png");
    //            yield return new WaitForEndOfFrame();
    //        }
    //    }
    //}

    //public void StartRecord()
    //{
    //    if (block) return;
    //    isRecording = true;
    //    StartCoroutine(Recording());
    //}


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
    public void tt()
    {
        StartCoroutine(Test());
    }
    private IEnumerator Test()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("test");
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnDrawGizmos()
    {
        if (!screenCamera) screenCamera = GameObject.FindGameObjectWithTag("ScreenCamera").GetComponent<Camera>();

        if (startRecord)
        {
            StartCoroutine(Test());
            startRecord = false;
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
}