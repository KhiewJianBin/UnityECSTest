using Unity.Entities;

public struct InputState : IComponentData
{
    public KeyState R_Key_State;
}

public enum KeyState
{
    None,
    KeyDown,
    KeyUp,
    KeyPress,
}
