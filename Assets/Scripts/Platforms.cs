using UnityEngine;
using UnityEngine.InputSystem;
public class Platforms : MonoBehaviour
{
    private BoxCollider2D _boxcollider;

    private InputAction _moveAction;
    private Vector2 _moveInput;

    [SerializeField] private bool isOnPlatform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        _boxcollider = GetComponent<BoxCollider2D>();
        _moveAction = InputSystem.actions["Move"];
    }
    // Update is called once per frame
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        Debug.Log(_moveInput);

        if (_moveInput.y < 0 && isOnPlatform)
        {
            _boxcollider.isTrigger = true;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = true;
            isOnPlatform = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOnPlatform = false;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    { 
        if(collider.gameObject.CompareTag("Player"))
        {
            _boxcollider.isTrigger = false;
        }
        
         
    }
   


}

