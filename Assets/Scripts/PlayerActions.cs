using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerActions : MonoBehaviour
{
    public GameObject eyePos;
    public LayerMask layerMask;

    private Rigidbody2D rb;
    private RaycastHit2D eyeSight;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rayDistance = 5f;
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

            float rayDir;

            switch(input) {
                case >0:
                    rayDir = 1;
                    break;
                case <0:
                    rayDir = -1;
                    break;
                default:
                    rayDir = 0;
                    break;
            }
            RadiateRay(rayDir);
        }
        
    }

    public void RuduceLightRadius(float amount) {
        spotLight.pointLightOuterRadius -= amount;
    }

    private void RadiateRay(float rayDir) {
        eyeSight = Physics2D.Raycast(eyePos.transform.position, Vector2.right * rayDir, rayDistance, layerMask);
        Debug.DrawRay(eyePos.transform.position, Vector2.right * rayDir * rayDistance, Color.yellow);

        if (eyeSight.collider != null && eyeSight.collider.CompareTag("Enemy")) {
            //Play Blink Animation
        }
    }
}
