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
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        _intialSpeed = speed;
    }
    
    void Update() 
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    
    void FixedUpdate() 
    {
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
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Traffic Mid Friction") || other.CompareTag("Traffic High Friction") || other.CompareTag("Traffic Low Friction")) {
            speed = _intialSpeed;
        }
    }
}
