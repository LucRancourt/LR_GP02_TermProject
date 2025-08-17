using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Singleton<PauseMenu>
{
    // Variables
    [Header("ClickSFX")]
    [SerializeField] private AudioClip clickSFX;

    [Header("Pause Settings")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Buttons")]
    [SerializeField] private Button resumeGame;
    [SerializeField] private Button openSettingsMenu;
    [SerializeField] private Button returnToMainMenu;

    private PlayerController caller;


    // Functions
    protected override void Awake()
    {
        base.Awake();

        pauseMenu.SetActive(false);

        resumeGame.onClick.AddListener(UnPauseGame);
        openSettingsMenu.onClick.AddListener(OpenSettingsMenu);
        returnToMainMenu.onClick.AddListener(ReturnToMainMenu);

        resumeGame.onClick.AddListener(PlayClickSFX);
        openSettingsMenu.onClick.AddListener(PlayClickSFX);
        returnToMainMenu.onClick.AddListener(PlayClickSFX);
    }


    public void PauseGame(PlayerController playerThatCalled)
    {
        caller = playerThatCalled;
        caller.SwitchCursorMode();

        pauseMenu.SetActive(true); 

        Time.timeScale = 0.0f;
    }

    public void UnPauseGame()
    {
        caller.SwitchCursorMode();
        caller = null;

        pauseMenu.SetActive(false);

        Time.timeScale = 1.0f;
    }

    private void OpenSettingsMenu()
    {
        SettingsMenu.Instance.OpenMenu();
    }

    private void ReturnToMainMenu()
    {
        UnPauseGame();
        LevelManager.Instance.LoadLevel("MainMenu");
    }

    private void PlayClickSFX()
    {
        AudioManager.Instance.PlaySoundEffect(clickSFX);
    }
}
