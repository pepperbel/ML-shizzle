using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView<T> {
    T Controller { get; set; }
    void OnControllerChange(T oldController, T newController);
}
