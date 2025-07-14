using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public abstract class Menu : MonoBehaviour
{
    // Variables
    public static Menu _instance;

    [Header("Audio")]
    [SerializeField] protected AudioClip[] _audioClips;
    protected AudioSource _audioSource;


    // Functions
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Menu GetInstance()
    {
        return _instance;
    }
}
