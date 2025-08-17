using UnityEngine;


[System.Serializable]
public class GameData
{
    public string PlayerName;
    public Vector3 PlayerPosition;
    public int PlayerScore;
}


public class SaveManager : Singleton<SaveManager>
{
    // Variables
    private string _savePath;


    // Functions
    protected override void Awake()
    {
        base.Awake();
        
        _savePath = Application.persistentDataPath + "/gameData.json";
    }

    public void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        
        System.IO.File.WriteAllText(_savePath, json);
    }

    public GameData LoadGame()
    {
        if (System.IO.File.Exists(_savePath))
        {
            string json = System.IO.File.ReadAllText(_savePath);
            return JsonUtility.FromJson<GameData>(json);
        }

        return null;
    }
}
