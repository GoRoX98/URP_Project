using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private LevelData _selectLevel;
    private Wallet _wallet;

    public LevelData Level => _selectLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            _wallet = new Wallet(5);
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
        if (PlayerPrefs.GetInt(level.Name, 0) < level.CurrentScore)
            PlayerPrefs.SetInt(level.Name, level.CurrentScore);
        else
            return;

        int saveStars = PlayerPrefs.GetInt(level.Name + "Stars", 0);
        int stars = level.Data.StarsCount(level.CurrentScore);
        if (stars > saveStars)
        {
            PlayerPrefs.SetInt(level.Name + "Stars", stars);
            PlayerPrefs.SetInt("Stars", _wallet.Stars + (stars - saveStars));
        }
        PlayerPrefs.Save();
        _wallet.UpdateStars();
    }

    private void SetLevel(LevelData level) => _selectLevel = level;
    
}
