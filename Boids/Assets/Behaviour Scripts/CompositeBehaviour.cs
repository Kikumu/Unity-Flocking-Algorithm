using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weights;
    public override Vector2 Decide(Flock_Agent agent, List<Transform> context, Flock flock)
    {
        if(weights.Length != behaviours.Length)
        {
            Debug.LogError("weights to behaviour length mismatch");
            return Vector2.zero; //do nothing
        }

        Vector2 move = Vector2.zero;

        for(int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].Decide(agent, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize(); //range the values
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }
        return move;
    }

}
