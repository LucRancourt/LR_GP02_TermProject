using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Variables
    private const string VolumeKey = "Volume";
    
    [Header("Settings UI")]
    [SerializeField] private Button applySettingsButton;
    
    [Header("Volume")]
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private Slider volumeSlider;
    

    private int _currentVolume = 0;



    
    // Functions
    private void Start()
    {
        applySettingsButton.onClick.AddListener(SaveSettings);
        
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
            
        LoadSettings();
    }
    
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

    private void UpdateVolume(float value)
    {
        _currentVolume = (int)value;
        volumeText.text = "Volume: " + _currentVolume;
    }
}
