using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject speakerPrefab;
    [SerializeField] private List<AudioClip> stepsSFX;

    private AudioPlayer speaker;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PlayAudioClip(Vector2 pos) {
        speaker = Instantiate(speakerPrefab, pos, Quaternion.identity).GetComponent<AudioPlayer>();
        speaker.PlayFootStepSFX(stepsSFX);
    }
}
