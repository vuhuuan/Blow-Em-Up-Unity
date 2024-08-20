using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(RobotNo1Controller))]
public class RobotNo1StateMachine : StateMachine
{
    [SerializeField] private RobotNo1Controller RobotNo1Controller;

    void Start()
    {

        // khai báo bộ điều khiển và các trạng thái 
        RobotNo1Controller = gameObject.GetComponent<RobotNo1Controller>();

        // nameof hay typeof both ok;
        _states.Add(nameof(RobotNo1PatrolState), new RobotNo1PatrolState(RobotNo1Controller));
        _states.Add(nameof(RobotNo1AttackState), new RobotNo1AttackState(RobotNo1Controller));
        
        _currentState = _states[nameof(RobotNo1PatrolState)];
    }


    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.Execute();
        }
    }
}

public class RobotNo1PatrolState : IState
{
    private RobotNo1Controller _robot;

    public RobotNo1PatrolState(RobotNo1Controller robot)
    {
        _robot = robot;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        _robot.MovingAround();
        _robot.LookAround();

        if (_robot.DetectPlayer())
        {
            _robot.ChangeState(nameof(RobotNo1AttackState));
        }
    }

    public void Exit()
    {
    }
}

public class RobotNo1AttackState : IState
{
    private RobotNo1Controller _robot;

    private float lostPlayerCounter;

    public RobotNo1AttackState(RobotNo1Controller robot)
    {
        _robot = robot;
    }

    public void Enter()
    {
        lostPlayerCounter = _robot.LostPlayerCoolDown;
        _robot.IsAttacking = true;
    }

    public void Execute()
    {
        _robot.LookAtPlayer();
        if (!_robot.DetectPlayer())
        {
            lostPlayerCounter -= Time.deltaTime;
            if (lostPlayerCounter <= 0)
            {
                _robot.ChangeState(nameof(RobotNo1PatrolState));
            }
        }
    }

    public void Exit()
    {
        _robot.IsAttacking = false;
    }
}