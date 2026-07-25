using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    private float moveSpeed = 2.5f;
    [SerializeField] private GameObject player;

    private PlayerActions playerActions;
    public bool allowMovement = true;
    private float startLightRadius;
    private float filmGrainActiveThreshold;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerActions = player.GetComponent<PlayerActions>();
        startLightRadius = playerActions.SpotLight.pointLightOuterRadius;
        filmGrainActiveThreshold = Random.Range(40f,60f);
        filmGrainActiveThreshold = (filmGrainActiveThreshold / 100) * startLightRadius;
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (allowMovement) {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position,input * moveSpeed * Time.deltaTime);
        }
        if(Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= playerActions.SpotLight.pointLightOuterRadius) {
            playerActions.ChangeLightRadius(0.1f);
            playerActions.ChangeGlobolVolume(0.1f, filmGrainActiveThreshold);
        }
        else {
            if(playerActions.SpotLight.pointLightOuterRadius < startLightRadius  && (input < 0 || input > 0)) {
                playerActions.ChangeLightRadius(-0.05f);
            }
            else if (input < 0 || input > 0) {
                playerActions.ChangeGlobolVolume(-0.05f, filmGrainActiveThreshold);
            }
        }
    }
}
