using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float fitness = 0;

    public Dot()
    {
        brain = new Brain(1000);

        pos = new Vector2(161, -170);
        vel = Vector2.zero;
        acc = Vector2.zero;
        goal = new Vector2(-163.7f, 164.2f);
    }

    public void Move()
    {
        if (brain.directions.Length > brain.step)
        {//if there are still directions left then set the acceleration as the next PVector in the direcitons array
            acc = brain.directions[brain.step];
            brain.step++;
        }
        else
        {//if at the end of the directions array then the dot is dead
            dead = true;
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
            Move();
            if (pos.x < -200 || pos.y < -200 || pos.x > 200 || pos.y > 200)
            {//if near the edges of the window then kill it 
                dead = true;
            }
            else if (Vector2.Distance(pos, goal) < 5)
            {//if reached goal
                Debug.Log("Goal");
                reachedGoal = true;
            }
            else 
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
                if (hit && hit.transform.tag != "Finish")
                {
                    dead = true;
                    Debug.Log("Dead");
                }else if(hit && hit.transform.tag == "Finish")
                {
                    Debug.Log("Goal");
                    reachedGoal = true;
                }
            }
        }
    }

    public void CalculateFitness()
    {
        if (reachedGoal)
        {//if the dot reached the goal then the fitness is based on the amount of steps it took to get there
            fitness = 1.0f / 16.0f + 10000.0f / (float)(brain.step * brain.step);
        }
        else
        {//if the dot didn't reach the goal then the fitness is based on how close it is to the goal
            float distanceToGoal = Vector2.Distance(pos, goal);
            fitness = 1.0f / (distanceToGoal * distanceToGoal);
        }
    }

    public Dot GimmeBaby()
    {
        Dot baby = new Dot();
        baby.brain = brain.Clone();//babies have the same brain as their parents
        return baby;
    }
}
