using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] Scrollbar _staminaSB;
    [SerializeField] Move _moveScript;
    [SerializeField] Image _bg;
    [SerializeField] Image _handle;

    private bool _visible = true;
    private float _maxStamina;
    private Color _bgVisible, _bgInvisible, _handleVisible, _handleInvisible;

    private void Start()
    {
        _maxStamina = _moveScript.Stamina;
        _bgVisible = _bg.color;
        _handleVisible = _handle.color;
        _bgInvisible = new Color(_bgVisible.r, _bgVisible.g, _bgVisible.b, 0);
        _handleInvisible = new Color(_handleVisible.r, _handleVisible.g, _handleVisible.b, 0);
    }

    private void Update()
    {
        if (_moveScript.Stamina < _maxStamina)
        {
            _staminaSB.size = _moveScript.Stamina / _maxStamina;
            if (!_visible)
            {
                _visible = true;
                _bg.DOColor(_bgVisible, 1);
                _handle.DOColor(_handleVisible, 1);
            }
        }
        else
        {
            _staminaSB.size = 1;
            if (_visible)
            {
                _visible = false;
                _bg.DOColor(_bgInvisible, 2);
                _handle.DOColor(_handleInvisible, 2);
            }
        }
    }
}
