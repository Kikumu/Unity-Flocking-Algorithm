using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Flock : MonoBehaviour
{
    public Flock_Agent flock_prefab;
    List<Flock_Agent> flocks = new List<Flock_Agent>();
    public FlockBehaviour behaviour;

    [Range(10,600)]
    public int initialCount = 200;
    const float FlockDensity = 0.08f;

    [Range(1f, 100f)]
    public float flock_speed = 10f;
    [Range(1f, 100f)]
    public float flock_speed_offset = 5f;
    [Range(1f, 10f)]
    public float neighbour_radius = 2f;
    [Range(1f, 10f)]
    public float avoidance_radius_multiplier = 0.5f;

    float squaremaxspeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius
    {
        get
        {
            return squareAvoidanceRadius;
        }
    }
    void Start()
    {
        squaremaxspeed = flock_speed_offset * flock_speed_offset;
        squareNeighbourRadius = neighbour_radius * neighbour_radius;
        squareNeighbourRadius = avoidance_radius_multiplier * avoidance_radius_multiplier;

        //populate
        for(int i = 0; i < initialCount; i++)
        {
            Flock_Agent bird = Instantiate(
                flock_prefab,
                UnityEngine.Random.insideUnitCircle * initialCount * FlockDensity,
                Quaternion.Euler(Vector3.forward* UnityEngine.Random.Range(0f,350f)),
                transform
                );
            bird.name = "bird" + i;
            flocks.Add(bird);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Flock_Agent bird in flocks)
        {
            List<Transform> context = GetNearbyObjects(bird);
            //bird.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.black, Color.white, context.Count / 3f);
            Vector2 move = behaviour.Decide(bird, context, this);
            move *= flock_speed;
            if (move.sqrMagnitude > squaremaxspeed)
            {
                move = move.normalized * flock_speed_offset;
            }
            bird.Adjust(move);
        }
    }

    List<Transform>GetNearbyObjects(Flock_Agent bird)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextCollider = Physics2D.OverlapCircleAll(bird.transform.position, neighbour_radius);
        foreach(Collider2D c in contextCollider)
        {
            if (c != bird.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
