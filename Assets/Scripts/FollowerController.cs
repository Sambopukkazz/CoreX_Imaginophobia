using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    private float moveSpeed = 2.5f;
    [SerializeField] private GameObject player;

    private PlayerActions playerActions;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerActions = player.GetComponent<PlayerActions>();
    }

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0) {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        if(Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= playerActions.spotLight.pointLightOuterRadius) {
            playerActions.RuduceLightRadius(0.1f);
        }
    }
}
