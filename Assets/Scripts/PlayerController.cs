using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField, Tooltip("Z Axis - Rolling")]
    float forwardSpeed;

    [SerializeField, Tooltip("X Axis - Pitching")]
    float strafeSpeed;

    [SerializeField, Tooltip("y Axis - Yawing")]
    float hoverSpeed;

    [Header("Acceleration")]
    [SerializeField]
    float forwardAcceleration;

    [SerializeField]
    float strafeAcceleration;

    [SerializeField]
    float hoverAcceleration;

    [Header("Roll")]
    [SerializeField]
    float rollSpeed;

    [SerializeField]
    float rollAcceleration;

    [Header("Look")]
    [SerializeField]
    float lookRateSpeed;

    Rigidbody _rigidbody;

    Vector2 _screenCenter;
    Vector2 _lookDirection;
    Vector2 _mouseDistance;

    float _activeForwardSpeed;
    float _activeStrafeSpeed;
    float _activeHoverSpeed;
    float _activeRoll;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _screenCenter = new Vector2(Screen.width * 0.5F, Screen.height * 0.5F);
    }

    private void Update()
    {
        HandleInputRoll();
        HandleInputForward();
        HandleInputStrafe();
        HandleInputHover();

    }

    private void HandleInputRoll()
    {
        _lookDirection = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float mouseX = (_lookDirection.x - _screenCenter.x) / _screenCenter.x;
        float mouseY = (_lookDirection.y - _screenCenter.y) / _screenCenter.y;

        _mouseDistance = Vector2.ClampMagnitude(new Vector2(mouseX, mouseY), 1.0F); 

        float currentRoll = Input.GetAxisRaw("Roll");
        _activeRoll = Mathf.Lerp(_activeRoll, currentRoll, rollAcceleration * Time.deltaTime);

    }

    private void HandleInputHover()
    {
        float currentHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;
        _activeHoverSpeed = Mathf.Lerp(_activeHoverSpeed, currentHoverSpeed, hoverAcceleration * Time.deltaTime);
    }

    private void HandleInputStrafe()
    {
        float currentStrafeSpeed = Input.GetAxisRaw("Strafe") * strafeSpeed;
        _activeStrafeSpeed = Mathf.Lerp(_activeStrafeSpeed, currentStrafeSpeed, strafeAcceleration * Time.deltaTime);
    }

    private void HandleInputForward()
    {
        float currentForwardSpeed = Input.GetAxisRaw("Forward") * forwardSpeed;
        currentForwardSpeed = Mathf.Clamp(currentForwardSpeed, forwardSpeed * 0.05F, forwardSpeed);
        _activeForwardSpeed = Mathf.Lerp(_activeForwardSpeed, currentForwardSpeed, forwardAcceleration * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        float x = -_mouseDistance.y * lookRateSpeed * Time.fixedDeltaTime;
        float y = _mouseDistance.x * lookRateSpeed * Time.fixedDeltaTime;
        float z = _activeRoll * rollSpeed * Time.fixedDeltaTime;

        transform.Rotate(x, y, z, Space.Self);
    }

    private void HandleMovement()
    {
        _rigidbody.position += transform.forward * _activeForwardSpeed * Time.fixedDeltaTime;
        _rigidbody.position += transform.right * _activeStrafeSpeed * Time.fixedDeltaTime;
        _rigidbody.position += transform.up * _activeHoverSpeed * Time.fixedDeltaTime;
    }
}

