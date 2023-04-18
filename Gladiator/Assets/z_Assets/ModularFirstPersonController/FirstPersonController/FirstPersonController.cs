﻿// CHANGE LOG
// 
// CHANGES || version VERSION
//
// "Enable/Disable Headbob, Changed look rotations - should result in reduced camera jitters" || version 1.0.1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
    using UnityEditor;
    using System.Net;
#endif

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Movement Variables

    public Camera playerCamera;
    public Transform cameraMove;

    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;

    #endregion

    #region Movement Variables

    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

    // Internal Variables
    private bool isWalking = false;
    [SerializeField] private Transform _jumpPoint; 
    #region Sprint

    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;

    // Sprint Bar
    public bool useSprintBar = true;
    public bool hideBarWhenFull = true;

    public Image sprintBar;
    public float sprintBarWidthPercent = .3f;
    public float sprintBarHeightPercent = .015f;

    // Internal Variables
    private CanvasGroup sprintBarCG;
    private bool isSprinting = false;
    private float sprintRemaining;
    private float sprintBarWidth;
    private float sprintBarHeight;
    private bool isSprintCooldown = false;
    private float sprintCooldownReset;

    #endregion

    #region Jump

    public bool enableJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;

    // Internal Variables
    private bool isGrounded = false;
    [SerializeField] private LayerMask playerGroundlayerMask;
    #endregion

    #endregion

    #region Head Bob

    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;
    private float timer = 0;

    #endregion
    
    #region Abilites
    private bool NotInAbilityState;

    [Header("Dashing")]
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float upwardForce;
    [SerializeField] private float _lookUpThreshold;
    [SerializeField] private KeyCode dashKey = KeyCode.Space;
    private bool isDashing = false;
    private Vector3 dashStartPosition;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    [Header("Shooting Fire Flames")]
    private bool AmIShootingFireBeams;
    private int NumberOfBeams;
    #endregion

    #region Player Animations
    [Header("Animation Variables")]
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private int _numOfClicks;
    [SerializeField] float lastClickedTime = 0;
    float maxComboDelay = 0.8f;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        crosshairObject = GetComponentInChildren<Image>();

        // Set internal variables
        playerCamera.fieldOfView = fov;
        jointOriginalPos = joint.localPosition;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }
    }

    void Start()
    {
        //pm = GetComponent<PlayerMovementDashing>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(crosshair)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }
        else
        {
            crosshairObject.gameObject.SetActive(false);
        }
    }

    private void OnClick()
    {
        lastClickedTime = Time.time;
        _numOfClicks++;
        if(_numOfClicks == 1)
        {
            _playerAnim.SetBool("LeftSlice", true);
        }

        _numOfClicks = Mathf.Clamp(_numOfClicks, 0, 2);
    
        if(_numOfClicks >= 2 && _playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _playerAnim.GetCurrentAnimatorStateInfo(0).IsName("LeftSlice"))
        {
            _playerAnim.SetBool("RightSlice", true); 
            _playerAnim.SetBool("LeftSlice", false);
        }
    }

    private Vector3 GetDashDirection(Transform forwardT)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        Vector3 direction = new Vector3();

        direction = forwardT.forward * vertical + forwardT.right * horizontal;
        direction = direction.normalized;

        if (horizontal == 0 && vertical == 0)
        {
            direction = forwardT.forward;
        }
        return direction.normalized;
    }

    private void Update()
    {
        if(!NotInAbilityState)
        {

        
        #region Dash
        if(PlayerUpgradesData.AbilityAttribute >= 0)
        {
            if (dashCooldownTimer > 0)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(dashKey) && !isDashing)
                {
                    isDashing = true;
                    //dashStartPosition = transform.position;
                    dashTimer = dashDuration;
                    rb.velocity = Vector3.zero;
                    Dash();
                }
            }
        }
        #endregion

        #region Attack

        if(_playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _playerAnim.GetCurrentAnimatorStateInfo(0).IsName("LeftSlice"))
        {
            //_playerAnim.SetBool("LeftSlice", false);
        }
        if(_playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && _playerAnim.GetCurrentAnimatorStateInfo(0).IsName("RightSlice"))
        {
            _playerAnim.SetBool("RightSlice", false);
            _numOfClicks = 0;
        }

        if(Time.time - lastClickedTime > maxComboDelay)
        {
            _playerAnim.SetBool("LeftSlice", false);
            _numOfClicks = 0;
        }
        if(Input.GetMouseButtonDown(0))
        {
            OnClick();
        }

        #endregion

        #region Camera

        // Control camera movement
        if(cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            cameraMove.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
        #endregion

        #region Sprint

        if(enableSprint)
        {
            if(isSprinting)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);

                // Drain sprint remaining while sprinting
                if(!unlimitedSprint)
                {
                    sprintRemaining -= 1 * Time.deltaTime;
                    if (sprintRemaining <= 0)
                    {
                        isSprinting = false;
                        isSprintCooldown = true;
                    }
                }
            }
            else
            {
                // Regain sprint while not sprinting
                sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);
            }

            // Handles sprint cooldown 
            // When sprint remaining == 0 stops sprint ability until hitting cooldown
            if(isSprintCooldown)
            {
                sprintCooldown -= 1 * Time.deltaTime;
                if (sprintCooldown <= 0)
                {
                    isSprintCooldown = false;
                }
            }
            else
            {
                sprintCooldown = sprintCooldownReset;
            }

            // Handles sprintBar 
            if(useSprintBar && !unlimitedSprint)
            {
                float sprintRemainingPercent = sprintRemaining / sprintDuration;
                sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);
            }
        }

        #endregion

        #region Jump

        // Gets input and calls jump method
        if(enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        #endregion

        CheckGround();

        if(enableHeadBob)
        {
            HeadBob();
        }

        #region Abiltiy Buttons
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            NotInAbilityState = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 0.05f;
            NumberOfBeams = GetNumberOfFireBeams();
            AmIShootingFireBeams = true;
            NotInAbilityState = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            NotInAbilityState = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            NotInAbilityState = true;
        }
        #endregion
        }
        else if(NotInAbilityState)
        {
            if(AmIShootingFireBeams)
            {
                if(NumberOfBeams != 0)
                {
                    Time.timeScale = 1;
                }
                if(Input.GetMouseButtonDown(0))
                {
                    ShootFireBeam();
                    NumberOfBeams--;
                }
            }
        }
    }

    private void ShootFireBeam()
    {

    }

    private int GetNumberOfFireBeams()
    {
        if(KillComboHandler.KillComboCounter >= 40)
        {
            return 3;
        }
        else if(KillComboHandler.KillComboCounter >= 20)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private void Dash()
    {
        SoundManager.Instance.PlaySound3D(SoundManager.Sound.DashEffect, transform.position);
        PostProcessingEffectManager.Instance.DashEffect(0.3f);
        Transform forwardT = transform;
        Vector3 direction = GetDashDirection(forwardT);
        Vector3 forceToApply = Vector3.zero;

        Vector3 camDirection = playerCamera.transform.forward;
        Vector3 upForce = Vector3.zero;
        if(Vector3.Dot(camDirection, Vector3.up) > _lookUpThreshold)
        {
            forceToApply = direction * dashForce + transform.up * upwardForce;
        }
        else
        {
            forceToApply = direction * dashForce;
        }

        rb.AddForce(forceToApply, ForceMode.Impulse);
        rb.mass = 6f;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            if (dashTimer > 0)
            {
                dashTimer -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }
        }

        #region Movement

        if (playerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Checks if player is walking and isGrounded
            // Will allow head bob
            if (targetVelocity.x != 0 || targetVelocity.z != 0 && isGrounded)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            // All movement calculations shile sprint is active
            if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown)
            {
                targetVelocity = transform.TransformDirection(targetVelocity) * sprintSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                // Player is only moving when valocity change != 0
                // Makes sure fov change only happens during movement
                if (velocityChange.x != 0 || velocityChange.z != 0)
                {
                    isSprinting = true;


                    if (hideBarWhenFull && !unlimitedSprint)
                    {
                        sprintBarCG.alpha += 5 * Time.deltaTime;
                    }
                }

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
            // All movement calculations while walking
            else
            {
                isSprinting = false;

                if (hideBarWhenFull && sprintRemaining == sprintDuration)
                {
                    sprintBarCG.alpha -= 3 * Time.deltaTime;
                }

                targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }

        #endregion
    }

    // Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        Vector3 origin = _jumpPoint.position; //new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = _jumpPoint.TransformDirection(Vector3.down);
        float distance = .75f;
        Debug.DrawRay(origin, direction * distance, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance, playerGroundlayerMask))
        {
            isGrounded = true;
            rb.mass = 0.5f;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        // Adds force to the player rigidbody to jump
        if (isGrounded)
        {
            rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;
        }
    }


    private void HeadBob()
    {
        if(isWalking)
        {
            // Calculates HeadBob speed during sprint
            if(isSprinting)
            {
                timer += Time.deltaTime * (bobSpeed + sprintSpeed);
            }
            // Calculates HeadBob speed during walking
            else
            {
                timer += Time.deltaTime * bobSpeed;
            }
            // Applies HeadBob movement
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            // Resets when play stops moving
            timer = 0;
            joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
        }
    }
}


