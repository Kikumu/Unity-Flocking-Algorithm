using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector2 Decide(Flock_Agent agent, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 AvoidanceMove = Vector2.zero;
        int inRange = 0;
        foreach (Transform item in context)
        {
            if(Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                inRange++;

                AvoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
           
        }

        if(inRange > 0)
        AvoidanceMove /= inRange; //average movement

        return AvoidanceMove;

    }
}
