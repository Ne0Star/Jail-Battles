using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //[SerializeField] private int sourcesInitialCount;
    //[SerializeField] private List<AudioSource> sources;

    //private Dictionary<int, GameObject> sourcesDict = new Dictionary<int, GameObject>();

    //private void Awake()
    //{
    //    for (int i = 0; i < sourcesInitialCount; i++)
    //    {
    //        GameObject go = new GameObject();
    //        sourcesDict.Add(go.GetInstanceID(), go);
    //        go.transform.parent = transform;
    //        sources.Add(go.AddComponent<AudioSource>());
    //        go.SetActive(false);
    //    }
    //}


    //private AudioSource GetFree()
    //{
    //    foreach (AudioSource source in sources)
    //    {
    //        if (source && !source.gameObject.activeSelf)
    //        {
    //            source.gameObject.SetActive(true);
    //            return source;
    //        }
    //    }
    //    return null;
    //}

    //public void CloseSource(int id)
    //{
    //    sourcesDict.TryGetValue(id, out GameObject go);
    //    if(go)
    //    {
    //        go.SetActive(false);
    //    }
    //}




    ///// <summary>
    ///// Вернёт id по которому при окончании нужно будет закрыть источник
    ///// </summary>
    ///// <param name="worldPosition"></param>
    ///// <param name="clip"></param>
    ///// <returns></returns>
    //public void PlayOneShot(Vector3 worldPosition, AudioClip clip)
    //{
    //    if (!clip) return;
    //    AudioSource source = GetFree();
    //    if (!source) return;
    //    source.transform.position = worldPosition;
    //    source.PlayOneShot(clip);
    //    source.pitch = Random.Range(0.5f, 1.2f);
    //    //return source.gameObject.GetInstanceID();
    //}
}
