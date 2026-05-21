using UnityEngine;

public class Camera : MonoBehaviour 




{
    public GameObject _virtualCamera; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& !collision.isTrigger)
        {
            _virtualCamera.SetActive(true); 
        }
    
    }
        private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& !collision.isTrigger)
        {
            _virtualCamera.SetActive(false); 
        }
    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
}
