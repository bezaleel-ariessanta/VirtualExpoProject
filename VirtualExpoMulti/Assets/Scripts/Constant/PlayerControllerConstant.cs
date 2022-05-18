using UnityEngine;

public class PlayerControllerConstant
{

    //please delete this if you want to use Unity New Input System
    public const KeyCode ACTIVE_MIC_KEY = KeyCode.V;
    public const KeyCode ACTIVE_CHAT_MESSAGE_KEY = KeyCode.Tab;

#if UNITY_EDITOR_OSX || PLATFORM_STANDALONE_OSX || UNITY_STANDALONE_OSX

    public const KeyCode SEND_THE_MESSAGE_ENTER = KeyCode.Return;

#elif UNITY_EDITOR_WIN || PLATFORM_STANDALONE || UNITY_STANDALONE

    public const KeyCode SEND_THE_MESSAGE_ENTER = KeyCode.KeypadEnter;

#endif

    public const KeyCode LEFT_SHIFT = KeyCode.LeftShift;

    public const string GET_AXIS_HORIZONTAL = "Horizontal";
    public const string GET_AXIS_VERTICAL = "Vertical";
    public const string GET_BUTTON_DOWN_JUMP = "Jump";

}
