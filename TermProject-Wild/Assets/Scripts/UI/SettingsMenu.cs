using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Singleton<SettingsMenu>
{
    // Variables
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    [Header("ClickSFX")]
    [SerializeField] private AudioClip clickSFX;

    [Header("Settings UI")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button closeSettingsMenu;

    #region Volume Vars
    [Header("Volume")]
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private TextMeshProUGUI sfxVolumeText;
    [SerializeField] private Slider sfxVolumeSlider;
    
    private float _currentMusicVolume = 0.0f;
    private float _currentSFXVolume = 0.0f;
    #endregion


    // Functions
    private void Start()
    {
        closeSettingsMenu.onClick.AddListener(CloseMenu);
        closeSettingsMenu.onClick.AddListener(SaveSettings);
        closeSettingsMenu.onClick.AddListener(PlayClickSFX);

        musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);
            
        LoadSettings();
    }

    #region Save/Load
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, _currentMusicVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, _currentSFXVolume);
        PlayerPrefs.Save();

        UpdateMusicVolume(_currentMusicVolume);
    }

    private void LoadSettings()
    {
        _currentMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
        _currentSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey);

        musicVolumeSlider.value = _currentMusicVolume;
        sfxVolumeSlider.value = _currentSFXVolume;

        UpdateMusicVolume(_currentMusicVolume);
        UpdateSFXVolume(_currentSFXVolume);
    }
    #endregion

    public void OpenMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        settingsMenu.SetActive(false);
    }

    #region Volume Funcs
    private void UpdateMusicVolume(float value)
    {
        _currentMusicVolume = value;
        musicVolumeText.text = "Music Volume: " + (int)(_currentMusicVolume * 100.0f);

        AudioManager.Instance.SetMusicVolume(_currentMusicVolume);
    }

    private void UpdateSFXVolume(float value)
    {
        _currentSFXVolume = value;
        sfxVolumeText.text = "SFX Volume: " + (int)(_currentSFXVolume * 100.0f);

        AudioManager.Instance.SetSFXVolume(_currentSFXVolume);
    }
    #endregion


    private void PlayClickSFX()
    {
        AudioManager.Instance.PlaySoundEffect(clickSFX);
    }
}
