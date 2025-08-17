using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Singleton<MainMenu>
{
    // Variables
    [Header("ClickSFX")]
    [SerializeField] private AudioClip clickSFX;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;


    // Functions
    protected override void Awake()
    {
        base.Awake();

        playButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);
    }


    private void StartGame()
    {
        LevelManager.Instance.LoadLevel("GameScene");
    }

    private void OpenSettings()
    {
        SettingsManager.Instance.OpenMenu();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PlayClickSFX()
    {
        AudioManager.Instance.PlaySoundEffect(clickSFX);
    }
}