// Custom Editor
// #if UNITY_EDITOR
//     [CustomEditor(typeof(FirstPersonController)), InitializeOnLoadAttribute]
//     public class FirstPersonControllerEditor : Editor
//     {
//     FirstPersonController fpc;
//     SerializedObject SerFPC;

//     private void OnEnable()
//     {
//         fpc = (FirstPersonController)target;
//         SerFPC = new SerializedObject(fpc);
//     }

//     public override void OnInspectorGUI()
//     {
//         SerFPC.Update();

//         EditorGUILayout.Space();
//         GUILayout.Label("Modular First Person Controller", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
//         GUILayout.Label("By Jess Case", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
//         GUILayout.Label("version 1.0.1", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
//         EditorGUILayout.Space();

//         #region Camera Setup

//         EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
//         GUILayout.Label("Camera Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
//         EditorGUILayout.Space();

//         fpc.playerCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Camera", "Camera attached to the controller."), fpc.playerCamera, typeof(Camera), true);
//         fpc.fov = EditorGUILayout.Slider(new GUIContent("Field of View", "The camera’s view angle. Changes the player camera directly."), fpc.fov, fpc.zoomFOV, 179f);
//         fpc.cameraCanMove = EditorGUILayout.ToggleLeft(new GUIContent("Enable Camera Rotation", "Determines if the camera is allowed to move."), fpc.cameraCanMove);

