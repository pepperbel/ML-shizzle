using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TastyView : View<TastyController>, IView<TastyController>
{
    [SerializeField] float rotateMe = 0.0f; 
    public float RotateMe {
        get { return rotateMe; }
        set { this.rotateMe = value; }
    }
    
    public TastyController Controller  {
        get { return controller; }
        set { OnControllerChange(controller, controller = value); }
    }

    public void OnControllerChange(TastyController oldController, TastyController newController)
    {
        //to-do: make code work;
    }

    public void Rotate()
    {
        Controller.RotateSpriteZ(RotateMe);
    }

    void Update()
    {
        Rotate();
    }



}
