using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLearning : MonoBehaviour {
    private Population population;
    public int populationCount = 1000;
    public Mesh mesh;
    public Material mat;
    public GameObject gO_goal;
    public LineRenderer lr;

    Vector2 goal;
    void Start() {
        lr = GetComponent<LineRenderer>();
        goal = new Vector2(gO_goal.transform.position.x, gO_goal.transform.position.y);
        population = new Population(populationCount, goal);
    }

    void FixedUpdate() {
        if (population.AllDotsDead()) {
            population.CalculateFitness();
            population.SetBestDot();
            SetupLineRenderer();
            population.NaturalSelection();
            population.MutateDemBabies();
        }
        else {
            population.Update();
        }
    }

    void Update() {
        for (int i = 0; i < population.dots.Length; i++) {
            Graphics.DrawMesh(mesh, Matrix4x4.TRS(population.dots[i].pos, Quaternion.identity, Vector3.one * 2), mat, 0);
        }
    }
    void SetupLineRenderer() {
        lr.positionCount = population.dots[population.bestDot].pLastStepIndex;
        int i = 0;
        Vector2 pos = new Vector2(161, -170);
        Vector2 vel = Vector2.zero;
        while(i < lr.positionCount) {
            vel += population.dots[population.bestDot].brain.directions[i];
            vel = Vector2.ClampMagnitude(vel, 5);
            pos += vel;
            lr.SetPosition(i, pos);
            ++i;
        }
    }
}
