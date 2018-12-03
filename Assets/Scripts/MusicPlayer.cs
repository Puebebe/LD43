using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	[SerializeField] public AudioClip[] clips;
	private AudioSource audioSource;

	void Start () {
		audioSource = FindObjectOfType<AudioSource>();
		audioSource.loop = false;
	}

	private AudioClip GetRandomClip(){
		return clips[Random.Range(0, clips.Length)];
	}

	void Update () {
		if (!audioSource.isPlaying)
		{
			audioSource.clip = GetRandomClip();
			audioSource.Play();
		}

	}
}