//         GUI.enabled = fpc.cameraCanMove;
//         fpc.invertCamera = EditorGUILayout.ToggleLeft(new GUIContent("Invert Camera Rotation", "Inverts the up and down movement of the camera."), fpc.invertCamera);
//         fpc.mouseSensitivity = EditorGUILayout.Slider(new GUIContent("Look Sensitivity", "Determines how sensitive the mouse movement is."), fpc.mouseSensitivity, .1f, 10f);
//         fpc.maxLookAngle = EditorGUILayout.Slider(new GUIContent("Max Look Angle", "Determines the max and min angle the player camera is able to look."), fpc.maxLookAngle, 40, 90);
//         GUI.enabled = true;

//         fpc.lockCursor = EditorGUILayout.ToggleLeft(new GUIContent("Lock and Hide Cursor", "Turns off the cursor visibility and locks it to the middle of the screen."), fpc.lockCursor);

//         fpc.crosshair = EditorGUILayout.ToggleLeft(new GUIContent("Auto Crosshair", "Determines if the basic crosshair will be turned on, and sets is to the center of the screen."), fpc.crosshair);

//         // Only displays crosshair options if crosshair is enabled
//         if(fpc.crosshair) 
//         { 
//             EditorGUI.indentLevel++; 
//             EditorGUILayout.BeginHorizontal(); 
//             EditorGUILayout.PrefixLabel(new GUIContent("Crosshair Image", "Sprite to use as the crosshair.")); 
//             fpc.crosshairImage = (Sprite)EditorGUILayout.ObjectField(fpc.crosshairImage, typeof(Sprite), false);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             fpc.crosshairColor = EditorGUILayout.ColorField(new GUIContent("Crosshair Color", "Determines the color of the crosshair."), fpc.crosshairColor);
//             EditorGUILayout.EndHorizontal();
//             EditorGUI.indentLevel--; 
//         }

//         EditorGUILayout.Space();

//         #region Camera Zoom Setup

//         GUILayout.Label("Zoom", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

//         fpc.enableZoom = EditorGUILayout.ToggleLeft(new GUIContent("Enable Zoom", "Determines if the player is able to zoom in while playing."), fpc.enableZoom);

