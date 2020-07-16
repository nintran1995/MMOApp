using System;
using System.Linq;

namespace ZChangerMMO.Events
{
    public enum AddNewProfileActionType
    {
        ADD_SUCCESS,
        CLOSE_DIALOG
    }

    public class AddNewProfileEventArgs : EventArgs
    {
        public AddNewProfileEventArgs(AddNewProfileActionType actionType, string message)
        {
            Message = message;
            ActionType = actionType;
        }

        public AddNewProfileActionType ActionType { get; private set; }

        public string Message { get; private set; }
    }
}
