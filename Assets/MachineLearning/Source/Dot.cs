using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dot
{
    public Vector2 pos;
    private Vector2 vel;
    private Vector2 acc;
    private Vector2 goal;
    public Brain brain;

    public bool dead = false;
    public bool reachedGoal = false;
    public bool isBest = false;
    public bool parentReachedGoal = false;

    public float smallestDistance = 1000000000;
    public float fitness = 0;
    public float timeAlive = 0;

    public Dot(Vector2 goal)
    {
        brain = new Brain();

        pos = new Vector2(161, -170);
        vel = Vector2.zero;
        acc = Vector2.zero;
        this.goal = goal;
    }

    public void Move()
    {
        float distance = Vector2.Distance(pos, goal);
        if (distance < smallestDistance) smallestDistance = distance;
        if (brain.directions.Length > brain.step)
        {//if there are still directions left then set the acceleration as the next PVector in the direcitons array
            acc = brain.directions[brain.step];
            brain.step++;
        }
        else
        {
            Debug.Log("MORE STEPS!");
            // HAHA THATS LIKE REALY BAD!
            Array.Resize(ref brain.directions, brain.directions.Length * 2);
            for (int i = brain.step; i < brain.directions.Length; i++)
            {
                brain.directions[i] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            }
            acc = brain.directions[brain.step];
            brain.step++;
        }

        //apply the acceleration and move the dot
        vel += acc;
        vel = Vector2.ClampMagnitude(vel, 5);//not too fast
        pos += vel;
    }

    public void Update()
    {
        if (!dead && !reachedGoal)
        {
            timeAlive += Time.deltaTime;
            Move();
            if (pos.x < -200 || pos.y < -200 || pos.x > 200 || pos.y > 200)
            {//if near the edges of the window then kill it 
                dead = true;
            }
            else if (Vector2.Distance(pos, goal) < 5)
            {//if reached goal
                reachedGoal = true;
            }
            else 
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
                if (hit && hit.transform.tag != "Finish")
                {
                    dead = true;
                }else if(hit && hit.transform.tag == "Finish")
                {
                    reachedGoal = true;
                }
            }
        }
    }

    public void CalculateFitness()
    {
        if (reachedGoal)
        {//if the dot reached the goal then the fitness is based on the amount of steps it took to get there
            fitness = 1.0f / (2.0f * timeAlive) + 10000.0f / (float)(brain.step * brain.step);
        }
        else
        {//if the dot didn't reach the goal then the fitness is based on how close it is to the goal
            float distanceToGoal = Vector2.Distance(pos, goal);
            fitness = 1.0f / 16.0f + ((1/smallestDistance) + (timeAlive)) / (distanceToGoal * distanceToGoal * distanceToGoal);
        }
        timeAlive = 0f;
    }

    public Dot GimmeBaby()
    {
        Dot baby = new Dot(this.goal);
        baby.brain = brain.Clone();//babies have the same brain as their parents
        return baby;
    }
}
