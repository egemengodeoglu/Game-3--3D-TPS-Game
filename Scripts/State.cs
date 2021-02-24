public abstract class State
{
    protected StateMachine stateMachine;

    public abstract void Tick();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

}

