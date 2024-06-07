using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private LevelData _selectLevel;

    public LevelData Level => _selectLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }

    private void OnEnable()
    {
        LevelCardUI.LevelSelect += SetLevel;
    }

    private void OnDisable()
    {
        LevelCardUI.LevelSelect -= SetLevel;
    }

    public void Save(LevelManager level)
    {
        PlayerPrefs.SetInt(level.Name, level.CurrentScore);
        PlayerPrefs.Save();
        print("Save succes: " + level.Name + " " + level.CurrentScore);
    }

    private void SetLevel(LevelData level) => _selectLevel = level;
    
}
