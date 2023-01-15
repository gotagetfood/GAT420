using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static AutonomousAgentData;

public class Autonomous_Agent : Agent
{
    public Perception flockPerception;
    public AutonomousAgentData data;

    public float wanderAngle { get; set; } = 0;

    void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }
        if (gameObjects.Length > 0) 
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.fleeWeight);
        }

        gameObjects = flockPerception.GetGameObjects();
        if (gameObjects.Length != 0) {
            movement.ApplyForce(Steering.Cohesion  (this, gameObjects                       ) * data.cohesionWeight);
            movement.ApplyForce(Steering.Seperation(this, gameObjects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Alignment (this, gameObjects                       ) * data.alignmentWeight);

        }

        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-50, -50, -50), new Vector3(50, 50, 50));
    }
}
