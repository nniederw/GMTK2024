using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverOverlay;
    private static GameManager ActiveGameManager;
    private void Start()
    {
        if (GameOverOverlay == null) throw new Exception($"{nameof(GameOverOverlay)} is null on {nameof(GameManager)}");
        ActiveGameManager = this;
        GameOverOverlay.SetActive(false);
    }
    public static void GameOver()
    {
        ActiveGameManager.GameOverOverlay.SetActive(true);
        ActiveGameManager.Invoke(nameof(StopTime), 0.25f);
        ActiveGameManager.Invoke(nameof(StopTime), 0.25f + 0.25f * 0.5f);
    }
    private void ReduceTime()
    {
        Time.timeScale = 0.5f;
    }
    private void StopTime()
    {
        Time.timeScale = 0f;
    }
}