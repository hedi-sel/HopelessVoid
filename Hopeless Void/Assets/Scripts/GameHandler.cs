using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
	
	static private GameHandler m_Instance;
	static public GameHandler instance { get { return m_Instance; } }

	private State m_state;
	public State state{get{return m_state;}}
	public int curState;
	public string[] states;
	public SoundHandler soundHandler;

	void Awake(){
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}

	public void SetState(string name){
		for (int k=0; k<states.Length;k++) {
			if (name == states[k]) {
				curState = k;
				StartState ();
			}
		}
	}

	void StartState(){
		if (m_state != null) {
			m_state.Stop ();
		}
		SceneManager.LoadScene (states [curState]);
	}

	public void SetMState(State s){
		m_state = s;
	}

	void Start () {
		destination = transform.position;
	}
	void Update () {
		if (Input.GetButtonDown ("Right Click")) {
			mouseLastPos = new Vector3(0,0,0) ;
			destination = transform.position;
		}
		if (Input.GetButton("Right Click")) {
			destination = destination - getMouseSpeed() ;
		}
		if (destination != transform.position)
			transform.position = (destination + transform.position) / 2;
		if (Input.GetButtonUp ("Right Click")) {
			mouseLastPos = new Vector3 (0, 0, 0);
		}
	}

	public int mouseVelocity;
	private Vector3 destination;
	private Vector3 mouseLastPos;
	public Vector3 getMouseSpeed(){
		Vector3 speed;
		if (mouseLastPos != new Vector3(0,0,0))
			speed = (Input.mousePosition - mouseLastPos)* mouseVelocity / 1000;
		else
			speed = new Vector3(0,0,0) ;
		mouseLastPos = Input.mousePosition;
		speed.z = 0;
		return speed;
	}
}
