using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Skill Check Objects")]
    [SerializeField] private GameObject skillCheckUI;
    [SerializeField] private Image needle;
    [SerializeField] private Image fillZone;
    [SerializeField] private Slider progressBar;

    private float rotationSpeed = 200f;
    private float currentAngle = 0f;
    private float fillZoneStartAngle = 0f;
    private float fillZoneEndAngle = 0f;

    public bool skillCheckIsActive = false;

    [SerializeField] private PlayerActions playerActions;

    void Start()
    {
        
    }

    void Update()
    {
        if (skillCheckIsActive) { 
            currentAngle += rotationSpeed * Time.deltaTime;
            needle.rectTransform.rotation = Quaternion.Euler(0f, 0f, -currentAngle);
            progressBar.value += 0.05f * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space)) {
                EvaluateInput();
            }

            if(progressBar.value == 1f) {
                skillCheckIsActive = false;
                skillCheckUI.SetActive(false);
                playerActions.allowMovement = true;
            }
        }
    }

    #region SkillCheckUI
    public void StartSkillCheck() {
        skillCheckUI.SetActive(true);
        progressBar.value = 0f;
        skillCheckIsActive = true;
        currentAngle = 0f;
        RandomZone();
    }

    private void RandomZone() {
        fillZoneStartAngle = Random.Range(0f, 315f);
        fillZoneEndAngle = fillZoneStartAngle + 45f;
        fillZone.rectTransform.rotation = Quaternion.Euler(0f, 0f, -fillZoneStartAngle);
        fillZone.fillAmount = 45f / 360f;
    }

    private void EvaluateInput() {
        float needleAngle = currentAngle % 360f;
        if(currentAngle < 0f) needleAngle += 360f;

        if (needleAngle >= fillZoneStartAngle && needleAngle <= fillZoneEndAngle) {
            progressBar.value += 0.2f;
        }
        else {
            progressBar.value -= 0.25f;
        }

        rotationSpeed *= -1f;
        RandomZone();
    }
    #endregion
}
