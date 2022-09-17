using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Инициаализирует уровень, загружается самым первым
/// </summary>
public class LevelSetup : OneSingleton<LevelSetup>
{

    [SerializeField] private List<string> bakedAnimation;

    [SerializeField] private EnemuManager enemuManager;
    [SerializeField] private AnimationCreator[] animationCreator;
    [SerializeField] private float progress;
    [SerializeField] private int completes = 0;
    public Transform testing;
    //[SerializeField] private int max = 0;
    private void Start()
    {
        //Time.timeScale = 4f;
        Application.targetFrameRate = 60;
        enemuManager = FindObjectOfType<EnemuManager>();
        //animationCreator = FindObjectsOfType<AnimationCreator>();

        StartCoroutine(WaitComplete());
        for (int i = 0; i < animationCreator.Length; i++)
        {
            StartCoroutine(Initital(animationCreator[i], i));
        }
    }

    private IEnumerator WaitComplete()
    {
        //yield return new WaitUntil(() => enemuManager.AllEnemies != null);
        while (progress < 100f)
        {
            if (enemuManager != null)
                progress = completes / enemuManager.AllEnemies.Length * 100f;


            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.position = testing.position;
        Camera.main.orthographicSize = 2.3f;
        Debug.Log("Complete ");
        //Time.timeScale = 1f;
        Application.targetFrameRate = 60;
        for (int i = 0; i < animationCreator.Length; i++)
        {
            Destroy(animationCreator[i].transform.parent.gameObject);
        }
    }

    private IEnumerator Initital(AnimationCreator creator, int itsIndex)
    {
        creator.transform.parent.gameObject.SetActive(true);

        yield return new WaitUntil(() => enemuManager.AllEnemies != null);
        int index = itsIndex;
        while (index < enemuManager.AllEnemies.Length)
        {
            //max++;
            //Debug.Log(index + " " + enemuManager.AllEnemies[index]);
            //if (!enemuManager.AllEnemies[index]) yield break;
            bool complete = false;
            Enemu e = enemuManager.AllEnemies[index];

            creator.CreateAnimations(e, bakedAnimation, () => complete = true);
            yield return new WaitUntil(() => complete);
            completes++;
            index += (animationCreator.Length + 0);
        }
    }

}
