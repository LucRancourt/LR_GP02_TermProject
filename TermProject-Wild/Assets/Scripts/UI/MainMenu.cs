using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Variables
    [Header("ClickSFX")]
    [SerializeField] private AudioClip clickSFX;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;


    // Functions
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(QuitGame);

        playButton.onClick.AddListener(PlayClickSFX);
        settingsButton.onClick.AddListener(PlayClickSFX);
        quitButton.onClick.AddListener(PlayClickSFX);
    }


    private void StartGame()
    {
        LevelManager.Instance.LoadLevel("GameScene");
    }

    private void OpenSettings()
    {
        SettingsMenu.Instance.OpenMenu();
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
