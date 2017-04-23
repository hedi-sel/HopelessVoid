using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State {

	public override void Stop () {
		GameBoard.DeleteInstance ();
		ConstantBoard.DeleteInstance ();
		GUIHandler.DeleteInstance ();
	}
}
