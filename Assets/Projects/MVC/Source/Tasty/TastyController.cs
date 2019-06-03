using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TastyController
{
    TastyModel model;
    public GameObject GameObject
    {
        get{ return model.gameobject; }
        set { model.gameobject = value; }
    }

    public void RotateSpriteZ(float value)
    {
        GameObject.transform.eulerAngles = new Vector3(this.GameObject.transform.eulerAngles.x, this.GameObject.transform.eulerAngles.y, this.GameObject.transform.eulerAngles.z + value);
        Debug.Log(value);

    }

    public TastyController(TastyModel model)
    {
        this.model = model;
    }
}
