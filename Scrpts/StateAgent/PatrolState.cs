using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private float timer;

    public PatrolState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.movement.Resume();
        owner.navigation.targetNode = owner.navigation.GetNearestNode();
        timer = Random.Range(5, 18);
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (owner.perceived.Length > 0) {
            owner.stateMachine.StartState(nameof(ChaseState));
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            owner.stateMachine.StartState(nameof(WanderState));
        }
    }
}
