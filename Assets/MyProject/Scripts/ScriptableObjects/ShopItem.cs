using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopItem", menuName = "ScriptableObjects/New Shop Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _starsPricePerLevel;
    [SerializeField] private List<float> _paramList;

    public string Name => _name;
    public int Price => _starsPricePerLevel;
    public List<float> ParamList => _paramList;
}
