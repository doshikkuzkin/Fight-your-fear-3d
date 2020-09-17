using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;

    public float timeBetweenSoundEffects;
    private float nextSoundEffectTime;

    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        if (!DataHolder.bossDead)
        if (Time.time >= nextSoundEffectTime)
        {
            int randomNaumber = Random.Range(0, clips.Length);
            source.clip = clips[randomNaumber];
            source.Play();
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }
    }
}
