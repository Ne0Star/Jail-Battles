using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LabelOverride : PropertyAttribute

{

    public string label;
    public string description;
    public LabelOverride(string label)
    {
        this.label = label;
        this.description = label;
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LabelOverride))]
    public class ThisPropertyDrawer : PropertyDrawer
    {
        private bool fold;
        private bool isArray;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            try
            {
                isArray = IsItBloodyArrayTho(property);
                var propertyAttribute = this.attribute as LabelOverride;
                label.text = propertyAttribute.label;
                label.tooltip = propertyAttribute.label;
                EditorGUI.PropertyField(position, property, label);
            }
            catch (System.Exception ex) { Debug.LogException(ex); }
        }

        private string GetPropName(SerializedProperty property)
        {
            string path = property.propertyPath;
            int idot = path.IndexOf('.');
            string propName = path.Substring(0, idot);
            return propName;
        }
        private int GetIndex(SerializedProperty property)
        {
            string path = property.propertyPath;
            int idot = path.IndexOf('.');
            return idot;
        }


        bool IsItBloodyArrayTho(SerializedProperty property)
        {
            string propName = GetPropName(property);
            if (GetIndex(property) == -1) return false;
            SerializedProperty p = property.serializedObject.FindProperty(propName);
            return p.isArray;
            //CREDITS: https://answers.unity.com/questions/603882/serializedproperty-isnt-being-detected-as-an-array.html
        }
    }
#endif
}