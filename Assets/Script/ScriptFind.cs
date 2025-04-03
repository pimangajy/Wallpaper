using UnityEngine;
using UnityEditor;

public class ScriptFind : EditorWindow
{
    private MonoScript scriptToFind;

    [MenuItem("Tools/Find Script Usage")]
    static void OpenWindow()
    {
        GetWindow<ScriptFind>("Find Script Usage");
    }

    void OnGUI()
    {
        GUILayout.Label("Find Objects Using a Script", EditorStyles.boldLabel);
        scriptToFind = (MonoScript)EditorGUILayout.ObjectField("Script", scriptToFind, typeof(MonoScript), false);

        if (GUILayout.Button("Find References"))
        {
            FindScriptReferences();
        }
    }

    void FindScriptReferences()
    {
        if (scriptToFind == null)
        {
            Debug.LogWarning("Please select a script.");
            return;
        }

        System.Type scriptType = scriptToFind.GetClass();
        if (scriptType == null)
        {
            Debug.LogWarning("Invalid script selected.");
            return;
        }

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent(scriptType) != null)
            {
                Debug.Log($"Script found on: {obj.name}", obj);
            }
        }
    }
}
