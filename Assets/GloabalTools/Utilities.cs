using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultra {
    struct TimedMessage {
        string message;
        public string Message { get { return message; } }
        float time;
        public float Time { get { return time; } }
        public void SetTime(float time) { this.time -= time; }
        public TimedMessage(string message, float time) {
            this.message = message;
            this.time = time;
        }
    }
    public class Utilities : Singelton<Utilities> {
        List<string> onScreenList = new List<string>();
        List<TimedMessage> onScreenListTimed = new List<TimedMessage>();
        public string DebugLogString(string className, string functionCaller, string information) {
            return "[<color=teal>" + className + "::" + functionCaller + "</color>] " + "<color=blue>" + information + "</color>";
        }
        public string DebugErrorString(string className, string functionCaller, string information) {
            return "[<color=orange>" + className + "::" + functionCaller + "</color>] " + "<color=red>" + information + "</color>";
        }
        public void DebugLogOnScreen(string message) {
            onScreenList.Add(message);
        }
        public void DebugLogOnScreen(string message, float time) {
            onScreenListTimed.Add(new TimedMessage(message, time));
        }
        async void OnGUI() {
            await new WaitForEndOfFrame();
            for (int i = 0; i < onScreenList.Count; i++) {
                GUI.Label(new Rect(0, 0 + i * 10, 1000f, 1000f), onScreenList[i]);
            }
            for (int i = 0; i < onScreenListTimed.Count; i++) {
                GUI.Label(new Rect(0, 0 + (onScreenList.Count + i) * 10, 1000f, 1000f), onScreenListTimed[i].Message);
                onScreenListTimed[i].SetTime(Time.deltaTime);
                if (onScreenListTimed[i].Time <= 0) onScreenListTimed.RemoveAt(i);
            }
            onScreenList.Clear();
        }
        void LateUpdate() {
//             for (int i = 0; i < onScreenList.Count; i++) {
//                 GUI.Label(new Rect(100, i * 100, 200f, 200f), onScreenList[i]);
//             }
        }
    }
}
