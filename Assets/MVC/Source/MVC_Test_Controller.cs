using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC_Test_Controller {
    private MVC_Test_Model model;
    public GameObject GameObject {
        get { return model.gameObject; }
        set { model.gameObject = value; }
    }

    public void PositionUpdate(float xAngle, float yAngle, float zAngle) {
        GameObject.transform.position = new Vector3(
            GameObject.transform.position.x + Mathf.Cos(xAngle) * Time.deltaTime,
            GameObject.transform.position.y + Mathf.Cos(yAngle) * Time.deltaTime,
            GameObject.transform.position.z + Mathf.Cos(zAngle) * Time.deltaTime
            );
    }
    public void RotationUpdate(float xAngle, float yAngle, float zAngle) {
        GameObject.transform.position = new Vector3(
            GameObject.transform.rotation.x + Mathf.Cos(xAngle) * Time.deltaTime,
            GameObject.transform.rotation.y + Mathf.Cos(yAngle) * Time.deltaTime,
            GameObject.transform.rotation.z + Mathf.Cos(zAngle) * Time.deltaTime
            );
    }
    public void SclaeUpdate(float xAngle, float yAngle, float zAngle) {
        GameObject.transform.position = new Vector3(
            GameObject.transform.localScale.x + Mathf.Cos(xAngle) * Time.deltaTime,
            GameObject.transform.localScale.y + Mathf.Cos(yAngle) * Time.deltaTime,
            GameObject.transform.localScale.z + Mathf.Cos(zAngle) * Time.deltaTime
            );
    }


    public
    MVC_Test_Controller(MVC_Test_Model model) {
        this.model = model;
    }
    ~MVC_Test_Controller() {
        model = null;
    }
}
