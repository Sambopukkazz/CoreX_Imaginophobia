using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;


public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject speakerPrefab;
    [SerializeField] private List<AudioClip> stepsSFX;
    [SerializeField] private List<AudioClip> lockerSFX;
    [SerializeField] private List<AudioClip> pipeSFX;
    [SerializeField] private List<AudioClip> elecSFX;

    [SerializeField] private UIManager uiManager;

    private AudioPlayer speaker;
    private GameObject speakerObject;
    private GameObject loopSpeaker;

    void Start()
    {

    }

    void Update()
    {
        if (uiManager.skillCheckIsActive == false && loopSpeaker != null) {
            Destroy(loopSpeaker);
        }
    }
    public void PlayRepairingClip(Vector2 pos, string obj) {
        speakerObject = Instantiate(speakerPrefab, pos, Quaternion.identity);
        speaker = speakerObject.GetComponent<AudioPlayer>();
        if(obj == "Pipe") {
            loopSpeaker = speaker.PlayLoopClip(pipeSFX[1]);
        }
        else {
            loopSpeaker = speaker.PlayLoopClip(elecSFX[1]);
        } 
    }

    public void PlaySingleSound(Vector2 pos, string sound,int index) {
        speakerObject = Instantiate(speakerPrefab, pos, Quaternion.identity);
        speaker = speakerObject.GetComponent<AudioPlayer>();
        if(sound == "Pipe") {
            speaker.PlayAudioClip(pipeSFX[index]);
        }
        else if(sound == "Electric") {
            speaker.PlayAudioClip(elecSFX[index]);
        }
        
    }

    public void PlayLockerSFX(Vector2 pos,bool open) {
        speakerObject = Instantiate(speakerPrefab, pos, Quaternion.identity);
        speaker = speakerObject.GetComponent<AudioPlayer>();
        if(open) speaker.PlayAudioClip(lockerSFX[0]);
        else speaker.PlayAudioClip(lockerSFX[1]);
    }

    public void PlayStepsClip(Vector3 pos, float maxDist) {
        speakerObject = Instantiate(speakerPrefab, pos, Quaternion.identity);
        speaker = speakerObject.GetComponent<AudioPlayer>();
        speaker.PlayFootStepSFX(stepsSFX, maxDist);
    }
}
