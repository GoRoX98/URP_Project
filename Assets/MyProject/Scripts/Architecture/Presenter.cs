public abstract class Presenter
{
    protected Model _model;
    protected View _view;

    public Presenter(Model model, View view)
    {
        _model = model;
        _view = view;
    }
}