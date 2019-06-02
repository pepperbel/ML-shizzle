using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork : IComparable<NeuralNetwork> {
    private int[] layers;           // layers
    private float[][] neurons;      // neurons matrix
    private float[][][] weights;    // weights matrix
    private float fitness;          // Fitness of the network
    
    /// <summary>
    /// Initializes and populate neural network with random weights
    /// </summary>
    /// <param name="layers"> layers to the neural network </param>
    public NeuralNetwork(int[] layers) {
        // deep copy of layers of this network
        this.layers = new int[layers.Length];
        for(int i = 0; i < layers.Length; i++) {
            this.layers[i] = layers[i];
        }

        // generate matrix
        InitNeurons();
        InitWeights();
    }

    /// <summary>
    /// Deep copy constructor
    /// </summary>
    /// <param name="copyNetwork"> Network to deep copy </param>
    public NeuralNetwork(NeuralNetwork copyNetwork) {
        // deep copy of layers of this network
        this.layers = new int[copyNetwork.layers.Length];
        for (int i = 0; i < copyNetwork.layers.Length; i++) {
            this.layers[i] = copyNetwork.layers[i];
        }
        InitNeurons();
        InitWeights();
        CopyWeights(copyNetwork.weights);
    }

    private void CopyWeights(float[][][] copyWeights) {
        for(int i = 0; i < weights.Length; i++) {
            for(int j = 0; j < weights[i].Length; j++) {
                for(int k = 0; k < weights[i][j].Length; k++) {
                    weights[i][j][k] = copyWeights[i][j][k];
                }
            }
        }
    }

    /// <summary>
    /// Create neuron matrix
    /// </summary>
    private void InitNeurons() {
        List<float[]> neuronList = new List<float[]>();
        for (int i = 0; i < layers.Length; i++) {       // run through all layers
            neuronList.Add(new float[layers[i]]);       // add layer to neuron list
        }
        neurons = neuronList.ToArray();                 // convert list to array
    }

    /// <summary>
    ///  Create weight matrix
    /// </summary>
    private void InitWeights() {
        List<float[][]> weightList = new List<float[][]>();                             // weights list which will later will converted into weights 3D array
        for(int i = 1; i < layers.Length; i++) {                                        // iterate over all neurons that have a wight connection
            List<float[]> layerWeightList = new List<float[]>();            
            int neuronsInPreviousLayer = layers[i - 1];
            for(int j = 0; j < neurons[i].Length; j++) {                                // iterate over all neuron in this current layer 
                float[] neuronWeights = new float[neuronsInPreviousLayer];  
                for(int k = 0; k < neuronsInPreviousLayer; k++) {                       // iterate over all neurons in the previous layer and set the weight randomly between 0.5f and -0.5f
                    neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);           // give random weight to neuron weights
                }
                layerWeightList.Add(neuronWeights);                                     // add neuron weights of this current layer to layer weights
            }
            weightList.Add(layerWeightList.ToArray());                                  // add this layers weights converted into 2D array into weights list
        }
        weights = weightList.ToArray();                                                 // convert to 3D array
    }

    /// <summary>
    /// Deed forward this neural network with a given input array
    /// </summary>
    /// <param name="inputs"> inputs to network </param>
    /// <returns></returns>
    public float[] FeedForward(float[] inputs) {
        for(int i = 0; i < inputs.Length; i++) {                            // Add input to the neuron matrix
            neurons[0][i] = inputs[i];
        }
        for(int i = 1; i < layers.Length; i++) {                            // iterate over all neurons and compute feed forward values
            for (int j = 0; j < neurons[i].Length; j++) {
                float value = 0.25f;
                for (int k = 0; k < neurons[i-1].Length; k++) {
                    value += weights[i - 1][j][k] * neurons[i - 1][k];      // sum off all weight connections of this neuron weight their values in previous layer
                }
                neurons[i][j] = (float)Math.Tanh(value);                    // Hyperbolic tangent activation
            }
        }
        return neurons[neurons.Length-1];                                   // return output layer
    }

    /// <summary>
    /// Mutate neural network weights
    /// </summary>
    public void Mutate() {
        for (int i = 0; i < weights.Length; i++) {
            for (int j = 0; j < weights[i].Length; j++) {
                for (int k = 0; k < weights[i][j].Length; k++) {
                    float weight = weights[i][j][k];
                    float randomNumber = UnityEngine.Random.Range(0f, 100f);    //mutate weight value 
                    if (randomNumber <= 2f) {       //if 1                   
                        weight *= -1f;                                          //flip sign of weight
                    }
                    else if (randomNumber <= 4f) {  //if 2                         
                        weight = UnityEngine.Random.Range(-0.5f, 0.5f);         //pick random weight between -1 and 1
                    }
                    else if (randomNumber <= 6f) {  //if 3                        
                        float factor = UnityEngine.Random.Range(0f, 1f) + 1f;   //randomly increase by 0% to 100%
                        weight *= factor;
                    }
                    else if (randomNumber <= 8f) {  //if 4                         
                        float factor = UnityEngine.Random.Range(0f, 1f);        //randomly decrease by 0% to 100%
                        weight *= factor;
                    }
                    weights[i][j][k] = weight;
                }
            }
        }
    }

    public void AddFitness(float fit) {  fitness += fit; }
    public void SetFitness(float fit) { fitness = fit; }
    public float GetFitness() { return fitness; }

    public int CompareTo(NeuralNetwork other) {
        if (other == null) return 1;
        if (fitness > other.fitness) return 1;
        else if (fitness < other.fitness) return -1;
        else return 0;
    }
}
