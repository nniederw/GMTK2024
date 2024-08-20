using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager :MonoBehaviour
{
    private const string MainScene = "Main";
    private const string TutorialScene = "Tutorial";
    private const string TitleScene = "Title";
    public static void LoadMainScene() => LoadScene(MainScene);
    public static void LoadTutorialScene() => LoadScene(TutorialScene);
    public static void LoadTitleScene() => LoadScene(TitleScene);
    private static void LoadScene(string name) => UnityEngine.SceneManagement.SceneManager.LoadScene(name);
}