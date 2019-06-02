using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ultra {
    public class Utilities : Singelton<Utilities> {
        List<string> onScreenList = new List<string>();
        List<TimedMessage> onScreenListTimed = new List<TimedMessage>();

        private int offset = 12;
        public string DebugLogString(string className, string functionCaller, string information) {
            return "[" + StringColor.Teal + className + "::" + functionCaller + StringColor.EndColor + "]" + StringColor.Blue + information + StringColor.EndColor;
        }
        public string DebugErrorString(string className, string functionCaller, string information) {
            string info = "[" + StringColor.Orange + className + "::" + functionCaller + StringColor.EndColor + "]" + StringColor.Red + information + StringColor.EndColor;
            DebugLogOnScreen(info, 10);
            return info;
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
                GUI.Label(new Rect(0, 0 + i * offset, 1000f, 1000f), onScreenList[i]);
            }
            // List is every second tick cleaned, don't know why
            if (onScreenList.Count > 0) {
                for (int i = 0; i < onScreenListTimed.Count; i++) {
                    GUI.Label(new Rect(0, 0 + (onScreenList.Count + i) * offset, 1000f, 1000f), onScreenListTimed[i].Message);
                    onScreenListTimed[i].Time -= Time.deltaTime;
                    if (onScreenListTimed[i].Time <= 0) onScreenListTimed.RemoveAt(i);
                }
            }
            onScreenList.Clear();
        }
    }
}
