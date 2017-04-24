using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State {

	public override void Stop () {
		GameBoard.DeleteInstance ();
		ConstantBoard.DeleteInstance ();
		GUIHandler.DeleteInstance ();
	}
	float xMax; float xMin;	float yMax; float yMin;
	void Update () {

		float a =GameBoard.instance.GetComponent<Transform>().localScale.x;

		xMax = GameBoard.instance.xMax * a;
		xMin = GameBoard.instance.xMin * a;
		yMax = GameBoard.instance.yMax * a;
		yMin = GameBoard.instance.yMin * a;

		if (Input.GetButtonDown ("Right Click")) {
			mouseLastPos = new Vector3(0,0,0) ;
			destination = transform.position;
		}
		if (Input.GetButton("Right Click")) {
			destination = destination - getMouseSpeed() ;
		}
		if (destination != transform.position) {
			destination.z = GameHandler.instance.transform.position.z;
			GameHandler.instance.transform.position = borner ((destination + GameHandler.instance.transform.position) / 2);
		}
		if (Input.GetButtonUp ("Right Click")) {
			mouseLastPos = new Vector3 (0, 0, 0);
		}
	}

	/*bool accessible(Vector3 position){
		return position.x < xMax && position.y < yMax &&
		position.x > xMin && position.y > yMin;
			
	}*/

	Vector3 borner(Vector3 position){
		if (position.x > xMax)
			position.x = xMax;
		if (position.y > yMax)
			position.y = yMax;
		if (position.x < xMin)
			position.x = xMin;
		if (position.y < yMin)
			position.y = yMin;
		return position;
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
	void Start () {
		destination = transform.position;

	}

}
