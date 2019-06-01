﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC_Test_Controller {
    private MVC_Test_Model model;
    public GameObject GameObject {
        get { return model.gameObject; }
        set { model.gameObject = value; }
    }

    public void PositionUpdate(float amplitude, float frequanzi) {
        GameObject.transform.position = new Vector3(
            (GameObject.transform.position.x + (Mathf.Sin(Time.time * frequanzi) * amplitude)),
            (GameObject.transform.position.y + (Mathf.Sin(Time.time * frequanzi) * amplitude)),
            (GameObject.transform.position.z + (Mathf.Sin(Time.time * frequanzi) * amplitude))
            );
    }
    public void RotationUpdate(float amplitude, float frequanzi) {
        GameObject.transform.rotation = Quaternion.Euler(
            (GameObject.transform.rotation.x + (Mathf.Sin(Time.time * frequanzi) * amplitude)),
            (GameObject.transform.rotation.y + (Mathf.Sin(Time.time * frequanzi) * amplitude)),
            (GameObject.transform.rotation.z + (Mathf.Sin(Time.time * frequanzi) * amplitude))
            );
    }
    public void SclaeUpdate(float amplitude, float frequanzi) {
        GameObject.transform.localScale = new Vector3(
            (GameObject.transform.lossyScale.x + (Mathf.Sin(Time.time * frequanzi) * amplitude)),
            (GameObject.transform.lossyScale.y + (Mathf.Sin(Time.time * frequanzi) * amplitude)),
            (GameObject.transform.lossyScale.z + (Mathf.Sin(Time.time * frequanzi) * amplitude))
            );
    }


    public MVC_Test_Controller(MVC_Test_Model model) {
        this.model = model;
    }
    ~MVC_Test_Controller() {
        model = null;
    }
}
