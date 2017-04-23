using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarBehavior : MonoBehaviour {

	public PeopleBarBehavior people;
	private BoxCollider2D box;
	private RectTransform rect;

	public int peopleMax;
	public bool hovered;

	public ActionPanelBehavior actionPanel;
	private ActionPanel[] actions;
	private int curAction;

	public HexagonBehavior hexagon;

	public void Awake(){
		rect = GetComponent<RectTransform> ();
		box = GetComponent<BoxCollider2D> ();
	}

	public void Open(HexagonBehavior _hexagon){
		hexagon = _hexagon;
		actions = _hexagon.getActionPlanelList ();
		curAction = 0;
		print ("Violence");
		actionPanel.SetActionPanel (actions [curAction]);
	}

	public void Update(){
		box.size = new Vector2(rect.rect.width,rect.rect.height);
	}

	void OnMouseEnter(){
		hovered = true;
	}

	void OnMouseExit(){
		hovered = false;
	}

	public void Mod (int i){
		bool t = hexagon.addPopulation(i);
		people.Refresh (hexagon.population, peopleMax);
	}
}
