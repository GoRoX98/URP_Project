using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour
{
    [SerializeField] private ShopItem _item;
    [SerializeField] private TextMeshProUGUI _card;
    [SerializeField] private Button _button;
    private int _level;

    public static event Action<int> BuyItem;

    private void Awake()
    {
        UpdateCard();
    }

    private void UpdateCard()
    {
        _level = PlayerPrefs.GetInt(_item.Name, 0);
        _card.text = "Уровень\n" + $"{_level + 1} / {_item.ParamList.Count}";
        if (_level >= _item.ParamList.Count)
            _button.interactable = false;
    }

    public void Buy()
    {
        if (Wallet.Instance.Stars > _item.Price)
        {
            BuyItem?.Invoke(_item.Price);
            _level += 1;
            PlayerPrefs.SetInt(_item.Name, _level);
            PlayerPrefs.SetFloat(_item.Name + "Param", _item.ParamList[_level]);
            PlayerPrefs.Save();
        }

        UpdateCard();
    }
}
