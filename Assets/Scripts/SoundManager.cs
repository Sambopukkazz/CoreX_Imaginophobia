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
    [SerializeField] private List<AudioClip> playSFX;

    private AudioPlayer speaker;
    private GameObject speakerObject;

    void Start()
    {

    }

    void Update()
    {
        
    }
    public void PlayLockerSFX(Vector2 pos,bool open) {
        speakerObject = Instantiate(speakerPrefab, pos, Quaternion.identity);
        speaker = speakerObject.GetComponent<AudioPlayer>();
        if(open) speaker.PlayAudioClip(lockerSFX[0]);
        else speaker.PlayAudioClip(lockerSFX[1]);
    }

    public void PlayStepsClip(Vector3 pos) {
        speakerObject = Instantiate(speakerPrefab, pos, Quaternion.identity);
        speaker = speakerObject.GetComponent<AudioPlayer>();
        speaker.PlayFootStepSFX(stepsSFX);
    }
}
