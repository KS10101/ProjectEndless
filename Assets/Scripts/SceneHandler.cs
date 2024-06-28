using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{

    public static SceneHandler instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public enum SceneSelect
    {
        MainMenu,
        Level_01,
    }

    public SceneSelect sceneSelect;
}
