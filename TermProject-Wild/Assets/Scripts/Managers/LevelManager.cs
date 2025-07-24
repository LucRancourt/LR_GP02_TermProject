using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    // Functions
    private void Start()
    {
        LoadLevel("MainMenu");
        SceneManager.sceneLoaded += SetActiveScene;
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
}
