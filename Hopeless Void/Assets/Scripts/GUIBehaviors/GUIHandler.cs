using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHandler : MonoBehaviour {

	static private GUIHandler m_Instance;
	static public GUIHandler instance { get { return m_Instance; } }

	void Awake(){
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}

	private bool opened;

	public TopBarBehavior top;
	public BottomBarBehavior bottom;
	public GameObject highlight;

	public void Update(){
	}

	public void Open(Vector2 position){
		opened = true;
		bottom.gameObject.SetActive (true);
		highlight.transform.position = position;
		highlight.SetActive (true);
	}

	public void Close(){
		opened = false;
		bottom.gameObject.SetActive (false);
		highlight.SetActive (false);
	}
}
