using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVC_Test_Model {
    int Testint;
    float testFloat;
    string testString;

    MVC_Test_Model(int constructTest) {
        Testint = constructTest;
        testFloat = 0;
        testString = "None";
    }
}
