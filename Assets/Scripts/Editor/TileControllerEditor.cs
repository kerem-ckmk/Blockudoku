using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileController))]
public class TileControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileController tileController = (TileController)target;

        bool hasChanged = false;

        for (int r = 0; r < tileController.rowCount; r++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int c = 0; c < tileController.columnCount; c++)
            {
                bool oldValue = tileController.shape[r].Collection[c];
                tileController.shape[r].Collection[c] = EditorGUILayout.Toggle(oldValue, GUILayout.Width(15), GUILayout.Height(15));
                if (tileController.shape[r].Collection[c] != oldValue)
                {
                    hasChanged = true;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        if (hasChanged)
        {
            EditorUtility.SetDirty(target);
            PrefabUtility.RecordPrefabInstancePropertyModifications(target);
        }
    }
}