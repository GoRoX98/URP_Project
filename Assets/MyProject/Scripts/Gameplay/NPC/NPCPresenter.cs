public class NPCPresenter
{
    private NPCModel _model;
    private NPCView _view;

    public NPCPresenter(NPCView view)
    {
        _view = view;
        _model = new(view.Agent, view.MaxSpeed, view.SpeedIncrase);
    }

    public void Update()
    {
        _model.Update();
    }
}
