using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public GameObject eyePos;
    public LayerMask layerMask;
    [SerializeField] private GameObject zoomCam;
    [SerializeField] private GameObject skillCheckUI;
    [SerializeField] private Animator blinkVFX;

    private Rigidbody2D rb;
    private RaycastHit2D eyeSight;
    [SerializeField] private GameObject globalVolume;
    [SerializeField] private SoundManager soundManager;
    private Vignette vignette;  
    private ColorAdjustments colorAdjustments;
    private FilmGrain filmGrain;
    private Animator animator;
	public float startLightRadius {  get; private set; }

	[SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rayDistance = 2f;
    public bool allowMovement;
    
    public Light2D SpotLight { get; private set; }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpotLight = GetComponentInChildren<Light2D>();
        animator = GetComponent<Animator>();
        soundManager = soundManager.GetComponent<SoundManager>();
        
        globalVolume.GetComponent<Volume>().profile.TryGet(out colorAdjustments);
        globalVolume.GetComponent<Volume>().profile.TryGet(out filmGrain);
        globalVolume.GetComponent<Volume>().profile.TryGet(out vignette);
        allowMovement = true;
        startLightRadius = SpotLight.pointLightOuterRadius;
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

        if (skillCheckUI.GetComponent<UIManager>().skillCheckIsActive) {
            animator.SetBool("repair", true);
        }
        else {
            animator.SetBool("repair", false);
        }
        if (allowMovement) {
            
            rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);
        }

        float rayDir;
        switch (input) {
            case > 0:
                rayDir = 1;
                break;
            case < 0:
                rayDir = -1;
                break;
            default:
                rayDir = 0;
                break;
        }
        RadiateRay(rayDir);
    }

    public void ChangeLightRadius(float amount) {
        SpotLight.pointLightOuterRadius -= amount;
    }

    public void ChangeGlobolVolume(float amount, float threshold) {
        rayDistance = threshold;
        if((colorAdjustments.saturation.value - amount / SpotLight.pointLightOuterRadius * 100) <= 0) {
            colorAdjustments.saturation.value -= amount / SpotLight.pointLightOuterRadius * 100;
        }
        if(SpotLight.pointLightOuterRadius < threshold) {
            filmGrain.active = true;
        }
        else {
            filmGrain.active = false;
        }
    }

    private void RadiateRay(float rayDir) {
        eyeSight = Physics2D.Raycast(eyePos.transform.position, Vector2.right * rayDir, rayDistance, layerMask);
        Debug.DrawRay(eyePos.transform.position, Vector2.right * rayDir * eyeSight.distance, Color.yellow);

        if (eyeSight.collider != null && eyeSight.collider.CompareTag("Autophobia")) {
            eyeSight.collider.GetComponent<AutophobiaEnemy>().allowMovement = false;
            Destroy(eyeSight.collider.gameObject, 1f);
            eyeSight.collider.gameObject.GetComponent<AutophobiaEnemy>().dead = true;
            blinkVFX.Play("Blink");
            Invoke(nameof(ResetPostProcessing), 1f);
        }
    }

    public void Hide() {
        //show hiding timer UI
        allowMovement = false;
        transform.GetComponent<SpriteRenderer>().enabled = false;
        soundManager.PlayLockerSFX(transform.position, true);
        vignette.active = true;
        zoomCam.SetActive(true);
    }
    public void GetOutOfHiding() {
        allowMovement = true;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        soundManager.PlayLockerSFX(transform.position, false);
        vignette.active = false;
        zoomCam.SetActive(false);
    }
    private void HideCountDown(GameObject hideout,Slider hideTimer) {
        if (hideTimer == null) {
            hideTimer = hideout.transform.GetChild(0).gameObject.GetComponent<Slider>();
        }
        hideTimer.value -= 0.2f * Time.deltaTime;
        if (hideTimer.value == 0f) {
            GetOutOfHiding();
            hideTimer.value = 1f;
        }
    }

    public void RepairObjects(string obj) {
        soundManager.PlayRepairingClip(transform.position, obj);
        rb.velocity = Vector2.zero;
        allowMovement = false;
    }

    private void ResetPostProcessing() {
		SpotLight.pointLightOuterRadius = startLightRadius;
		filmGrain.active = false;
		colorAdjustments.saturation.value = 0;
	}

    public void PlayStepsSound() {
        soundManager.PlayStepsClip(transform.position, 5f);
    }

    public void PlayPipeSkillCheckSound(bool success) {
        if(success) {
            soundManager.PlaySingleSound(transform.position, "Pipe", 2);
        }
        else {
            soundManager.PlaySingleSound(transform.position, "Pipe", 3);
        }
        
    }
    public void PlayElecSkillCheckSound(bool success) {
        if (success) {
            soundManager.PlaySingleSound(transform.position, "Electric", 2);
        }
        else {
            soundManager.PlaySingleSound(transform.position, "Electric", 3);
        }

    }

    public void PlayRepairCompleteSound(string obj) {
        soundManager.PlaySingleSound(transform.position, obj, 0);
    }
}
