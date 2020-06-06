using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehaviour : ScriptableObject
{
    //avoidance or cohesion applied here
    public abstract Vector2 Decide(Flock_Agent agent, List<Transform> context, Flock flock);
    
}
