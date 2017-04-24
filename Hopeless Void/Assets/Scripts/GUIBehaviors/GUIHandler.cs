using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {

	static private GUIHandler m_Instance;
	static public GUIHandler instance { get { return m_Instance; } }

	public GameObject gameOver;
	public GameObject victory;
	public GameObject nextTurn;
	public GameObject intro;

	void Awake(){
		nextTurn.SetActive (true);
		if (m_Instance != null) {
			Destroy (this);
		} else {
			m_Instance = this;
		}
	}

	public static void DeleteInstance(){
		m_Instance = null;
	}

	public TopBarBehavior top;
	public BottomBarBehavior bottom;
	private GameObject outline;
	private GameObject highlight;

	public GameObject hexagon;
	public Sprite outlineSprite;
	public Sprite highlightSprite;

	private HexagonBehavior outlined;
	private HexagonBehavior highlighted;

	void Start(){
		GetComponent<Canvas> ().worldCamera = GameHandler.instance.GetComponent<Camera>();
		Transform here = GameBoard.instance.transform;
		outline = Instantiate (hexagon, here);
		outline.GetComponent<SpriteRenderer> ().sprite = outlineSprite;
		highlight = Instantiate (hexagon, here);
		highlight.GetComponent<SpriteRenderer> ().sprite = highlightSprite;

		gameOver.SetActive(false);
		victory.SetActive(false);
		nextTurn.SetActive(true);

	}


	void Update(){
		if (Input.GetButtonDown ("Left Click")) {
			if (bottom.hovered) {

			} else if (highlighted != null && highlight.activeSelf) {
				GUIHandler.instance.Open (highlighted);
			}
		}
			
	
	}
		

	public void Highlight(HexagonBehavior _hexagon){
		highlighted = _hexagon;
		Vector3 pos = _hexagon.transform.position;
		highlight.transform.position = new Vector3(pos.x,pos.y,pos.z-5);
		highlight.SetActive (true);
	}

	public void UnHighlight(HexagonBehavior _hexagon){
		if (highlighted == _hexagon) {
			highlight.SetActive (false);
		}
	}

	public void Open(HexagonBehavior _hexagon){
		bottom.gameObject.SetActive (true);
		outlined = _hexagon;
		bottom.Open (_hexagon);
		Vector3 pos = _hexagon.transform.position;
		outline.transform.position = new Vector3(pos.x,pos.y,pos.z-5);
		outline.SetActive (true);
	}

	public void Refresh(){
		bottom.Change ();
	}

	public void Close(){
		bottom.Off ();
		outline.SetActive (false);
	}

	public void GameOver(){
		gameOver.SetActive(true);
		nextTurn.SetActive(false);
		//GameBoard.instance.gameObject.SetActive (false);

		SoundHandler.instance.playMusic ("gameOver");
	}
	public void Victory(){
		victory.SetActive(true);
		nextTurn.SetActive(false);
		intro.SetActive(true);
		//GameBoard.instance.gameObject.SetActive (false);

		SoundHandler.instance.playMusic ("victory");

	}
	public void Intro(){
		intro.SetActive(false);
	}
}
