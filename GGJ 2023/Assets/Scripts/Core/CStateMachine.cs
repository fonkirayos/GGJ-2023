public class CStateMachine<T>
{
    CState<T> m_currentState = null;
    // CState<T> m_next = null;

    public void setCurrentState(CState<T> state, T Controller)
    {
        if (m_currentState != null)
            m_currentState.onExit(Controller);

        m_currentState = state;
        m_currentState.onEnter(Controller);
    }

    public void update(T Controller)
    {
        m_currentState.update(Controller);
    }

    public void fixedUpdate(T Controller)
    {
        m_currentState.fixedUpdate(Controller);
    }
}