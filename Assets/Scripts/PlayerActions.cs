using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerActions : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 2f;
    private bool allowMovement = false;
    public Light2D spotLight { get; private set; }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spotLight = GetComponentInChildren<Light2D>();
        allowMovement = true;
    }

    void Update()
    {
        if (allowMovement) {
            float input = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);
        }
        
    }

    public void RuduceLightRadius(float amount) {
        spotLight.pointLightOuterRadius -= amount;
    }
}
