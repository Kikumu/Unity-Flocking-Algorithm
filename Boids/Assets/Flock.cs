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
    public float kono_speedo_da = 10f;
    [Range(1f, 100f)]
    public float maxo_speedo = 5f;
    [Range(1f, 10f)]
    public float neighbour_radius_da = 2f;
    [Range(1f, 10f)]
    public float avoidance_radius_multiplier_da = 0.5f;

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
        squaremaxspeed = maxo_speedo * maxo_speedo;
        squareNeighbourRadius = neighbour_radius_da * neighbour_radius_da;
        squareNeighbourRadius = avoidance_radius_multiplier_da * avoidance_radius_multiplier_da;

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
            move *= kono_speedo_da;
            if (move.sqrMagnitude > squaremaxspeed)
            {
                move = move.normalized * maxo_speedo;
            }
            bird.Adjust(move);
        }
    }

    List<Transform>GetNearbyObjects(Flock_Agent bird)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextCollider = Physics2D.OverlapCircleAll(bird.transform.position, neighbour_radius_da);
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