//         GUI.enabled = fpc.enableZoom;
//         fpc.holdToZoom = EditorGUILayout.ToggleLeft(new GUIContent("Hold to Zoom", "Requires the player to hold the zoom key instead if pressing to zoom and unzoom."), fpc.holdToZoom);
//         fpc.zoomKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Zoom Key", "Determines what key is used to zoom."), fpc.zoomKey);
//         fpc.zoomFOV = EditorGUILayout.Slider(new GUIContent("Zoom FOV", "Determines the field of view the camera zooms to."), fpc.zoomFOV, .1f, fpc.fov);
//         fpc.zoomStepTime = EditorGUILayout.Slider(new GUIContent("Step Time", "Determines how fast the FOV transitions while zooming in."), fpc.zoomStepTime, .1f, 10f);
//         GUI.enabled = true;

//         #endregion

//         #endregion

//         #region Movement Setup

//         EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
//         GUILayout.Label("Movement Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
//         EditorGUILayout.Space();

//         fpc.playerCanMove = EditorGUILayout.ToggleLeft(new GUIContent("Enable Player Movement", "Determines if the player is allowed to move."), fpc.playerCanMove);

//         GUI.enabled = fpc.playerCanMove;
//         fpc.walkSpeed = EditorGUILayout.Slider(new GUIContent("Walk Speed", "Determines how fast the player will move while walking."), fpc.walkSpeed, .1f, fpc.sprintSpeed);
//         GUI.enabled = true;

//         EditorGUILayout.Space();

//         #region Sprint

//         GUILayout.Label("Sprint", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

//         fpc.enableSprint = EditorGUILayout.ToggleLeft(new GUIContent("Enable Sprint", "Determines if the player is allowed to sprint."), fpc.enableSprint);

//         GUI.enabled = fpc.enableSprint;
//         fpc.unlimitedSprint = EditorGUILayout.ToggleLeft(new GUIContent("Unlimited Sprint", "Determines if 'Sprint Duration' is enabled. Turning this on will allow for unlimited sprint."), fpc.unlimitedSprint);
//         fpc.sprintKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Sprint Key", "Determines what key is used to sprint."), fpc.sprintKey);
//         fpc.sprintSpeed = EditorGUILayout.Slider(new GUIContent("Sprint Speed", "Determines how fast the player will move while sprinting."), fpc.sprintSpeed, fpc.walkSpeed, 20f);

//         //GUI.enabled = !fpc.unlimitedSprint;
//         fpc.sprintDuration = EditorGUILayout.Slider(new GUIContent("Sprint Duration", "Determines how long the player can sprint while unlimited sprint is disabled."), fpc.sprintDuration, 1f, 20f);
//         fpc.sprintCooldown = EditorGUILayout.Slider(new GUIContent("Sprint Cooldown", "Determines how long the recovery time is when the player runs out of sprint."), fpc.sprintCooldown, .1f, fpc.sprintDuration);
//         //GUI.enabled = true;

//         fpc.sprintFOV = EditorGUILayout.Slider(new GUIContent("Sprint FOV", "Determines the field of view the camera changes to while sprinting."), fpc.sprintFOV, fpc.fov, 179f);
//         fpc.sprintFOVStepTime = EditorGUILayout.Slider(new GUIContent("Step Time", "Determines how fast the FOV transitions while sprinting."), fpc.sprintFOVStepTime, .1f, 20f);

//         fpc.useSprintBar = EditorGUILayout.ToggleLeft(new GUIContent("Use Sprint Bar", "Determines if the default sprint bar will appear on screen."), fpc.useSprintBar);

//         // Only displays sprint bar options if sprint bar is enabled
//         if(fpc.useSprintBar)
//         {
//             EditorGUI.indentLevel++;

//             EditorGUILayout.BeginHorizontal();
//             fpc.hideBarWhenFull = EditorGUILayout.ToggleLeft(new GUIContent("Hide Full Bar", "Hides the sprint bar when sprint duration is full, and fades the bar in when sprinting. Disabling this will leave the bar on screen at all times when the sprint bar is enabled."), fpc.hideBarWhenFull);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             EditorGUILayout.PrefixLabel(new GUIContent("Bar BG", "Object to be used as sprint bar background."));
//             fpc.sprintBarBG = (Image)EditorGUILayout.ObjectField(fpc.sprintBarBG, typeof(Image), true);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             EditorGUILayout.PrefixLabel(new GUIContent("Bar", "Object to be used as sprint bar foreground."));
//             fpc.sprintBar = (Image)EditorGUILayout.ObjectField(fpc.sprintBar, typeof(Image), true);
//             EditorGUILayout.EndHorizontal();


