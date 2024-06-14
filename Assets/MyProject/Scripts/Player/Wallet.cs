using System;
using UnityEngine;

public class Wallet
{
    private int _stars;

    public static Wallet Instance;
    public int Stars
    {
        get
        {
            UpdateStars();
            return _stars;
        }
        private set
        {
            _stars = value;
            BalanceChanged?.Invoke(_stars);
        }
    }

    public static event Action<int> BalanceChanged;

    public Wallet()
    {
        UpdateStars();
        Instance = this;
    }
    public Wallet(int stars)
    {
        PlayerPrefs.SetInt("Stars", stars);
        PlayerPrefs.Save();
        UpdateStars();
        Instance = this;
        ShopCardUI.BuyItem += OnBuy;
    }
    ~Wallet() 
    {
        ShopCardUI.BuyItem -= OnBuy;
    }

    public void UpdateStars() => Stars = PlayerPrefs.GetInt("Stars", 0);

    private void OnBuy(int cost)
    {
        PlayerPrefs.SetInt("Stars", _stars - cost);
        PlayerPrefs.Save();
        UpdateStars();
    }
}
