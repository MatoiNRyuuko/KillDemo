﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangScense : MonoBehaviour {
    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("OnGame");
        }
	}

}
