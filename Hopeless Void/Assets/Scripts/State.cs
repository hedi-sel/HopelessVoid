using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {

	public void Awake(){
		GameHandler.instance.SetMState(this);
	}

	public virtual void Launch () {}

	public virtual void Stop () {}
}
