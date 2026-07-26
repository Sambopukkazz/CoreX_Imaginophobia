using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPanel;

    public string dialogue;
    private bool resetingDialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPanel.text = ObjectiveManager.repairedObjectCount + "/9" + "    " + dialogue; 
        if(dialogue != null && !resetingDialogue) {
            StartCoroutine(resetDialogue());
            resetingDialogue = true;
        }
    }

    IEnumerator resetDialogue() {
        yield return new WaitForSeconds(5f);
        dialogue = null;
        resetingDialogue= false;
    }
}
