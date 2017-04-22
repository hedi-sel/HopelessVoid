using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {

	// Use this for initialization
	public Dictionary<string,AudioClip> musics = new Dictionary<string,AudioClip>();
	public Dictionary<string,AudioClip> sounds = new Dictionary<string,AudioClip>();
	public AudioClip[] musiques;
	public AudioClip[] sons;
	public AudioSource musicPlayer;

	private const int NUMBER_SOUNDS = 3;
	private int currentSound = 0;
	private AudioSource[] soundPlayers = new AudioSource[NUMBER_SOUNDS] ;

	void Start () {
		foreach(AudioClip audioClip in musiques){
			musics.Add (audioClip.name, audioClip);
		}
		foreach(AudioClip audioClip in sons){
			sounds.Add (audioClip.name, audioClip);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playMusic (string name){
		musicPlayer.clip = musics[name];
		musicPlayer.Play ();
	}

	public void playSound (string name){
		soundPlayers [currentSound].PlayOneShot (sounds[name]);
		currentSound = (currentSound + 1) % NUMBER_SOUNDS;
	}
}
