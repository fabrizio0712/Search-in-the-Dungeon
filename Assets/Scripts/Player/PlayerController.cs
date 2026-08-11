using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Game Managers
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerUIManager playerUIManager;

    // Components
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform bodyOrientation;
    [SerializeField] private Collider noiseCollider;

    // Input Variables
    private float inputVertical;
    private float inputHorizontal;
    private bool inputJump;
    private bool inputCrouch;

    // Movement Variables
    [SerializeField] private float baseSpeed;
    [SerializeField] private float crouchMultiplier;

    private float speedMultiplier = 1;
    private float buffSpeed = 1;
    private float debuffSpeed = 1;
    private Vector3 moveDirection = Vector3.zero;
    
    // Gravity Variables
    private float gravity = -32f;
    private float groundedGravity = -0.1f;

    // Jump Variables
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float jumpTime = 0.5f;
    private float initialJumpVelocity = 8;
    private bool isJumping = false;

    // Health Variables
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;

    public Transform BodyOrientation { get => bodyOrientation; }
    public float BuffSpeed { get => buffSpeed; set => buffSpeed = value; }
    public float DebuffSpeed { get => debuffSpeed; set => debuffSpeed = value; }

    // -------- Variables De Debug ----------
    //private float jumpTimer = 0f;
    // --------------------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        SetupJumpVariables();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        InputUpdate();
        HandleSoundEmition();
        HandleHeight();
        HandleJump();
        MovementUpdate();
        HandleGravity();

        /* // --------- Debug del tiempo del salto -----------
        if (!characterController.isGrounded) jumpTimer += Time.deltaTime;
        else 
        {
            //Debug.Log("Tiempo estimado del Salto: " + jumpTimer);
            jumpTimer = 0f;
        }
        // ------------------------------------------------
        // --------- Debug altura del salto ---------------
        if (transform.position.y >= 1 && characterController.velocity.y > 0) Debug.Log("El salto alcanzó la siguiente altura: " + transform.position.y);
        // ------------------------------------------------ */
    }
    private void InputUpdate() 
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        inputJump = Input.GetButton("Jump");
        inputCrouch = Input.GetButton("Crouch");
    }
    private void MovementUpdate() 
    {
        HandleSpeedMultiplier();
        moveDirection = (bodyOrientation.forward * inputVertical + bodyOrientation.right * inputHorizontal).normalized * baseSpeed * speedMultiplier + new Vector3(0,moveDirection.y,0);
        characterController.Move(moveDirection * Time.deltaTime);
    }
    private void HandleSpeedMultiplier() 
    {
        if (inputCrouch) speedMultiplier = crouchMultiplier;
        else speedMultiplier = 1;
        speedMultiplier = speedMultiplier * buffSpeed * debuffSpeed;
    }
    private void HandleSoundEmition() 
    {
        if (!inputCrouch)
        {
            if (inputHorizontal == 0 && inputVertical == 0) noiseCollider.enabled = false;
            else noiseCollider.enabled = true;
        }
        else noiseCollider.enabled = false;
    }
    private void HandleGravity() 
    {
        if (characterController.isGrounded) 
        {
            moveDirection.y = groundedGravity;
        }
        else 
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
    }
    private void SetupJumpVariables()
    {
        float timeToApex = jumpTime / 2;
        initialJumpVelocity = (2 * jumpHeight) / timeToApex;
        gravity = -initialJumpVelocity / timeToApex;

        // -------- Debug de Valores Calculados --------
        //Debug.Log("Velocidad inicial para el salto: " + initialJumpVelocity);
        //Debug.Log("Gravedad Utilizada: " + gravity);
        // ---------------------------------------------
    }
    private void HandleJump() 
    {
        if (!isJumping && characterController.isGrounded && inputJump) 
        {
            isJumping = true;
            moveDirection.y = initialJumpVelocity;
        }
        else if(isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }
    private void HandleHeight() 
    {
        if (inputCrouch) 
        {
            characterController.height = 1;
            bodyOrientation.localScale = new Vector3(1,0.5f,1);
        }
        else 
        {
            characterController.height = 2;
            bodyOrientation.localScale = new Vector3(1, 1, 1);
        }
    }
    public void GetDamage(float damage) 
    {
        currentHealth -= damage;

        if (currentHealth > 0) playerUIManager.UpdateHealthBar(maxHealth, currentHealth);
        else
        {
            currentHealth = 0;
            playerUIManager.UpdateHealthBar(maxHealth, currentHealth);
            gameManager.LossRun();
        }
    }
    public void GetHeal(float heal) 
    {
        currentHealth += heal;
        if (currentHealth < maxHealth) currentHealth = maxHealth;
        playerUIManager.UpdateHealthBar(maxHealth,currentHealth);
    }
}
