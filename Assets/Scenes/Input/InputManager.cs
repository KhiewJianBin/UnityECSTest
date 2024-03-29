using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public InputState inputstate;
    void Awake()
    {
        inputstate.R_Key_State = KeyState.None;
    }
    void Update()
    {
        if (inputstate.R_Key_State == KeyState.KeyUp)
        {
            inputstate.R_Key_State = KeyState.None;
        }
    }
    public void Rotate(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            inputstate.R_Key_State = KeyState.KeyPress;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            inputstate.R_Key_State = KeyState.KeyUp;
        }
    }
}