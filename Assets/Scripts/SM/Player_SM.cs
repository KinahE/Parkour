using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class Player_SM : Base_SM
{

    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public GameObject CameraTarget { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public EnvironmentChecker EnvironmentChecker { get; private set; }
    [field: SerializeField] public GroundedChecker GroundedChecker { get; private set; }
    [field: SerializeField] public PlayerAnimationManager PlayerAnimationManager { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;
    [field: SerializeField] public float SprintSpeed { get; private set; } = 30f;
    [field: SerializeField] public float Acceleration { get; private set; } = 20f;
    [field: SerializeField] public float JumpForce { get; private set; } = 10f;
    [field: SerializeField] public float Deceleration { get; private set; } = 15f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
    [field: SerializeField] public List<ParkourAction> ParkourActions { get; private set; }
    public bool InAction { get; private set; } = false;
    public bool HasControl { get; private set; } = true;
    public Transform MainCameraTransform { get; private set; }
    public Vector3 CurrentVelocity = Vector3.zero;

    void Awake()
    {
        MainCameraTransform = Camera.main.transform;
    }

    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
    
    public void SetInAction(bool active)
    {
        InAction = active;   
    }

    public void SetControl(bool active)
    {
        HasControl = active;
        Controller.enabled = active;

        if (!active)
        {
            Animator.SetFloat("movementSpeed", 0f);
        }
    }



}
