using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _starsCount;

    private void Start()
    {
        UpdateBalance(Wallet.Instance.Stars);
    }

    private void OnEnable()
    {
        Wallet.BalanceChanged += UpdateBalance;
    }

    private void OnDisable()
    {
        Wallet.BalanceChanged -= UpdateBalance;
    }

    private void UpdateBalance(int stars) => _starsCount.text = stars.ToString();
}