//             EditorGUILayout.BeginHorizontal();
//             fpc.sprintBarWidthPercent = EditorGUILayout.Slider(new GUIContent("Bar Width", "Determines the width of the sprint bar."), fpc.sprintBarWidthPercent, .1f, .5f);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             fpc.sprintBarHeightPercent = EditorGUILayout.Slider(new GUIContent("Bar Height", "Determines the height of the sprint bar."), fpc.sprintBarHeightPercent, .001f, .025f);
//             EditorGUILayout.EndHorizontal();
//             EditorGUI.indentLevel--;
//         }
//         GUI.enabled = true;

//         EditorGUILayout.Space();

//         #endregion

//         #region Jump

//         GUILayout.Label("Jump", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

//         fpc.enableJump = EditorGUILayout.ToggleLeft(new GUIContent("Enable Jump", "Determines if the player is allowed to jump."), fpc.enableJump);

//         GUI.enabled = fpc.enableJump;
//         fpc.jumpKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Jump Key", "Determines what key is used to jump."), fpc.jumpKey);
//         fpc.jumpPower = EditorGUILayout.Slider(new GUIContent("Jump Power", "Determines how high the player will jump."), fpc.jumpPower, .1f, 20f);
//         GUI.enabled = true;

//         EditorGUILayout.Space();

//         #endregion

//         #region Crouch

//         GUILayout.Label("Crouch", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));

//         fpc.enableCrouch = EditorGUILayout.ToggleLeft(new GUIContent("Enable Crouch", "Determines if the player is allowed to crouch."), fpc.enableCrouch);

//         GUI.enabled = fpc.enableCrouch;
//         fpc.holdToCrouch = EditorGUILayout.ToggleLeft(new GUIContent("Hold To Crouch", "Requires the player to hold the crouch key instead if pressing to crouch and uncrouch."), fpc.holdToCrouch);
//         fpc.crouchKey = (KeyCode)EditorGUILayout.EnumPopup(new GUIContent("Crouch Key", "Determines what key is used to crouch."), fpc.crouchKey);
//         fpc.crouchHeight = EditorGUILayout.Slider(new GUIContent("Crouch Height", "Determines the y scale of the player object when crouched."), fpc.crouchHeight, .1f, 1);
//         fpc.speedReduction = EditorGUILayout.Slider(new GUIContent("Speed Reduction", "Determines the percent 'Walk Speed' is reduced by. 1 being no reduction, and .5 being half."), fpc.speedReduction, .1f, 1);
//         GUI.enabled = true;

//         #endregion

//         #endregion

//         #region Head Bob

//         EditorGUILayout.Space();
//         EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
//         GUILayout.Label("Head Bob Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
//         EditorGUILayout.Space();

//         fpc.enableHeadBob = EditorGUILayout.ToggleLeft(new GUIContent("Enable Head Bob", "Determines if the camera will bob while the player is walking."), fpc.enableHeadBob);
        

//         GUI.enabled = fpc.enableHeadBob;
//         fpc.joint = (Transform)EditorGUILayout.ObjectField(new GUIContent("Camera Joint", "Joint object position is moved while head bob is active."), fpc.joint, typeof(Transform), true);
//         fpc.bobSpeed = EditorGUILayout.Slider(new GUIContent("Speed", "Determines how often a bob rotation is completed."), fpc.bobSpeed, 1, 20);
//         fpc.bobAmount = EditorGUILayout.Vector3Field(new GUIContent("Bob Amount", "Determines the amount the joint moves in both directions on every axes."), fpc.bobAmount);
//         GUI.enabled = true;

//         #endregion

//         //Sets any changes from the prefab
//         if(GUI.changed)
//         {
//             EditorUtility.SetDirty(fpc);
//             Undo.RecordObject(fpc, "FPC Change");
//             SerFPC.ApplyModifiedProperties();
//         }
//     }

// }

// #endif