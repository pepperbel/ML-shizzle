using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : Singelton<Utilities> {
    public string DebugLog(string className, string functionCaller, string information) {
        return "[<color=teal>" + className + "::" + functionCaller + "</color>] " + "<color=blue>" + information + "</color>";
    }
    public string DebugError(string className, string functionCaller, string information) {
        return "[<color=orange>" + className + "::" + functionCaller + "</color>] " + "<color=red>" + information + "</color>";
    }
}
