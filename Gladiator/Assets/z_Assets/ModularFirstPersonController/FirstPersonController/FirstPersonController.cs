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
    [SerializeField] private PlayerHealth _playerHealth;
    private Rigidbody rb;
    private Collider _playerCol;
    float camRotation;

    #region Camera Movement Variables
    public Camera playerCamera;
    public Transform camLook;
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

    #region Camera Zoom Variables

    public bool enableZoom = true;
    public bool holdToZoom = false;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public float zoomFOV = 30f;
    public float zoomStepTime = 5f;

    // Internal Variables
    private bool isZoomed = false;

    #endregion
    #endregion

    #region Movement Variables

    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

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
    // public bool useSprintBar = true;
    // public bool hideBarWhenFull = true;
    // public Image sprintBarBG;
    // public Image sprintBar;
    // public float sprintBarWidthPercent = .3f;
    // public float sprintBarHeightPercent = .015f;

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

    #region Crouch

    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchHeight = .75f;
    public float speedReduction = .5f;

    // Internal Variables
    private bool isCrouched = false;
    private Vector3 originalScale;

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
    public float GetTotalDashTime{get{return dashTotalCooldown;}}
    public float GetTotalForceFieldTime{get{return _forceFieldCoolDownTime;}}
    public float GetTotalFireBeamsTime{get{return _fireBeamsCoolDownTime;}}
    public float GetTotalGravityPoundTime{get{return _gravityPoundCoolDownTime;}}
    public float GetTotalThanosSnapTime{get{return _thanosSnapCoolDownTime;}}

    [Header("Dashing")]
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float upgradedDashForceAdd;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float upwardForce;
    [SerializeField] private float _lookUpThreshold;
    [SerializeField] private KeyCode dashKey = KeyCode.Space;
    [SerializeField] private DashBehaviour _dashBehaviour;
    private bool _hasBetterDash;
    private bool isDashing = false;
    private Vector3 dashStartPosition;
    public float dashTimer;
    public float dashTotalCooldown;
    public float dashCooldownTimer{get; private set;}


    [Header("Force Field")]
    [SerializeField] private float _forceFieldStrength;
    [SerializeField] private float _forceFieldSkillRadius;
    [SerializeField] private float _extraForceFieldRadius;
    float usedForceRange;
    [SerializeField] private Transform _centrePoint;
    [SerializeField] private LayerMask _enemyForceFieldLayer;
    [SerializeField] private float _forceFieldCoolDownTime;
    [SerializeField] private float _forceFieldTotalDuration;
    [SerializeField] private ParticleSystemForceField _forceFieldForceFieldEffect;
    private float _forceFieldDuration;
    public float _forceFieldTimer{get; private set;}
    private bool _canUseForceField;
    public bool AmIForceField{get; private set;}


    [Header("Shooting Fire Flames")]
    [SerializeField] private PlayerFireLane _weakFireBeam;
    [SerializeField] private PlayerFireLane _strongFireBeam;
    [SerializeField] private float _fireBeamsCoolDownTime;
    public float _fireBeamsTimer{get; private set;}
    private bool _canUseFireBeams;
    private List<PlayerFireLane> _currentFireBeams;
    private bool AmIShootingFireBeams;
    private int NumberOfBeams;


    [Header("Gravity Pound")]
    [SerializeField] private float _gravitySkillRadius;
    [SerializeField] private float _liftPower;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _gravityPoundCoolDownTime;
    [SerializeField] private float _firstRadiusIncrease;
    [SerializeField] private float _secondRadiusIncrease;
    public float _gravityPoundTimer{get; private set;}
    private bool _canUseGravityPound;
    private bool AmIGravityLifting;
    private bool FirstGravityCast;


    [Header("Thanos Snap")]
    [SerializeField] private Transform _enemyContainer; 
    [SerializeField] private AnimationClip _durationTilWhite;
    [SerializeField] private float _thanosSnapCoolDownTime;
    public float _thanosSnapTimer{get; private set;}
    private bool _canUseThanosSnap;
    private bool AmIUsingThanosSnap;
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
        originalScale = transform.localScale;
        jointOriginalPos = joint.localPosition;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }
    }

    void Start()
    {
        _forceFieldForceFieldEffect.enabled = false;
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
        if(PlayerUpgradesData.StarOne)
        {
            dashForce += upgradedDashForceAdd;
        }
        NotInAbilityState = false;

        //Timers
        dashCooldownTimer = dashTotalCooldown;
        isDashing = false;
        
        _forceFieldTimer = _forceFieldCoolDownTime;
        _canUseForceField = false;

        _fireBeamsTimer = _fireBeamsCoolDownTime;
        _canUseFireBeams = false;

        _gravityPoundTimer = _gravityPoundCoolDownTime;
        _canUseGravityPound = false;

        _thanosSnapTimer = _thanosSnapCoolDownTime;
        _canUseThanosSnap = false;
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
        #region NON-ABILITY

        if(PlayerUpgradesData.StarOne)
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
                    dashTimer = dashDuration;
                    rb.velocity = Vector3.zero;
                    Dash();
                }
            }
        }
        
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
            camLook.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        #region Camera Zoom

        if (enableZoom)
        {
            // Changes isZoomed when key is pressed
            // Behavior for toogle zoom
            if(Input.GetKeyDown(zoomKey) && !holdToZoom && !isSprinting)
            {
                if (!isZoomed)
                {
                    isZoomed = true;
                }
                else
                {
                    isZoomed = false;
                }
            }

            // Changes isZoomed when key is pressed
            // Behavior for hold to zoom
            if(holdToZoom && !isSprinting)
            {
                if(Input.GetKeyDown(zoomKey))
                {
                    isZoomed = true;
                }
                else if(Input.GetKeyUp(zoomKey))
                {
                    isZoomed = false;
                }
            }

            // Lerps camera.fieldOfView to allow for a smooth transistion
            if(isZoomed)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
            }
            else if(!isZoomed && !isSprinting)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, zoomStepTime * Time.deltaTime);
            }
        }

        #endregion
        #endregion

        #region Sprint

        if(enableSprint)
        {
            if(isSprinting)
            {
                isZoomed = false;
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
            // if(useSprintBar && !unlimitedSprint)
            // {
            //     float sprintRemainingPercent = sprintRemaining / sprintDuration;
            //     sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);
            // }
        }

        #endregion

        #region Jump

        // Gets input and calls jump method
        if(enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        #endregion

        #region Crouch

        if (enableCrouch)
        {
            if(Input.GetKeyDown(crouchKey) && !holdToCrouch)
            {
                Crouch();
            }
            
            if(Input.GetKeyDown(crouchKey) && holdToCrouch)
            {
                isCrouched = false;
                Crouch();
            }
            else if(Input.GetKeyUp(crouchKey) && holdToCrouch)
            {
                isCrouched = true;
                Crouch();
            }
        }

        #endregion

        CheckGround();

        if(enableHeadBob)
        {
            HeadBob();
        }

        #region Abiltiy Buttons
        
        if(Input.GetKeyDown(KeyCode.Alpha1) && !NotInAbilityState && _canUseForceField)
        {
            if(PlayerUpgradesData.StarTwo)
            {
                if(KillComboHandler.KillComboCounter >= 30)
                {
                    usedForceRange = _forceFieldSkillRadius + _extraForceFieldRadius;
                }
                else
                {
                    usedForceRange = _forceFieldSkillRadius;
                }
                if(PlayerUpgradesData.ShieldTwo)
                {
                    _playerHealth.TakeNoFireDamage = true;
                    _forceFieldForceFieldEffect.endRange = usedForceRange;
                    _forceFieldForceFieldEffect.enabled = true;
                }
                _forceFieldDuration = _forceFieldTotalDuration;
                AmIForceField = true;
                NotInAbilityState = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && !NotInAbilityState && _canUseFireBeams)
        {
            if(PlayerUpgradesData.StarThree)
            {
                _currentFireBeams = new List<PlayerFireLane>();
                Time.timeScale = 0.1f;
                PostProcessingEffectManager.Instance.StartSlowEffect();
                NumberOfBeams = GetNumberOfFireBeams();
                AmIShootingFireBeams = true;
                NotInAbilityState = true; 
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && !NotInAbilityState && _canUseGravityPound)
        {
            if(PlayerUpgradesData.StarFour)
            {
                FirstGravityCast = true;
                AmIGravityLifting = true;
                NotInAbilityState = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && !NotInAbilityState && _canUseThanosSnap)
        {
            if(PlayerUpgradesData.StarFive)
            {
                AmIUsingThanosSnap = true;
                NotInAbilityState = true;
            }
        }
        #endregion
        #endregion
        else if(NotInAbilityState)
        {
            if(AmIForceField)
            {
                if(_forceFieldDuration > 0)
                {
                    ForceField();
                    _forceFieldDuration -= Time.deltaTime;
                }
                else
                {
                    if(PlayerUpgradesData.ShieldTwo)
                    {
                        _playerHealth.TakeNoFireDamage = true;
                        _forceFieldForceFieldEffect.enabled = false;
                    }
                    _forceFieldTimer = _forceFieldCoolDownTime;
                    _canUseForceField = false;
                    NotInAbilityState = false;
                    AmIForceField = false;
                }               
            }
            if(AmIShootingFireBeams)
            {
                if(NumberOfBeams == 0)
                {
                    Time.timeScale = 1;
                    foreach (var fireLane in _currentFireBeams)
                    {
                        fireLane.ShootThem();
                    }
                    PostProcessingEffectManager.Instance.EndSlowEffect();
                    CameraEffectsSystem.Instance.ShakeCamera(12, 0.3f);
                    _fireBeamsTimer = _fireBeamsCoolDownTime;
                    _canUseFireBeams = false;
                    AmIShootingFireBeams = false;
                    NotInAbilityState = false;
                }
                if(Input.GetMouseButtonDown(0))
                {
                    ShootFireBeam();
                    NumberOfBeams--;
                }
            }
            if(AmIGravityLifting)
            {
                if(FirstGravityCast)
                {
                    FirstGravityCast = false;
                    _gravityPoundTimer = _gravityPoundCoolDownTime;
                    _canUseGravityPound = false;
                    StartCoroutine(GravityPound());
                }
            }
            if(AmIUsingThanosSnap)
            {
                AmIUsingThanosSnap = false;
                _thanosSnapTimer = _thanosSnapCoolDownTime;
                _canUseThanosSnap = false;
                StartCoroutine(KillHalfOfEnemies());
            }
        }

        #region AbilityTimers
        if(!_canUseForceField)
        {
            if(_forceFieldTimer < 0)
            {
                _canUseForceField = true;
            }
            _forceFieldTimer -= Time.deltaTime;
        }
        if(!_canUseFireBeams)
        {
            if(_fireBeamsTimer < 0)
            {
                _canUseFireBeams = true;
            }
            _fireBeamsTimer -= Time.deltaTime;
        }
        if(!_canUseGravityPound)
        {
            if(_gravityPoundTimer < 0)
            {
                _canUseGravityPound = true;
            }
            _gravityPoundTimer -= Time.deltaTime;
        }
        if(!_canUseThanosSnap)
        {
            if(_thanosSnapTimer < 0)
            {
                _canUseThanosSnap = true;
            }
            _thanosSnapTimer -= Time.deltaTime;
        }
        #endregion
    }

    private void ForceField()
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, usedForceRange, Vector3.up, 5, _enemyForceFieldLayer);

        foreach (RaycastHit hit in raycastHits)
        {
            if(hit.transform.TryGetComponent<Rigidbody>(out Rigidbody enemyRb))
            { 
                enemyRb.AddExplosionForce(_forceFieldStrength, _centrePoint.position, _forceFieldSkillRadius, 0 , ForceMode.Impulse);
                if(hit.transform.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyBehaviour))
                {
                    enemyBehaviour.StopEnemy(true);
                }
            }
        }  
    }

    IEnumerator GravityPound()
    {
        CameraEffectsSystem.Instance.ShakeCamera(1, 1f);
        float usedGravityRadius = _gravitySkillRadius;

        if(KillComboHandler.KillComboCounter >= 50)
        {
            usedGravityRadius = _firstRadiusIncrease + _secondRadiusIncrease;
        }
        else if(KillComboHandler.KillComboCounter >= 15)
        {
            usedGravityRadius = _firstRadiusIncrease;
        }

        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, usedGravityRadius, Vector3.up, 5, _enemyLayer);
        List<EnemyBehaviour> enemyBehaviours = new List<EnemyBehaviour>();
        
        foreach (RaycastHit hit in raycastHits)
        {
            EnemyBehaviour enemyBehaviour = hit.transform.GetComponent<EnemyBehaviour>();
            enemyBehaviour.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * (_liftPower), ForceMode.Impulse);
            enemyBehaviour.StopEnemy(false);
            enemyBehaviours.Add(enemyBehaviour);
        }

        yield return new WaitForSeconds(1.3f);

        foreach (var enemyBehaviour in enemyBehaviours)
        {
            enemyBehaviour.transform.GetComponent<Rigidbody>().AddForce(Vector3.down * _liftPower*4, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.2f);
        CameraEffectsSystem.Instance.ShakeCamera(20, 0.5f);
        foreach (var enemyBehaviour in enemyBehaviours)
        {
            enemyBehaviour.UpdateExplodePoint(Vector3.zero, true);
            enemyBehaviour.StartDeath();
        }
        
        AmIGravityLifting = false;
        NotInAbilityState = false;
    }

    private void ShootFireBeam()
    {
        PlayerFireLane tempFireLane;
        if(KillComboHandler.KillComboCounter >= 40)
        {
            tempFireLane = _strongFireBeam;
        }
        else
        {
            tempFireLane = _weakFireBeam;
        }
        PlayerFireLane playerFirelane = Instantiate(_weakFireBeam, transform.position, camLook.rotation);
        _currentFireBeams.Add(playerFirelane);
    }

    private int GetNumberOfFireBeams()
    {
        int num = 0;
        if(PlayerUpgradesData.ShieldFour)
        {
            num += 1;
        }
        if(KillComboHandler.KillComboCounter >= 60)
        {
            num += 3;
        }
        else if(KillComboHandler.KillComboCounter >= 20)
        {
            num += 2;
        }
        else
        {
            num += 1;
        }
        return num;
    }

    IEnumerator KillHalfOfEnemies()
    {
        Time.timeScale = 0.1f;
        InGameLevelManager.Instance.FlashScreenWhite();

        List<EnemyBehaviour> enemyBehaviours = new List<EnemyBehaviour>();

        foreach (Transform enemy in _enemyContainer)
        {
            if(enemy.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyBehaviour))
            {
                enemyBehaviours.Add(enemyBehaviour);
            }
        }
        yield return new WaitForSecondsRealtime(_durationTilWhite.length);

        int num = KillComboHandler.KillComboCounter/2;
        
        for (int i = 0; i < num; i++)
        {
            if(enemyBehaviours[i] != null)
            {
                enemyBehaviours[i].Thanosnaped();
            }
        }

        KillComboHandler.SetCombo(num);
        Time.timeScale = 1f;
        
        NotInAbilityState = false;
    }

    private void Dash()
    {
        SoundManager.Instance.PlaySound3D(SoundManager.Sound.DashEffect, transform.position);
        PostProcessingEffectManager.Instance.DashEffect(0.3f);

        if(_hasBetterDash)
        {
            _dashBehaviour.StartDash();
            if(KillComboHandler.KillComboCounter >= 25)
            {
                _playerCol.enabled = false;
                StartCoroutine(ActivteMyColAgain());
            }
        }

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

        isDashing = false;
        dashCooldownTimer = dashTotalCooldown;
    }

    IEnumerator ActivteMyColAgain()
    {
        yield return new WaitForSeconds(dashDuration);
        _playerCol.enabled = false;
    }

    public void CoolDownReduce(float value)
    {
        dashCooldownTimer -= value;
        _fireBeamsCoolDownTime -= value;
    }

    void FixedUpdate()
    {
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

                    if (isCrouched)
                    {
                        Crouch();
                    }

                    // if (hideBarWhenFull && !unlimitedSprint)
                    // {
                    //     sprintBarCG.alpha += 5 * Time.deltaTime;
                    // }
                }

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
            // All movement calculations while walking
            else
            {
                isSprinting = false;

                // if (hideBarWhenFull && sprintRemaining == sprintDuration)
                // {
                //     sprintBarCG.alpha -= 3 * Time.deltaTime;
                // }

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

        // When crouched and using toggle system, will uncrouch for a jump
        if(isCrouched && !holdToCrouch)
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        // Stands player up to full height
        // Brings walkSpeed back up to original speed
        if(isCrouched)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            walkSpeed /= speedReduction;

            isCrouched = false;
        }
        // Crouches player down to set height
        // Reduces walkSpeed
        else
        {
            transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
            walkSpeed *= speedReduction;

            isCrouched = true;
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
            // Calculates HeadBob speed during crouched movement
            else if (isCrouched)
            {
                timer += Time.deltaTime * (bobSpeed * speedReduction);
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