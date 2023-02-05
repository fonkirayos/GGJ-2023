using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CState<T>
{
    protected CStateMachine<T> m_fsm; //tener un acceso a la maquina de estados para que los estados se encarguen de sus trancisiones

    public CState(CStateMachine<T> stateMachine)
    {
        m_fsm = stateMachine;
    }
    public abstract void onEnter(T controller);
    public abstract void update(T Controller);

    public abstract void fixedUpdate(T Controller);
    public abstract void onExit(T controller);
    protected abstract void inputManage(T Controller);

}
