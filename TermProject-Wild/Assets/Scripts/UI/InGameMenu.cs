using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameMenu : Singleton<InGameMenu>
{
    // Variables
    [Header("General UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;

    private float _maxHealth = 0.0f;
    [SerializeField] private Slider _healthBar;


    // Functions
    public void SetScore(int score)
    {
        _scoreText.SetText("Score: " + score);
    }

    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        _healthBar.value = currentHealth / _maxHealth;
    }
}
