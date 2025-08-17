using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Singleton<MainMenu>
{
    // Variables
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
        LevelManager.Instance.LoadLevel("Sandbox");
    }

    private void OpenSettings()
    {
        SettingsManager.Instance.OpenMenu();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
