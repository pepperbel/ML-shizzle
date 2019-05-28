using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLearning : MonoBehaviour
{
    private Population population;
    public int populationCount = 1000;
    public Mesh mesh;
    public Material mat;
    public GameObject gO_goal;

    Vector2 goal;
    void Start()
    {
        goal = new Vector2(gO_goal.transform.position.x, gO_goal.transform.position.y);
        population = new Population(populationCount);
        Debug.Log(population.dots.Length);
    }

    void FixedUpdate()
    {
        if (population.AllDotsDead())
        {
            population.CalculateFitness();
            population.NaturalSelection();
            population.MutateDemBabies();
        }
        else
        {
            population.Update();
        }
    }

    void Update()
    {
        for(int i = 0; i < population.dots.Length; i++)
        {
            Graphics.DrawMesh(mesh, Matrix4x4.TRS(population.dots[i].pos, Quaternion.identity, Vector3.one * 2), mat, 0);
        }
    }
}
