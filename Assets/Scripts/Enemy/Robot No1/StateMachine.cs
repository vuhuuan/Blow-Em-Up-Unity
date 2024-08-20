using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter()
    {
        Debug.Log("Entering " + this.GetType().Name + " state");
    }

    public void Execute()
    {
        Debug.Log(this.GetType().Name + " state is executing");
    }

    public void Exit()
    {
        Debug.Log("Exiting " + this.GetType().Name + " state");
    }
}

public class StateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public IState _currentState;

    public Dictionary<string, IState> _states = new Dictionary<string, IState>();

    public void ChangeState (string newStateName)
    {
        if (newStateName == _currentState.GetType().Name) return;

        if (_states.TryGetValue(newStateName, out IState newState))
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
        else
        {
            Debug.LogError("State not found: " + newStateName);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState != null)
        {
            _currentState.Execute();
        }
    }
}
