using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutophobiaEnemy : MonoBehaviour
{
    private float moveSpeed = 2.5f;
    private GameObject player;

    private Animator animator;
    private PlayerActions playerActions;
    private SoundManager soundManager;
    public bool allowMovement = true;
    public bool dead;
    private float filmGrainActiveThreshold;

    void Start() {
        player = GameObject.FindWithTag("Player");
        playerActions = player.GetComponent<PlayerActions>();
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        animator = gameObject.GetComponent<Animator>();

        filmGrainActiveThreshold = Random.Range(40f, 60f);
        filmGrainActiveThreshold = ((filmGrainActiveThreshold / 100) * playerActions.startLightRadius) + 0.5f;
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        int parameter;
        switch (input) {
            case > 0: parameter = 1; break;
            case < 0: parameter = -1; break;
            default: parameter = 0; break;
        }
        animator.SetInteger("input", parameter);

        if (allowMovement) {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position,input * moveSpeed * Time.deltaTime);
        }
        if (!dead) {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) <= playerActions.SpotLight.pointLightOuterRadius - 0.5f) {
                playerActions.ChangeLightRadius(0.1f);
                playerActions.ChangeGlobolVolume(0.05f, filmGrainActiveThreshold);
            }
            else {
                if (playerActions.SpotLight.pointLightOuterRadius < playerActions.startLightRadius && (input < 0 || input > 0)) {
                    playerActions.ChangeLightRadius(-0.05f);
                }
                else if (input < 0 || input > 0) {
                    playerActions.ChangeGlobolVolume(-0.05f, filmGrainActiveThreshold);
                }
            }
        }
    }
    public void PlayStepsSound() {
        Invoke(nameof(DelayedStep), 0.1f);
    }
    private void DelayedStep() {
        soundManager.PlayStepsClip(transform.position, 10f);
    }
}
