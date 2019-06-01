﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MVC_Test_View : View<MVC_Test_Controller>, IView<MVC_Test_Controller> {

    [SerializeField] bool positionLocked = false;
    [SerializeField] bool rotationLocked = false;
    [SerializeField] bool scaleLocked = false;

    /// <summary>
    /// TODO: Clamp dis shit into container
    /// </summary>
    [Header("Position")]
    [Range(-10, 10)]
    [SerializeField] float posAmplitude;
    public float PosAmplitude {
        get { return posAmplitude; }
        set { posAmplitude = value; }
    }
    [Range(-10, 10)]
    [SerializeField] float posFrequency;
    public float PosFrequency {
        get { return posFrequency; }
        set { posFrequency = value; }
    }
    [Header("Rotation")]
    [Range(-10, 10)]
    [SerializeField] float rotAmplitude;
    public float RotAmplitude {
        get { return rotAmplitude; }
        set { rotAmplitude = value; }
    }
    [Range(-10, 10)]
    [SerializeField] float rotFrequency;
    public float RotFrequency {
        get { return rotFrequency; }
        set { rotFrequency = value; }
    }
    [Header("Scale")]
    [Range(-10, 10)]
    [SerializeField] float scaleAmplitude;
    public float ScaleAmplitude {
        get { return scaleAmplitude; }
        set { scaleAmplitude = value; }
    }
    [Range(-10, 10)]
    [SerializeField] float scaleFrequency;
    public float ScaleFrequency {
        get { return scaleFrequency; }
        set { scaleFrequency = value; }
    }

    public MVC_Test_Controller Controller {
        get { return controller; }
        set { OnControllerChange(controller, controller = value); }
    }
    public void OnControllerChange(MVC_Test_Controller oldController, MVC_Test_Controller newController) {
        if (oldController != null) {

        }
        else Debug.Log(Utilities.Instance.DebugError(this.ToString(), "OnControllerChange", "OldController was Null!"));
        if (newController != null) {
            Position();
            Rotation();
            Scale();
        }
        else Debug.Log(Utilities.Instance.DebugError(ToString(), "OnControllerChange", "NewController was Null!"));
    }
    void Start() {
   
    }
    void Update() {
    
    }
    async void Position() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!positionLocked) {
            Debug.Log(Utilities.Instance.DebugLog(ToString(), "Position", "Update!"));
            Controller.PositionUpdate(PosAmplitude, PosFrequency);
            await frame;
        }
    }
    async void Rotation() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!rotationLocked) {
            Debug.Log(Utilities.Instance.DebugLog(ToString(), "Rotation", "Update!"));
            Controller.RotationUpdate(RotAmplitude, RotFrequency);
            await frame;
        }
    }
    async void Scale() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!scaleLocked) {
            Debug.Log(Utilities.Instance.DebugLog(ToString(), "Scale", "Update!"));
            Controller.SclaeUpdate(ScaleAmplitude, ScaleFrequency);
            await frame;
        }
    }
}
