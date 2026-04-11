using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    
    private Vector2 movement;
    [SerializeField] private GameObject winScreen, loseScreen;
    
    [SerializeField] private Animator playerAnimator;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update() 
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // FlipSprite();
        
        // if (lightPlacementInstance.gameStart)
        // {
        //     if(!pauseDepletion)
        //         _health -= _healthDepletionRate * Time.deltaTime;
        //     else
        //     {
        //         _health += _healthDepletionRate * Time.deltaTime;
        //     }
        // }
    }
    
    void FixedUpdate() 
    {
        rb.velocity = movement * speed * Time.fixedDeltaTime;

        if (movement != Vector2.zero) 
        {
            // Hareket yönüne göre açıyı hesapla (Radyan -> Derece)
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            /* Unity'de 0 derece sağa (X ekseni) bakar.
               Eğer senin üçgeninin "önü" yukarı bakıyorsa,
               hesaplanan açıdan 90 derece çıkarman gerekir.
            */
            rb.rotation = angle - 90f; 
        }
    }

    // private void FlipSprite()
    // {
    //     if (movement.x > 0)
    //         transform.localScale = new Vector3(1, 1, 1);
    //     else if (movement.x < 0)
    //         transform.localScale = new Vector3(-1, 1, 1);
    // }
}
