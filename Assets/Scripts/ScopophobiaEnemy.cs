using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopophobiaEnemy : MonoBehaviour
{
    private GameObject player;
    private float moveDir;
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float waitSeconds = 8f;
    private SoundManager soundManager;
    private bool startMoving;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        soundManager.PlaySingleSound(transform.position,"Scopo",0);
        player = GameObject.FindWithTag("Player");
        if(player.transform.position.x > gameObject.transform.position.x) {
            moveDir = 1f;
        }
        else {
            moveDir = -1f;
        }
        StartCoroutine(StartMoving());
    }

    // Update is called once per frame
    void Update()
    {
        if(startMoving) {
            transform.Translate(new Vector2(moveDir * moveSpeed * Time.deltaTime, 0));
        }
    }

    private IEnumerator StartMoving() {
        yield return new WaitForSeconds(waitSeconds);
        startMoving = true;
    }
}
