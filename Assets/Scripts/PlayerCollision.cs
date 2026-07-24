using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject skillCheckUI;

    private bool hiding;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Enemy")) {
            if (hiding) {

            }
            Debug.Log("Collided with Enemy");
        }
        if(collision.gameObject.CompareTag("Pipe")) {
            skillCheckUI.GetComponent<UIManager>().StartSkillCheck();
        }
        else if (collision.gameObject.CompareTag("Hideout")) {

        }
        else if (collision.gameObject.CompareTag("Door")) {
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        
    }

    IEnumerator LoadNextScene() {
        yield return new WaitForSeconds(2f);
    }
}
