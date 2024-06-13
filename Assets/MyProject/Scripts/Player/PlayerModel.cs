using UnityEngine;

public class PlayerModel : Model, ICanInteract
{
    private Animator _animator;
    private Transform _backpack;
    private Transform _player;

    public Transform Backpack => _backpack;
    public Transform PlayerTransform => _player;

    public PlayerModel(Animator animator, Transform player, Transform backpack)
    {
        _animator = animator;
        _backpack = backpack;
        _player = player;
    }

    public void OnInteract()
    {
        _animator.SetTrigger("Interact");
    }
}
