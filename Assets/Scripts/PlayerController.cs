using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 startPosition;

    public float movementSpeed = 5f;
    public float jumpForce = 10;
    public float bounceForce = 10;
    public int direction = 1;

    public Vector3 initialPosition;
    public Vector3 finalPosition;

    private InputAction moveAction;
    public Vector2 moveDirection;
    private InputAction jumpAction;
    private InputAction _pauseAction;
    private InputAction _attackAction;

    public Rigidbody2D rBody2D;
    private SpriteRenderer render;
    private GroundSensor sensor;
    //--------------------------------- HE CAMBIANDO PRIVADOR A PUBLICO PORQ NO FUNCIONABA LA ANIMACION CAMINANDO 
    public Animator animator; 
    //------------------------------------------------------------------------------------------------------------
    private GameManager _gameManager;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    void Awake()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        sensor = GetComponentInChildren<GroundSensor>();
        animator = GetComponent<Animator>();
        //_gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
        //_pauseAction = InputSystem.actions["Pause"];
        //_attackAction = InputSystem.actions["Attack"];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.position = new Vector3(0, 0, 0);

        //transform.position = startPosition;  
    }

    // Update is called once per frame
    void Update()
    {
        /*if(_pauseAction.WasPressedThisFrame())
        {
            _gameManager.Pause();
        }

        if(_gameManager._pause == true)
        {
            return;
        }*/

        moveDirection = moveAction.ReadValue<Vector2>();

       
       
    //HE CAMBIANDO LA I DEL IS RUNNING POR UNA I EN MINUSCULA EN VEZ DE MAYUSCULA DE LOS TRES (lo he dejado igual que en el apartado de animator > parameters)
        if(moveDirection.x > 0)
        {
            render.flipX = false;
            animator.SetBool("isRunning", true);
        }
        else if(moveDirection.x < 0)
        {
            render.flipX = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    //____________________________----------------------------------------------------------------------------------   

        if(jumpAction.WasPressedThisFrame() && sensor.isGrounded)
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
         
        



        if(_attackAction.WasPressedThisFrame())
        {
            Shoot();
        }        
        //HE CAMBIADO EL LA I A MINUSCULA 
        animator.SetBool("isJumping", !sensor.isGrounded);
    }

    void FixedUpdate()
    {
        rBody2D.linearVelocity = new Vector2(moveDirection.x * movementSpeed, rBody2D.linearVelocity.y);
    }

    public void Bounce()
    {
        rBody2D.linearVelocity = new Vector2(rBody2D.linearVelocity.x, 0);
        rBody2D.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }
}