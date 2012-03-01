using System;
using System.Windows.Forms;

namespace ClarionEdge.EditorMacros
{

    /// <summary>
    /// All possible events that macro can record
    /// </summary>
    [Serializable]
    public enum MacroEventType
    {
        MouseMove,
        MouseDown,
        MouseUp,
        MouseWheel,
        KeyDown,
        KeyUp
    }

    /// <summary>
    /// Series of events that can be recorded any played back
    /// </summary>
    [Serializable]
    public class MacroEvent
    {

        public MacroEventType MacroEventType;
        public EventArgs EventArgs;
        public int TimeSinceLastEvent;
        private string displayString;

        public MacroEvent(MacroEventType macroEventType, EventArgs eventArgs, int timeSinceLastEvent)
        {

            MacroEventType = macroEventType;
            EventArgs = eventArgs;
            TimeSinceLastEvent = timeSinceLastEvent;
            KeyEventArgs keyArgs = (KeyEventArgs)eventArgs;
            string eventTypeString;
            if (macroEventType == EditorMacros.MacroEventType.KeyDown)
                eventTypeString = "↓";
            else if (macroEventType == EditorMacros.MacroEventType.KeyUp)
                eventTypeString = "↑";
            else
                eventTypeString = macroEventType.ToString();

            displayString = eventTypeString + " " + keyArgs.KeyCode;

        }

        public string DisplayString
        { get { return displayString; } }
    }

}
