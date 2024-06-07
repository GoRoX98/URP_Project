using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Color _starColor;
    [SerializeField] private Image _firstStar;
    [SerializeField] private Image _secondStar;
    [SerializeField] private Image _thirdStar;
    
    private LevelData _data;

    public LevelData Data => _data;

    public static event Action<LevelData> LevelSelect;

    public void Init(LevelData data)
    {
        _data = data;
        UpdateCard();
    }

    private void UpdateCard()
    {
        _title.text = _data.Name;
        int score = PlayerPrefs.GetInt(_data.Name, 0);
        _score.text = $"{score} / {_data.MaxScore}";
        int stars = _data.StarsCount(score);
        if (stars > 0)
            _firstStar.color = _starColor;
        if (stars > 1)
            _secondStar.color = _starColor;
        if (stars > 2)
            _thirdStar.color = _starColor;
    }

    public void Play()
    {
        LevelSelect?.Invoke(_data);
        SceneManager.LoadScene($"{_data.Name}");
    }
}
