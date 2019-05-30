using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC_Test_Model {
    public GameObject gameObject;

    MVC_Test_Model(GameObject gameObject) {
        this.gameObject = gameObject;
    }
    ~MVC_Test_Model() {
        GameObject.Destroy(gameObject);
    }
}
