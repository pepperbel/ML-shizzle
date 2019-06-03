using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TastyManager : MonoBehaviour
{

    [SerializeField] GameObject myself;
    public GameObject Myself
    {
        get { return myself; }
        set { myself = value; }
    }


    void Start()
    {
        GameObject blah = GameObject.Instantiate(Myself);
        TastyView myView = blah.GetComponent<TastyView>();
        myView.Controller = new TastyController(new TastyModel(blah));

        
    }
}
