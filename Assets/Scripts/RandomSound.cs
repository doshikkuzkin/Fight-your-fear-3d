﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;

    void Start()
    {
        source = GetComponent<AudioSource>();
        int randomNaumber = Random.Range(0, clips.Length);
        source.clip = clips[randomNaumber];
        source.Play();
    }

    void Update()
    {

    }
}
