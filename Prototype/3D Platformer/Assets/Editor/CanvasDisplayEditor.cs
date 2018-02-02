using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanvasDisplayEditor : EditorWindow {

    [MenuItem("Window/Canvas Display")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CanvasDisplayEditor window = (CanvasDisplayEditor)EditorWindow.GetWindow(typeof(CanvasDisplayEditor));
        window.Show();
    }


    List<Canvas> canvases = new List<Canvas>();    

    public void OnEnable()
    {
   
    }

    bool hideInEditor = false;

    public void OnGUI()
    {
        Canvas[] c = GameObject.FindObjectsOfType<Canvas>();


        EditorGUILayout.BeginVertical("Box");

        GUILayout.Label("Canvases", EditorStyles.boldLabel);

        foreach (Canvas o in c)
        {
            if (hideInEditor)
                if (Application.isPlaying)
                    o.enabled = true;
                else o.enabled = false;

        }

        foreach (Canvas o in c)
        {
            EditorGUILayout.BeginHorizontal();
            o.gameObject.name = EditorGUILayout.TextField(o.gameObject.name, GUILayout.MaxWidth(128));
            o.enabled = EditorGUILayout.Toggle(o.enabled);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Hide In Editor", GUILayout.MaxWidth(128));
        hideInEditor = EditorGUILayout.Toggle(hideInEditor);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }  
}
