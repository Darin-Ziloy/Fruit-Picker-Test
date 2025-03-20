using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour, IMobileInput
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private CanvasDrag canvasDrag;

    public Vector2 MoveDirection => joystick.Direction;
    public Vector2 LookDirection => canvasDrag.Direction;
}