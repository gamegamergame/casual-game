using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    PlayerScript pScript;

    [SerializeField]
    GameManager manager;

    public void OnMove(InputAction.CallbackContext context)
    {
        pScript.MovePlayer(context.ReadValue<Vector2>());
    }

    public void OnMoveMouse(InputAction.CallbackContext context)
    {
        pScript.MouseMovePlayer();
    }
}


  