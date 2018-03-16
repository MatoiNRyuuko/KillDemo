using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTextChange : MonoBehaviour {
    public void Awake()
    {
        UIButton button = this.gameObject.GetComponent<UIButton>();
        button.state = UIButton.State.Disabled;
        button.GetComponent<BoxCollider>().enabled = false;
    }
}
