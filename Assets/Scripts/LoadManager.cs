using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadManager
{
    public enum Scene
    {
        MainMenu,
        Loading,
        GameScene,
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        LoadManager.targetScene = targetScene;

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
