using System;
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
        else Debug.Log(Ultra.Utilities.Instance.DebugErrorString(this.ToString(), "OnControllerChange", "OldController was Null!"));
        if (newController != null) {
            Position();
            Rotation();
            Scale();
        }
        else Debug.Log(Ultra.Utilities.Instance.DebugErrorString(ToString(), "OnControllerChange", "NewController was Null!"));
    }
    async void Start() {
        await new WaitForSeconds(3);
        Ultra.Utilities.Instance.DebugLogOnScreen("WOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO", 5f);
    }
    void Update() {
    
    }
    async void Position() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!positionLocked) {
            Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString(ToString(), "Position", Controller.GameObject.transform.position.ToString()));
            Controller.PositionUpdate(PosAmplitude, PosFrequency);
            await frame;
        }
    }
    async void Rotation() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!rotationLocked) {
            Controller.RotationUpdate(RotAmplitude, RotFrequency);
            Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString(ToString(), "Rotation", Controller.GameObject.transform.rotation.eulerAngles.ToString()));
            await frame;
        }
    }
    async void Scale() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!scaleLocked) {
            Ultra.Utilities.Instance.DebugLogOnScreen(Ultra.Utilities.Instance.DebugLogString(ToString(), "Scale", Controller.GameObject.transform.localScale.ToString()));
            Controller.SclaeUpdate(ScaleAmplitude, ScaleFrequency);
            await frame;
        }
    }
}
