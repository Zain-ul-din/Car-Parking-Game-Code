using UnityEngine.SceneManagement;
using UnityEngine;

public static class SceneLoader {
    [HideInInspector] public static bool HideUI = false;
    // Name of All Scene
    public enum Scene{
        MainScene,
        LevelSelection,
        ShowRoom,
    }

    public static void LoadScene(Scene scene){
        SceneManager.LoadScene(scene.ToString());
        if (scene == Scene.ShowRoom)
            HideUI = true;
    }
}
