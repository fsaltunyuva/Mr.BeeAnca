using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    private float _intialSpeed;
    private Rigidbody2D rb;
    
    private Vector2 movement;
    [SerializeField] private GameObject winScreen, loseScreen;
    
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private TextMeshProUGUI movementCounterText; // UI'daki text objesi
    
    public float movementCounter; // Sürekli artacak olan değer
    public float counterIncreaseRate = 1f; // Saniyede ne kadar artacağı
    
    public bool canMove = true;

    public bool isOnEndPoint = false;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        _intialSpeed = speed;
    }
    
    void Update() 
    {
        if (!canMove) 
        {
            movement = Vector2.zero;
            return;
        }
        
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (movement.sqrMagnitude > 0.01f) 
        {
            // Zamanla ve belirlediğin hızla artar
            movementCounter += counterIncreaseRate * Time.deltaTime;
            movementCounterText.text = $"{movementCounter:F1}"; // UI'ı güncelle
        }
    }
    
    void FixedUpdate() 
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        rb.velocity = movement * speed * Time.fixedDeltaTime;

        if (movement != Vector2.zero) 
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f; 
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Traffic Mid Friction")) {
            speed = _intialSpeed * 0.5f;
        }
        else if (other.CompareTag("Traffic High Friction")) {
            speed = _intialSpeed * 0.25f;
        }
        else if (other.CompareTag("Traffic Low Friction")) {
            speed = _intialSpeed * 0.75f;
        }
        
        if(other.CompareTag("End Point")) {
            isOnEndPoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Traffic Mid Friction") || other.CompareTag("Traffic High Friction") || other.CompareTag("Traffic Low Friction")) {
            speed = _intialSpeed;
        }
        
        if (other.CompareTag("End Point")) {
            isOnEndPoint = false;
        }
    }
    
    public void ResetMovementCounter()
    {
        movementCounter = 0f;
        movementCounterText.text = $"{movementCounter:F1}";
    }
}
