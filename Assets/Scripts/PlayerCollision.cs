using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject skillCheckUI;
    [SerializeField] private GameObject transitionUI;

    private TextMeshProUGUI guideTxt;
    private PlayerActions playerActions;
    private Slider hideTimer;
    private bool hiding;
    private bool dead;

    public bool readyToHide;
    public bool readyToOpenDoor;
    public bool readyToRepair;

    void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        guideTxt = transitionUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (readyToHide && !hiding) {
                Debug.Log("Hide");
                hiding = true;
                playerActions.Hide();
            }
            else if (hiding) {
                hiding = false;
                playerActions.GetOutOfHiding();
            }
            else if (readyToRepair && skillCheckUI.GetComponent<UIManager>().skillCheckIsActive == false) {
                skillCheckUI.GetComponent<UIManager>().StartSkillCheck();
                playerActions.RepairObjects();
            }
            else if (readyToOpenDoor) { 
                LoadNextScene();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            if (hiding) {

            }
            else {
                dead = true;
                playerActions.allowMovement = false;
                StartCoroutine(LoadNextScene());
            }
        }
        if (collision.gameObject.CompareTag("Pipe")) {
            readyToRepair = true;
            collision.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Hideout")) {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            readyToHide = true;
        }
        else if (collision.gameObject.CompareTag("Door")) {
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Pipe")) {
            readyToRepair = true;
            collision.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Hideout")) {
            readyToHide = false;
            collision.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Door")) {

        }
    }

    IEnumerator LoadNextScene() {
        transitionUI.GetComponent<Animator>().SetTrigger("ChangeScene");
        yield return new WaitForSeconds(1f);
        if (dead) {
            guideTxt.text = "Try looking at the enemy on the right time!";
            //Load Current Scene Again and try reset each value (restart mission on that scene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else {
            //Load Next Scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
