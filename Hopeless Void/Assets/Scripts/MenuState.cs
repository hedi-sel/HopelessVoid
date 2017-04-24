using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State {
	void Start(){
		SoundHandler.instance.playMusic ("intro");

	}

	override public void Launch() {

	}
}
