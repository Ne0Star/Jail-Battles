using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
//public class AIAction
//{

//    private AIActionType actionType;

//    public AIActionType ActionType { get => actionType; set => actionType = value; }

//    public enum AIActionType
//    {
//        Бежать
//    }

//}
///// <summary>
///// Отвечает за движение куда-либо
///// </summary>
//[System.Serializable]
//public class MoveAction : AIAction
//{
//    public bool moveToTarget;
//    public Transform target;
//    public Vector3 targetPosition;
//}



//[CustomEditor(typeof(AIConsistency))]
//public class AIActionEditor : Editor
//{
//    private AIConsistency data;
//    public override void OnInspectorGUI()
//    {
//        data = (AIConsistency)target;

//        var Actions = serializedObject.FindProperty("actions");
//        //var _Actions = EditorGUILayout.PropertyField(Actions);

//        GUIStyle scrollSkin = new GUIStyle(GUI.skin.box);
//        GUIStyle toggleSkin = new GUIStyle(GUI.skin.button);



//        serializedObject.Update();

//        List<AIAction> actions = (List<AIAction>)Actions.GetValue();

//        if (actions.Count > 0)
//        {
//            GUILayout.BeginHorizontal();
//            data.showList = GUILayout.Toggle(data.showList, "Показать");
//            GUILayout.Label("Список действий: ");
//            GUILayout.EndHorizontal();
//        }
//        else
//        {
//            GUILayout.Label("Список действий пуст");
//            if (GUILayout.Button("Добавить"))
//            {
//                actions.Add(new AIAction());
//            }
//            return;
//        }


//        if (data.showList)
//        {
//            GUILayout.BeginScrollView(new Vector2(200f, 0.5f), scrollSkin);
//            for (int i = 0; i < actions.Count; i++)
//            {
//                AIAction action = actions[i];

//                GUILayout.BeginHorizontal();
//                action.ActionType = (AIAction.AIActionType)EditorGUILayout.EnumPopup(action.ActionType);

//                switch (action.ActionType)
//                {
//                    case AIAction.AIActionType.Бежать:
//                        actions[i] = new MoveAction();
//                        break;
//                }
// actions[i].ActionType = action.ActionType;
//                if (GUILayout.Button("X"))
//                {
//                    actions.RemoveAt(i);
//                }
//                GUILayout.EndHorizontal();
//                action.DrawEditor();
//                //GUILayout.Box("123123", scrollSkin);
//            }
//            GUILayout.EndScrollView();
//        }
//        if (GUILayout.Button("Добавить"))
//        {
//            actions.Add(new AIAction());
//        }




//        serializedObject.ApplyModifiedProperties();


//        if (GUI.changed)
//        {
//            data.actions = actions;
//            EditorUtility.SetDirty(target);
//        }


//    }
//}

/// <summary>
/// Набор действий AI
/// </summary>
public class AIConsistency : AIEvents
{

    [SerializeField] private AITypes applyTypes;
    /// <summary>
    /// Типы AI которые могут принять данный сценарий
    /// </summary>
    public AITypes ApplyTypes { get => applyTypes; }


    [SerializeField] private bool free;
    [SerializeField] private List<AIAction> actions;
    [SerializeField] private System.Action onStart;


    /// <summary>
    /// Свободен ли данный сценарий для выполнения
    /// </summary>
    public bool Free { get => CheckFree(); set => free = value; }


    private bool CheckFree()
    {
        bool tempFree = true;
        foreach (AIAction action in actions)
        {
            if (!action.Free)
            {
                tempFree = false;
                break;
            }
        }
        return tempFree;
    }


    private void SetFree(bool val)
    {
        free = val;
        foreach (AIAction action in actions)
        {
            if (!val)
            {
                action.Block();
            }
        }
    }

    /// <summary>
    /// Запускает сценарий для данного AI
    /// </summary>
    /// <param name="onComplete"></param>
    public void StartConsisstency(AI ai, System.Action onComplete)
    {
        if (!free) return;
        SetFree(false);
        StartCoroutine(StartActions(ai, onComplete));
    }
    [SerializeField] int index = 0;
    private IEnumerator StartActions(AI ai, System.Action onComplete)
    {
        index = 0;
        foreach (AIAction action in actions)
        {
            bool next = false;
            action.StartAction(ai, () => next = true);
            yield return new WaitUntil(() => next);
            index++;
        }
        Debug.Log("Сценарий завершён");
        SetFree(true);
        onComplete?.Invoke();
        yield break;
    }

}
