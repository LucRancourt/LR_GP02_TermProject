using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Variables



    // Functions
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        SettingsManager.Instance.OpenMenu();
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }
}
