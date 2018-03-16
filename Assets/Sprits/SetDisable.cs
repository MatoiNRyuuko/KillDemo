using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisable : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIButton button = this.gameObject.GetComponent<UIButton>();
        button.state = UIButton.State.Disabled;
        button.GetComponent<BoxCollider>().enabled = false;

    }
}
