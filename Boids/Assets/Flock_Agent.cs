using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//will be checking around itself(radius of circle) so we need to attatch a collider
[RequireComponent(typeof(Collider2D))]
public class Flock_Agent : MonoBehaviour
{
    Collider2D agentDetector;
    public Collider2D AgentCollider
    {
        get
        {
            return agentDetector;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        agentDetector = GetComponent<Collider2D>();
    }

    public void Adjust(Vector2 v)
    {
        transform.up = v; //magnitude and direction
        transform.position += (Vector3)v * Time.deltaTime;
    }
}
