using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeeperDeeper : MonoBehaviour {

    void OnPress(bool isDown)
    {
        if (isDown)
        {
            this.gameObject.GetComponent<UISprite>().spriteName = "shan";
        }

    }

}
