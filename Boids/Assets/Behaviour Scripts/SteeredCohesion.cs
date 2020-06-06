using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeredCohesion : FlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;


    public override Vector2 Decide(Flock_Agent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count; //average movement

        //create offset
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        //direction, whereto, velocity, timetakento move to "where to"
        return cohesionMove;
    }
}
