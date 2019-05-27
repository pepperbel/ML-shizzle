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
        this.goal = new Vector2(this.gO_goal.transform.position.x, this.gO_goal.transform.position.y);
        this.population = new Population(this.populationCount);
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
        foreach(Dot index in this.population.dots)
        {
            Graphics.DrawMesh(this.mesh, Matrix4x4.TRS(index.pos, Quaternion.identity, Vector3.one * 2), mat, 0);
        }
    }
}
