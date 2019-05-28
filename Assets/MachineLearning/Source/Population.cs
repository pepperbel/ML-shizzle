using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Population
{
    public Dot[] dots;

    private float fitnessSum;
    private int gen = 1;

    private int bestDot = 0;

    private int minStep = 1000;

    public Population(int size)
    {
        dots = new Dot[size];
        for (int i = 0; i < size; i++)
        {
            dots[i] = new Dot();
        }
    }

    public void Update()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            if (dots[i].brain.step > minStep)
            {
                dots[i].dead = true;
            }
            else
            {
                dots[i].Update();
            }
        }
    }

    public void CalculateFitness()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].CalculateFitness();
        }
    }

    public bool AllDotsDead()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            if (!dots[i].dead && !dots[i].reachedGoal)
            {
                return false;
            }
        }

        return true;
    }

    public void NaturalSelection()
    {
        Dot[] newDots = new Dot[dots.Length];//next gen
        SetBestDot();
        CalculateFitnessSum();

        //the champion lives on 
        newDots[0] = dots[bestDot].GimmeBaby();
        newDots[0].isBest = true;
        for (int i = 1; i < newDots.Length; i++)
        {
            //select parent based on fitness
            Dot parent = SelectParent();

            //get baby from them
            newDots[i] = parent.GimmeBaby();
        }

        Array.Copy(newDots, dots, newDots.Length);
        gen++;
    }

    public void CalculateFitnessSum()
    {
        fitnessSum = 0;
        for (int i = 0; i < dots.Length; i++)
        {
            fitnessSum += dots[i].fitness;
        }
    }

    Dot SelectParent()
    {
        float rand = Random.Range(0, fitnessSum);


        float runningSum = 0;

        for (int i = 0; i < dots.Length; i++)
        {
            runningSum += dots[i].fitness;
            if (runningSum > rand)
            {
                return dots[i];
            }
        }

        //should never get to this point
        // useless but better than chrash
        return new Dot();
    }

    public void MutateDemBabies()
    {
        for (int i = 1; i < dots.Length; i++)
        {
            dots[i].brain.Mutate();
        }
    }

    public void SetBestDot()
    {
        float max = 0;
        int maxIndex = 0;
        for (int i = 0; i < dots.Length; i++)
        {
            if (dots[i].fitness > max)
            {
                max = dots[i].fitness;
                maxIndex = i;
            }
        }

        bestDot = maxIndex;

        //if this dot reached the goal then reset the minimum number of steps it takes to get to the goal
        if (dots[bestDot].reachedGoal)
        {
            minStep = dots[bestDot].brain.step;
        }
    }
}
