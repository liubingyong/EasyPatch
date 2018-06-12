using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CATool : MonoBehaviour
{
    [MenuItem("Tools/ComponentAttribute/Load All %#l")]
    static void LoadAllComponents()
    {
        foreach(var rootGameObject in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            foreach (var behaviour in rootGameObject.GetComponentsInChildren<MonoBehaviour>())
            {
                behaviour.LoadComponents();
            }
        }
    }

    [MenuItem("Tools/ComponentAttribute/Load %l")]
    static void NewNestedOption()
    {
        foreach (var selection in Selection.GetTransforms(SelectionMode.Unfiltered))
        {
            foreach (var behaviour in selection.GetComponentsInChildren<MonoBehaviour>())
            {
                behaviour.LoadComponents();
            }
        }
    }
}
