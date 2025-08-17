using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : Singleton<SettingsManager>
{
    // Variables
    private const string VolumeKey = "Volume";

    [Header("Settings UI")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button closeSettingsMenu;
    
    [Header("Volume")]
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private Slider volumeSlider;
    

    private int _currentVolume = 0;



    
    // Functions
    private void Start()
    {
        closeSettingsMenu.onClick.AddListener(CloseMenu);
        closeSettingsMenu.onClick.AddListener(SaveSettings);
        
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
            
        LoadSettings();
    }

    #region Save/Load
    public void SaveSettings()
    {
        PlayerPrefs.SetInt(VolumeKey, _currentVolume);
        PlayerPrefs.Save();
        
        UpdateVolume(_currentVolume);
    }

    private void LoadSettings()
    {
        _currentVolume = PlayerPrefs.GetInt(VolumeKey);
        volumeSlider.value = _currentVolume;

        UpdateVolume(_currentVolume);
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


    private void UpdateVolume(float value)
    {
        _currentVolume = (int)value;
        volumeText.text = "Volume: " + _currentVolume;
    }
}
