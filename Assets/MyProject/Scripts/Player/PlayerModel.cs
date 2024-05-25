using UnityEngine;

public class PlayerModel : Model, ICanInteract
{
    private Animator _animator;

    public PlayerModel(Animator animator)
    {
        _animator = animator;
    }

    public void OnInteract()
    {
        _animator.SetTrigger("Interact");
    }
}
