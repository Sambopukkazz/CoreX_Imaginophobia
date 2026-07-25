using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource speaker;
    private static int stepOrder;
    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAudioClip(AudioClip clip) {
        Destroy(gameObject, clip.length + 0.5f);
        speaker.PlayOneShot(clip);
    }

    public void PlayFootStepSFX(List<AudioClip> stepsSFX) {
        speaker.maxDistance = 5;
        if (stepOrder < stepsSFX.Count) {
            Destroy(gameObject, stepsSFX[stepOrder].length + 0.5f);
            speaker.PlayOneShot(stepsSFX[stepOrder++]);
        }
        else {
            stepOrder = 0;
            Destroy(gameObject, stepsSFX[stepOrder].length + 0.5f);
            speaker.PlayOneShot(stepsSFX[stepOrder++]);
        }
    }
}
