using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Variables
    public int Score { get; private set; }


    // Functions
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        SettingsMenu.Instance.OpenMenu();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
