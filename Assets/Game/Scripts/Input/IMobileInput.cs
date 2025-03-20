using UnityEngine;

public interface IMobileInput
{
    Vector2 MoveDirection { get; }
    Vector2 LookDirection { get; }
}