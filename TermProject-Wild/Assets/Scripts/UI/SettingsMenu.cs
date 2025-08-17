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
    
    private int _currentMusicVolume = 0;
    private int _currentSFXVolume = 0;
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
        PlayerPrefs.SetInt(MusicVolumeKey, _currentMusicVolume);
        PlayerPrefs.SetInt(SFXVolumeKey, _currentSFXVolume);
        PlayerPrefs.Save();

        UpdateMusicVolume(_currentMusicVolume);
    }

    private void LoadSettings()
    {
        _currentMusicVolume = PlayerPrefs.GetInt(MusicVolumeKey);
        _currentSFXVolume = PlayerPrefs.GetInt(SFXVolumeKey);

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
        _currentMusicVolume = (int)value;
        musicVolumeText.text = "Music Volume: " + _currentMusicVolume;

        //AudioManager.Instance.SetMusicVolume(_currentMusicVolume);
    }

    private void UpdateSFXVolume(float value)
    {
        _currentSFXVolume = (int)value;
        sfxVolumeText.text = "SFX Volume: " + _currentSFXVolume;

        //AudioManager.Instance.SetSFXVolume(_currentSFXVolume);
    }
    #endregion


    private void PlayClickSFX()
    {
        AudioManager.Instance.PlaySoundEffect(clickSFX);
    }
}
