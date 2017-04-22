using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
	

	static private GameHandler m_Instance;
	static public GameHandler instance { get { return m_Instance; } }

	public State gameState;
	public State menuState;
	public SoundHandler soundHandler;

	void Awake(){
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		//soundHandler.playMusic ("test");
	}
	
	// Update is called once per frame
	void Update () {

	}



}
