using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brain
{
    public Vector2[] directions;
    public int step = 0;

    public Brain(int size = 1000)
    {
        directions = new Vector2[size];
        Randomize();
    }

    void Randomize()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }

    public Brain Clone()
    {
        Brain clone = new Brain(directions.Length);
        for (int i = 0; i < directions.Length; i++)
        {
            clone.directions[i] = new Vector2(directions[i].x, directions[i].y);
        }

        return clone;
    }

    public void Mutate(float mutationRate)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            float rand = Random.value;
            if (rand < mutationRate)
            {
                directions[i] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            }
        }
    }
}
