using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CATool : MonoBehaviour
{
    [MenuItem("Tools/ComponentAttribute/Load All")]
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
}
