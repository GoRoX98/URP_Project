using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private LevelManager _currentLevel;

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
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnSceneChange(Scene arg0, Scene arg1)
    {
        
    }

    public void Save(LevelManager level)
    {
        PlayerPrefs.SetInt($"{level.Name}", level.CurrentScore);
        PlayerPrefs.Save();
    }
}
