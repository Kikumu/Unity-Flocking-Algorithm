using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Flock/Behaviour/Border")]
public class Border : FlockBehaviour
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 Decide(Flock_Agent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float value = centerOffset.magnitude / radius;

        if (value < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * value * value;
        
    }
}
