using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Population {
    public Dot[] dots;

    private float fitnessSum;
    private int gen = 1;

    public int bestDot = 0;

    private int minStep = 1000;
    Vector2 goal;

    public Population(int size, Vector2 goal) {
        this.goal = goal;
        dots = new Dot[size];
        for (int i = 0; i < size; i++) {
            dots[i] = new Dot(this.goal);
        }
    }

    public void Update() {
        for (int i = 0; i < dots.Length; i++) {
            if (dots[i].brain.step > minStep) {
                dots[i].pLastStepIndex = dots[i].brain.step;
                dots[i].dead = true;
            }
            else {
                dots[i].Update();
            }
        }
    }

    public void CalculateFitness() {
        for (int i = 0; i < dots.Length; i++) {
            dots[i].CalculateFitness();
        }
    }

    public bool AllDotsDead() {
        for (int i = 0; i < dots.Length; i++) {
            if (!dots[i].dead && !dots[i].reachedGoal) {
                return false;
            }
        }

        return true;
    }

    public void NaturalSelection() {
        Dot[] newDots = new Dot[dots.Length];//next gen
        CalculateFitnessSum();

        int campoinKidz = 5;
        if (dots[bestDot].reachedGoal && dots[bestDot].parentReachedGoal) campoinKidz = 20; 
        else if (dots[bestDot].reachedGoal ) campoinKidz = 10; 
        // Champion has more kids
        for (int i = 0; i < campoinKidz; i++) {
            if (i == 0) {
                //the champion lives on 
                newDots[0] = dots[bestDot].GimmeBaby();
                newDots[0].isBest = true;
            }
            else {
                newDots[i] = dots[bestDot].GimmeBaby();
            }
            if (dots[bestDot].reachedGoal) newDots[i].parentReachedGoal = true;
        }
        for (int i = campoinKidz; i < newDots.Length; i++) {
            //select parent based on fitness
            Dot parent = SelectParent(dots[bestDot].fitness);

            //get baby from them
            newDots[i] = parent.GimmeBaby();

            if (parent.reachedGoal) newDots[i].parentReachedGoal = true;
        }

        Array.Copy(newDots, dots, newDots.Length);
        gen++;
    }

    public void CalculateFitnessSum() {
        fitnessSum = 0;
        for (int i = 0; i < dots.Length; i++) {
            fitnessSum += dots[i].fitness;
        }
    }

    Dot SelectParent(float bestfitness) {
        float rand = Random.Range(bestfitness / 2, bestfitness);
        if (dots[bestDot].reachedGoal) rand = Random.Range(bestfitness / 5, bestfitness);
        //Debug.Log("BestFitness::" + bestfitness + " Rand::" + rand + " RandIndex::" + randIndex);

        // Slows down the process A LOT ... maybe use List?
        Array.Sort(dots, (a, b) => b.fitness.CompareTo(a.fitness));

        List<List<Vector2>> bestResult = new List<List<Vector2>>();
        for (int i = 0; i < 100; i++) { bestResult.Add(new List<Vector2>(dots[i].brain.directions)); }

        int randIndex = Random.Range(0, bestResult.Count);
        for (int i = randIndex; i < bestResult.Count; i++) {
            if (dots[i].fitness > rand) {
                return dots[i];
            }
        }

        //should never get to this point
        // useless but better than chrash
        return new Dot(this.goal);
    }

    public void MutateDemBabies() {
        for (int i = 1; i < dots.Length; i++) {
            if (dots[i].parentReachedGoal) dots[i].brain.Mutate(0.01f);
            else dots[i].brain.Mutate(0.1f);
        }
    }

    public void SetBestDot() {
        float max = 0;
        int maxIndex = 0;
        for (int i = 0; i < dots.Length; i++) {
            if (dots[i].fitness > max) {
                max = dots[i].fitness;
                maxIndex = i;
            }
        }

        bestDot = maxIndex;

        //if this dot reached the goal then reset the minimum number of steps it takes to get to the goal
        //if (dots[bestDot].reachedGoal)
        //{
        //    minStep = dots[bestDot].brain.step;
        //}
    }
}
