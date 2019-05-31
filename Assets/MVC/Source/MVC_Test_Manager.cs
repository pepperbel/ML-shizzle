using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC_Test_Manager : MonoBehaviour
{
    [SerializeField] GameObject mvcTest;
    public GameObject MVCTest {
        get { return mvcTest; }
        set { mvcTest = value; }
    }
    void Start() {
        if (!mvcTest) return;
        MVC_Test_View view = MVCTest.GetComponent<MVC_Test_View>();
        if (view) view.Controller = new MVC_Test_Controller(new MVC_Test_Model(MVCTest));
        else MVCTest.AddComponent<MVC_Test_View>().Controller = new MVC_Test_Controller(new MVC_Test_Model(MVCTest));
        GameObject go = GameObject.Find(MVCTest.name);
        if (!go) GameObject.Instantiate(MVCTest, Vector3.zero, Quaternion.identity);
    }

    void Update() {
        
    }
}
