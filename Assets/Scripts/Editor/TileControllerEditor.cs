using UnityEditor;
using UnityEngine;
using static TileController;

[CustomEditor(typeof(TileController))]
public class TileControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TileController tileController = (TileController)target;

        if (tileController.columnCount == 0)
        {
            tileController.shape = new BoolCollection[tileController.rowCount];
            for (int i = 0; i < tileController.rowCount; i++)
            {
                tileController.shape[i] = new BoolCollection();
                tileController.shape[i].Collection = new bool[tileController.columnCount];
            }
        }

        for (int r = 0; r < tileController.rowCount; r++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int c = 0; c < tileController.columnCount; c++)
            {
                tileController.shape[r].Collection[c] = EditorGUILayout.Toggle(tileController.shape[r].Collection[c], GUILayout.Width(15), GUILayout.Height(15));
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Update Shape"))
        {
            tileController.CreateShape();
        }

        if (GUILayout.Button("Set All"))
        {
            for (int r = 0; r < tileController.rowCount; r++)
            {
                for (int c = 0; c < tileController.columnCount; c++)
                {
                    tileController.shape[r].Collection[c] = true;
                }
            }
        }

        if (GUILayout.Button("Set None"))
        {
            for (int r = 0; r < tileController.rowCount; r++)
            {
                for (int c = 0; c < tileController.columnCount; c++)
                {
                    tileController.shape[r].Collection[c] = false;
                }
            }
        }
    }
}