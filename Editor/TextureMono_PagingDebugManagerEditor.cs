using UnityEngine;
using UnityEditor;
using Eloi.TextureUtility;

[CustomEditor(typeof(TextureMono_PagingDebugManager))]
public class TextureMono_PagingDebugManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TextureMono_PagingDebugManager manager = (TextureMono_PagingDebugManager)target;

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Previous"))
        {
            manager.PreviousPage();
        }

        if (GUILayout.Button("Next"))
        {
            manager.NextPage();
        }

        if (GUILayout.Button("Fetch Pages"))
        {
            manager.FetchPagesFromChildren();
        }

        EditorGUILayout.EndHorizontal();
        DrawDefaultInspector();

    }
}
