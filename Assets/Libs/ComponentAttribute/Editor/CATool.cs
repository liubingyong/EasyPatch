#define CATOOL_AUTO_SAVE

using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class CATool : MonoBehaviour
{
    [MenuItem("Tools/ComponentAttribute/Load All &2")]
    static void LoadAllComponents()
    {
        var activeScene = SceneManager.GetActiveScene();

        foreach (var rootGameObject in activeScene.GetRootGameObjects())
        {
            foreach (var behaviour in rootGameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                behaviour.LoadComponents();
            }
        }

        EditorSceneManager.MarkSceneDirty(activeScene);
#if CATOOL_AUTO_SAVE
        EditorSceneManager.SaveScene(activeScene);
#endif
    }

    [MenuItem("Tools/ComponentAttribute/Load &1")]
    static void NewNestedOption()
    {
        foreach (var selection in Selection.GetTransforms(SelectionMode.Unfiltered))
        {
            Debug.Log(selection);

            foreach (var behaviour in selection.GetComponentsInChildren<MonoBehaviour>())
            {
                behaviour.LoadComponents();
            }
        }

        var activeScene = SceneManager.GetActiveScene();

        EditorSceneManager.MarkSceneDirty(activeScene);
#if CATOOL_AUTO_SAVE
        EditorSceneManager.SaveScene(activeScene);
#endif
    }
}
