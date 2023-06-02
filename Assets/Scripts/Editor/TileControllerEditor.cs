using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileController))]
public class TileControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileController tileController = (TileController)target;

        // Þekil matrisini çizdir
        for (int r = 0; r < tileController.rowCount; r++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int c = 0; c < tileController.columnCount; c++)
            {
                tileController.Shape[r].Collection[c] = EditorGUILayout.Toggle(tileController.Shape[r].Collection[c], GUILayout.Width(15), GUILayout.Height(15));
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Update Shape"))
        {
            tileController.UpdateShape();
        }

        if (GUILayout.Button("Set All"))
        {
            for (int r = 0; r < tileController.rowCount; r++)
            {
                for (int c = 0; c < tileController.columnCount; c++)
                {
                    tileController.Shape[r].Collection[c] = true;
                }
            }
        }

        if (GUILayout.Button("Set None"))
        {
            for (int r = 0; r < tileController.rowCount; r++)
            {
                for (int c = 0; c < tileController.columnCount; c++)
                {
                    tileController.Shape[r].Collection[c] = false;
                }
            }
        }
    }
}