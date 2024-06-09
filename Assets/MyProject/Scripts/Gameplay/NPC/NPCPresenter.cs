public class NPCPresenter
{
    private NPCModel _model;
    private NPCView _view;

    public NPCPresenter(NPCView view)
    {
        _view = view;
        _view.Interact += OnInteract;
        _model = new(view.Agent, view.MaxSpeed, view.SpeedIncrase);
    }
    ~NPCPresenter() 
    {
        _view.Interact -= OnInteract;
    }

    public void Update()
    {
        _model.Update();
    }

    private void OnInteract(UnityEngine.Transform target)
    {
        _model.Follow(_model.State is not FollowState, target);
    }
}
