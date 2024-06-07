using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<LevelData> _levels;
    [SerializeField] private Transform _levelContent;
    [SerializeField] private GameObject _levelCardPrefab;


    private void Awake()
    {
        foreach (var level in _levels)
        {
            LevelCardUI card = Instantiate(_levelCardPrefab, _levelContent).GetComponent<LevelCardUI>();
            card.Init(level);
        }
    }
}
