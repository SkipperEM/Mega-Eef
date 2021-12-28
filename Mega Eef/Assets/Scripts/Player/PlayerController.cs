using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    bool isControllable = true;

    // CACHED OBJECTS
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera fpsCamera;
    [SerializeField] Animator animator;
    public PlayerUIController playerUIController;
    public GameObject playerMesh;

    // LOOKING
    float verticalLookRotation;
    public float mouseSensitivity = 3f;

    // HEALTH
    public int currentHealth;
    public int maxHealth = 100;


    #region MOVEMENT
    // MOVEMENT
    Vector3 moveDir;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    [SerializeField] float smoothTime;
    float movementModifer = 1f;
    public float moveSpeed = 5f;
    public float jumpForce = 240f;
    public bool isGrounded;
    #endregion

    // MENU
    [HideInInspector] public bool isPaused;
    bool inComputerScreen;
    [SerializeField] GameObject pausedPanel;

    // INVENTORY
    int currentItem;
    public WeaponBase currentActiveItem;
    public GameObject itemHolder;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHealth = maxHealth;
        currentItem = -1;
    }

    void Update()
    {

        ProcessMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (isPaused)
            return;

        int previousItem = currentItem;

        if (!isControllable)
            return;

        if (!inComputerScreen)
        {
            ProcessLooking();
            ProcessJump();

            ProcessInventoryKeys();
            if (previousItem != currentItem) // Checks if the player has changed inventory items
            {
                //inventoryHandler.SelectSlot(currentItem);

                SetItemActive();
            }

            // MOUSE CONTROLS
            if (Input.GetMouseButtonDown(0))
            {
                if (currentActiveItem == null)
                    return;
                currentActiveItem.Use();
            }
            //else if (Input.GetMouseButtonDown(1))
            //{
            //    if (currentActiveItem == null)
            //        return;
            //    if (currentItemBase.isActive)
            //        currentItemBase.AltUse();
            //}
        }
    }

    private void FixedUpdate()
    {
        if (!isControllable)
            return;
        if (isPaused)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        //inventoryHandler.UpdateResourceCount();
    }

    #region MOVEMENT
    void ProcessMovement()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (moveSpeed * movementModifer), ref smoothMoveVelocity, smoothTime);

        //if (moveDir != Vector3.zero)
        //{
        //    animator.SetBool("isWalking", true);
        //}
        //else
        //{
        //    animator.SetBool("isWalking", false);
        //}

        if (!isControllable || inComputerScreen)
        {
            moveDir = new Vector3(0, 0, 0);
            moveAmount = new Vector3(0, 0, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void ProcessLooking()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        fpsCamera.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void ProcessJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
    }

    public void SetGroundedState(bool grounded)
    {
        isGrounded = grounded;
    }
    #endregion

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pausedPanel.SetActive(isPaused);

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    #region INVENTORY
    void ProcessInventoryKeys()
    {
        if (Input.anyKeyDown)
        {
            switch (Input.inputString)
            {
                case "1":
                    currentItem = 0;
                    break;
                case "2":
                    currentItem = 1;
                    break;
                case "3":
                    currentItem = 2;
                    break;
                case "4":
                    currentItem = 3;
                    break;
                case "5":
                    currentItem = 4;
                    break;
                case "6":
                    currentItem = 5;
                    break;
            }
        }
    }

    public void SetItemActive()
    {
        int itemIndex = 0;

        currentActiveItem = null;

        foreach(Transform item in itemHolder.transform)
        {
            if (itemIndex == currentItem)
            {
                item.gameObject.SetActive(true);
                item.gameObject.GetComponent<WeaponBase>().isActive = true;
                currentActiveItem = item.gameObject.GetComponent<WeaponBase>();
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.GetComponent<WeaponBase>().isActive = false;
                item.gameObject.SetActive(false);
            }
            itemIndex++;
        }
    }
    #endregion

    #region HEALTH
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        playerUIController.ChangeHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        playerUIController.ChangeHealth(currentHealth);
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Die()
    {

    }
    #endregion
}
