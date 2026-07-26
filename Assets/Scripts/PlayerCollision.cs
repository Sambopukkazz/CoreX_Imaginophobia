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
    [SerializeField] private GameObject zoomOutCam;
    [SerializeField] private DialogueManager dialogueManager;

    private TextMeshProUGUI guideTxt;
    private PlayerActions playerActions;
    private Slider hideTimer;
    private bool hiding;
    private bool dead;
    private string killedBy;
    public Collider2D collidedObj;
    public static string lastScene;

    public bool readyToHide;
    public bool readyToOpenDoor;
    public bool readyToRepair;
    public bool readyToClimb;

    public string obj;

    void Start()
    {
        playerActions = GetComponent<PlayerActions>();
        guideTxt = transitionUI.GetComponentInChildren<TextMeshProUGUI>();

        if (lastScene == "ER1" && SceneManager.GetActiveScene().name == "Sewer") {
            transform.position = new Vector2(11.5f, -0.5f);
        }
        else if(lastScene == "Sewer" && SceneManager.GetActiveScene().name == "ER2") {
            transform.position = new Vector2(-8.25f, -0.5f);
        }
        else if (lastScene == "Sewer" && SceneManager.GetActiveScene().name == "ER3") {
            transform.position = new Vector2(-8.25f, -0.5f);
        }
        else if (lastScene == "ER2" && SceneManager.GetActiveScene().name == "Sewer 2nd spot") {
            transform.position = new Vector2(4.5f, -0.5f);
        }
        else if (lastScene == "ER2" && SceneManager.GetActiveScene().name == "Sewer") {
            transform.position = new Vector2(-14.5f, -0.5f);
        }
        else if (lastScene == "ER3" && SceneManager.GetActiveScene().name == "Sewer") {
            transform.position = new Vector2(-9.5f, -22.5f);
        }
        else if (lastScene == "ER3" && SceneManager.GetActiveScene().name == "Sewer 2nd spot") {
            transform.position = new Vector2(-2.5f, -0.5f);
        }
        else if (lastScene == "Sewer 2nd spot" && SceneManager.GetActiveScene().name == "ER2") {
            transform.position = new Vector2(8.25f, -0.5f);
        }
        else if (lastScene == "Sewer 2nd spot" && SceneManager.GetActiveScene().name == "ER3") {
            transform.position = new Vector2(8.25f, -0.5f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (readyToHide && !hiding) {
                hiding = true;
                playerActions.Hide();
            }
            else if (hiding) {
                hiding = false;
                playerActions.GetOutOfHiding();
            }
            else if (readyToRepair && skillCheckUI.GetComponent<UIManager>().skillCheckIsActive == false) {
                skillCheckUI.GetComponent<UIManager>().StartSkillCheck();
                playerActions.RepairObjects(obj);
                collidedObj.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (readyToOpenDoor) {
                if (collidedObj.name == "End Hitbox") {
                    if(ObjectiveManager.repairedObjectCount == 9) {
                        StartCoroutine(LoadNextScene());
                    }
                    else {
                        dialogueManager.dialogue = "You feel obligated to not exit.";
                    }
                }
                else StartCoroutine(LoadNextScene());

            }
            else if (readyToClimb) {
                if(SceneManager.GetActiveScene().name == "Sewer") {
                    if(collidedObj.name == "Ladder Hitbox") {
                        transform.position = new Vector3(15.5f, -22.5f, 0f);
                    }
                    else if (collidedObj.name == "Ladder Hitbox 2") {
                        transform.position = new Vector3(15.5f, -0.5f, 0f);
                    }
                }
                readyToClimb = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        collidedObj = collision;
        if (collision.gameObject.CompareTag("Autophobia") || collision.gameObject.CompareTag("Scopophobia")) {
            dead = true;
            killedBy = collision.gameObject.tag;
            playerActions.allowMovement = false;
            StartCoroutine(LoadNextScene());
        }
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Electric")) {
            readyToRepair = true;
            obj = collision.gameObject.tag;
            collision.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Hideout")) {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            readyToHide = true;
        }
        else if (collision.gameObject.CompareTag("Door")) {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            readyToOpenDoor = true;
        }
        else if (collision.gameObject.CompareTag("Ladder")) {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            readyToClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Electric")) {
            readyToRepair = false;
            collision.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Hideout")) {
            readyToHide = false;
            collision.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Door")) {
            collision.transform.GetChild(0).gameObject.SetActive(false);
            readyToOpenDoor = false;
        }
        else if (collision.gameObject.CompareTag("Ladder")) {
            collision.transform.GetChild(0).gameObject.SetActive(false);
            readyToClimb = false;
        }
    }

    IEnumerator LoadNextScene() {
        if(collidedObj.name != "End Hitbox") {
            transitionUI.GetComponent<Animator>().SetTrigger("ChangeScene");
        }
        yield return new WaitForSeconds(1f);
        if (dead) {
            if (killedBy == "Autophobia") {
                guideTxt.text = "You have autophobia, don’t you?";

                yield return new WaitForSeconds(5f);

                guideTxt.text = "Listen closely… You never look back, do you?";

                yield return new WaitForSeconds(5f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (killedBy == "Scopophobia") {
                guideTxt.text = "Scopophobia? You don’t like being watched, do you?";
                yield return new WaitForSeconds(5f);
                guideTxt.text = "Doesn’t it make you want to hide?";
                yield return new WaitForSeconds(5f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
        else {
            lastScene = SceneManager.GetActiveScene().name;
            if(SceneManager.GetActiveScene().name == "ER1") {
                if (collidedObj.name == "Door Hitbox") {
                    SceneManager.LoadScene("Sewer");
                }
                else if(collidedObj.name == "End Hitbox") {
                    transitionUI.transform.GetChild(0).gameObject.SetActive(false);
                    zoomOutCam.gameObject.SetActive(true);
                    transitionUI.GetComponent<Animator>().Play("FadeOut");
                    yield return new WaitForSeconds(5f);
                    guideTxt.text = "You can only pray that it’s over";
                    yield return new WaitForSeconds(5f);
                    guideTxt.text = "Stepping into the light, would you assume that the darkness had merely given shape to something that was never there to begin with?";
                    yield return new WaitForSeconds(5f);
                    guideTxt.text = "Had the darkness birthed a horror that the light could only beg you to dismiss?";
                    yield return new WaitForSeconds(5f);
                    guideTxt.text = "...";
                    yield return new WaitForSeconds(5f);
                    guideTxt.text = "You could never say for sure.";
                    yield return new WaitForSeconds(5f);
                    guideTxt.text = "";
                }
            }
            else if(SceneManager.GetActiveScene().name == "Sewer") {
                if(collidedObj.name == "Door Hitbox") {
                    SceneManager.LoadScene("ER2");
                }
                else if(collidedObj.name == "Door Hitbox 2") {
                    SceneManager.LoadScene("ER1");
                }
                else if (collidedObj.name == "Door Hitbox 3") {
                    SceneManager.LoadScene("ER3");
                }
            }
            else if (SceneManager.GetActiveScene().name == "ER2") {
                if (collidedObj.name == "Door Hitbox 2") {
                    SceneManager.LoadScene("Sewer 2nd spot");
                }
                else if (collidedObj.name == "Door Hitbox") {
                    SceneManager.LoadScene("Sewer");
                }
            }
            else if (SceneManager.GetActiveScene().name == "ER3") {
                if (collidedObj.name == "Door Hitbox 2") {
                    SceneManager.LoadScene("Storage");
                }
                else if (collidedObj.name == "Door Hitbox") {
                    SceneManager.LoadScene("Sewer");
                }
                else if (collidedObj.name == "Door Hitbox 3") {
                    SceneManager.LoadScene("Sewer 2nd spot");
                }
            }
            else if (SceneManager.GetActiveScene().name == "Sewer 2nd spot") {
                if (collidedObj.name == "Door Hitbox 2") {
                    SceneManager.LoadScene("ER2");
                }
                else if (collidedObj.name == "Door Hitbox") {
                    SceneManager.LoadScene("ER3");
                }
            }
            else if (SceneManager.GetActiveScene().name == "Storage") {
                SceneManager.LoadScene("ER3");
            }
        }
    }
}
