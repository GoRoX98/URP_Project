public abstract class StateSM
{
    protected readonly StateMachine _machine;

    public StateSM(StateMachine machine)
    {
        _machine = machine;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
