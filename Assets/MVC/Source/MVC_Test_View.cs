using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MVC_Test_View : View<MVC_Test_Controller>, IView<MVC_Test_Controller> {

    [SerializeField] bool positionLocked = true;
    [SerializeField] bool rotationLocked = true;
    [SerializeField] bool scaleLocked = true;

    /// <summary>
    /// Could be collapsed in a Container
    /// maybe List<Array> or Vectors or Class/Struct
    /// </summary>
    [Header("Positions")]
    [Range(-5,5)]
    [SerializeField] private float postionX;
    public float PositionX {
        get { return postionX; }
        set { postionX = value; }
    }
    [Range(-5, 5)]
    [SerializeField] private float postionY;
    public float PositionY {
        get { return postionY; }
        set { postionY = value; }
    }
    [Range(-5, 5)]
    [SerializeField] private float postionZ;
    public float PositionZ {
        get { return postionZ; }
        set { postionZ = value; }
    }

    [Header("Rotations")]
    [Range(-5, 5)]
    [SerializeField] private float rotationX;
    public float RotationX {
        get { return rotationX; }
        set { rotationX = value; }
    }
    [Range(-5, 5)]
    [SerializeField] private float rotationY;
    public float RotationY {
        get { return rotationY; }
        set { rotationY = value; }
    }
    [Range(-5, 5)]
    [SerializeField] private float rotationZ;
    public float RotationZ {
        get { return rotationZ; }
        set { rotationZ = value; }
    }

    [Header("Scales")]
    [Range(-5, 5)]
    [SerializeField] private float scaleX;
    public float ScaleX {
        get { return scaleX; }
        set { scaleX = value; }
    }
    [Range(-5, 5)]
    [SerializeField] private float scaleY;
    public float ScaleY {
        get { return scaleY; }
        set { scaleY = value; }
    }
    [Range(-5, 5)]
    [SerializeField] private float scaleZ;
    public float ScaleZ {
        get { return scaleZ; }
        set { scaleZ = value; }
    }

    public MVC_Test_Controller Controller {
        get { return controller; }
        set { OnControllerChange(controller, controller = value); }
    }
    public void OnControllerChange(MVC_Test_Controller oldController, MVC_Test_Controller newController) {
        if (oldController != null) {

        }
        if (newController != null) {

        }
    }
    void Start() {
        Position();
        Rotation();
        Scale();
    }
    void Update() {
    
    }
    async void Position() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!positionLocked) {
            Controller.PositionUpdate(PositionX, PositionY, PositionZ);
            await frame;
        }
    }
    async void Rotation() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!positionLocked) {
            Controller.RotationUpdate(RotationX, RotationY, RotationZ);
            await frame;
        }
    }
    async void Scale() {
        WaitForEndOfFrame frame = new WaitForEndOfFrame();
        while (!positionLocked) {
            Controller.SclaeUpdate(ScaleX, ScaleY, ScaleZ);
            await frame;
        }
    }
}
