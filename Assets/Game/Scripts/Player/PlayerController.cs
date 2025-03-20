using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform playerCamera;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float touchSensitivity = 1f;
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;

    [Header("Smoothing Settings")]
    [SerializeField] private float smoothTime = 0.12f;  // Adjust for desired smoothing

    private IMobileInput _mobileInput;
    private Vector3 _velocity;
    private float _verticalRotation;
    private bool _isGrounded;

    private Vector2 _currentCameraVelocity;
    private Vector2 _targetCameraRotation;


    [Inject]
    public void Construct(IMobileInput mobileInput)
    {
        _mobileInput = mobileInput;
    }

    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
        HandleCameraRotation();
    }

    private void HandleMovement()
    {
        Vector2 input = _mobileInput.MoveDirection;

        if (input.magnitude >= 0.1f)
        {
            Vector3 forward = playerCamera.forward;
            Vector3 right = playerCamera.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = (forward * input.y + right * input.x).normalized;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        _isGrounded = controller.isGrounded;
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        _targetCameraRotation = _mobileInput.LookDirection;
        Vector2 smoothInput = Vector2.SmoothDamp(Vector2.zero, _targetCameraRotation, ref _currentCameraVelocity, smoothTime);

        _verticalRotation -= smoothInput.y;
        _verticalRotation = Mathf.Clamp(_verticalRotation, minVerticalAngle, maxVerticalAngle);
        playerCamera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * smoothInput.x);

        _targetCameraRotation = Vector2.zero;
    }
}