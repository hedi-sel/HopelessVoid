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
	private GameObject outline;
	private GameObject highlight;

	public GameObject hexagon;
	public Sprite outlineSprite;
	public Sprite highlightSprite;

	private HexagonBehavior outlined;
	private HexagonBehavior highlighted;

	void Start(){
		Transform here = GameBoard.instance.transform;
		outline = Instantiate (hexagon, here);
		outline.GetComponent<SpriteRenderer> ().sprite = outlineSprite;
		highlight = Instantiate (hexagon, here);
		highlight.GetComponent<SpriteRenderer> ().sprite = highlightSprite;
	}

	void Update(){
		if(Input.GetButtonDown ("Left Click")){
			if (bottom.hovered) {
				print ("Bottom");
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
		opened = true;
		bottom.gameObject.SetActive (true);
		outlined = _hexagon;
		Vector3 pos = _hexagon.transform.position;
		outline.transform.position = new Vector3(pos.x,pos.y,pos.z-5);
		outline.SetActive (true);
	}

	public void Close(){
		opened = false;
		bottom.gameObject.SetActive (false);
		outline.SetActive (false);
	}
}
