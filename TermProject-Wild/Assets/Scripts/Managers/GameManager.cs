using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Variables
    public int Score { get; private set; }


    // Functions
    private void Start()
    {
        GameData gameData = SaveManager.Instance.LoadGame();
        Score = gameData.PlayerScore;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        SettingsMenu.Instance.OpenMenu();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }

    public void SetScore(int score)
    {
        Score = score;
        InGameMenu.Instance.SetScore(Score);
    }

    public void AddScore(int amount)
    {
        Score += amount;
        InGameMenu.Instance.SetScore(Score);
    }
}
