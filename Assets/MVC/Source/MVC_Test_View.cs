using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC_Test_View : View<MVC_Test_Controller>, IView<MVC_Test_Controller> {

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

    }
    void Update() {

    }

}
