using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class CopyInspector : Editor
{
    static System.Type m_OriginalType;
    static Dictionary <PropertyInfo, object> m_Values;

    private List <PropertyInfo> GetProperties (Component component)
    {
        List <string> ignoredProperties;
        List <PropertyInfo> properties;

        properties = new List <PropertyInfo> ();
        ignoredProperties = new List <string> ();
        foreach (PropertyInfo propertyInfo in typeof (Component).GetProperties ())
        {
            ignoredProperties.Add (propertyInfo.Name);
        }

        foreach (PropertyInfo propertyInfo in component.GetType ().GetProperties ())
        {
            if (ignoredProperties.Contains (propertyInfo.Name))
            {
                continue;
            }
            properties.Add (propertyInfo);
        }

        return properties;
    }

    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector ();
        OnCopyInspectorGUI ();
    }

    public void OnCopyInspectorGUI ()
    {
        bool enabled;
        List <PropertyInfo> properties;
        Component component;

        component = target as Component;

        if (component == null)
        {
            return;
        }

        GUILayout.Space (10.0f);

        Color backgroundColor = GUI.backgroundColor;

        GUI.backgroundColor = new Color (0.8f, 0.8f, 0.8f);

        GUILayout.BeginVertical ("Toolbar");

            GUI.backgroundColor = backgroundColor;

            GUILayout.BeginHorizontal ();

                GUILayout.Space (10.0f);

                GUILayout.Label ("Copied: " + (m_OriginalType != null ? m_OriginalType.Name : "Nothing"), "MiniLabel");

                GUILayout.FlexibleSpace ();

                if (GUILayout.Button (new GUIContent ("Copy", "Copy component values"), "MiniLabel"))
                {
                    m_OriginalType = target.GetType ();

                    properties = GetProperties (component);

                    m_Values = new Dictionary <PropertyInfo, object> ();
                    foreach (PropertyInfo property in properties)
                    {
                        m_Values [property] = property.GetValue (component, null);
                    }
                }

                enabled = GUI.enabled;
                GUI.enabled = target.GetType () == m_OriginalType;

                GUILayout.Space (10.0f);

                if (GUILayout.Button (new GUIContent ("Paste", "Paste component values"), "MiniLabel"))
                {
                    properties = GetProperties (component);
                    foreach (PropertyInfo property in properties)
                    {
                        if (!property.CanWrite)
                        {
                            continue;
                        }

                        property.SetValue (component, m_Values [property], null);
                    }
                }

                GUILayout.Space (10.0f);

                GUI.enabled = enabled;

            GUILayout.EndHorizontal ();

        GUILayout.EndVertical ();

        GUILayout.Space (-2.0f);
    }
}