using UnityEngine;

public class PlayerPresenter : Presenter
{
    private new PlayerModel _model;
    private new PlayerView _view;

    public PlayerPresenter(PlayerModel model, PlayerView view) : base(model, view) 
    { 
        _model = model;
        _view = view;
    }

    public void OnInteract(Collider[] colliders)
    {
        foreach (var collider in colliders) 
        { 
            if (collider.TryGetComponent(out InteractView view))
            {
                _model.OnInteract();
                view.OnInteract(_model);
                break;
            }
        }
    }
}
