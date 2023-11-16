using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    public float speed;
    public float jumpForce;

    

    [Header("Camera")]
    public float mouseSensibility;
    public float maxView, minView;
    private float rotationX;

    [Header("Health")]
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private float currentHealthPoints;

    private Rigidbody rb;
    private Camera cam;
    private WeaponController weaponController;
    #endregion

    #region controls
    private void Awake()
    {
        currentHealthPoints = maxHealthPoints;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        weaponController = GetComponent<WeaponController>();

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void Start()
    {
        HUDController.instance.UpdateHealthBar(currentHealthPoints/maxHealthPoints);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Update()
    {
        CameraView();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButton("Fire1"))
            if(weaponController.CanShoot())
                weaponController.Shoot();
        
    }
    #endregion

    #region Movement
    /// <summary>
    /// Player Movement Input Controller
    /// <summary>

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = (transform.right * x + transform.forward * z).normalized * speed;
        direction.y = rb.velocity.y;

        rb.velocity = direction;
    }

    /// <summary>
    /// Jump Action
    /// </summary>
    private void Jump()
    {
        //Throw a ray downwards from the player
        Ray ray = new Ray(transform.position + new Vector3(0,0.1f,0), Vector3.down);
        if (Physics.Raycast(ray, 0.4f))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Controls Camera view with Mouse inputs
    /// </summary>
    private void CameraView()
    {
        //Get from mouse Input X and Y Axis
        float y = Input.GetAxis("Mouse X") * mouseSensibility;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensibility;

        //Block rotation with min and max limits
        rotationX = Mathf.Clamp(rotationX, minView, maxView);

        //Rotate the camera
        cam.transform.localRotation = Quaternion.Euler(-rotationX,0, 0);
        //Rotate the player
        transform.eulerAngles += Vector3.up * y;
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints -= damage;
        HUDController.instance.UpdateHealthBar(currentHealthPoints / maxHealthPoints);


    }

    
    #endregion
}
