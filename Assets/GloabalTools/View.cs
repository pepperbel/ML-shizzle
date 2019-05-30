using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View<T> : MonoBehaviour, IView<T> {
    private T controller;
    public T Controller {
        get { return controller; }
        set { OnControllerChange(controller, controller = value); }
    }
    public Action OnControllerChange(T oldController, T newController) {
        return null;
    }
}
