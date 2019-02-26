/*
 * Copyright 2017, OYMotion Inc.
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 * FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 * COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS
 * OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED
 * AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF
 * THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
 * DAMAGE.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmChoose : MonoBehaviour {

    //The key definitions
    [HideInInspector]
    public ColorBlock GreenButton;
    [HideInInspector]
    public ColorBlock GrayButton;

    public GameObject anotherButton;
    public bool RightButton = false;

    // Use this for initialization
    void Start () {

        //Initialize the button state color
        GreenButton.normalColor = Color.green;
        GreenButton.highlightedColor = Color.green;
        GreenButton.pressedColor = Color.green;
        GreenButton.disabledColor = Color.green;
        GreenButton.colorMultiplier = 1;


        GrayButton.normalColor = Color.gray;
        GrayButton.highlightedColor = Color.gray;
        GrayButton.pressedColor = Color.gray;
        GrayButton.disabledColor = Color.gray;
        GrayButton.colorMultiplier = 1;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //Turn the selected button green
    public void ChangeToGreen()
    {
        gameObject.GetComponent<Button>().colors = GreenButton;
        anotherButton.GetComponent<Button>().colors = GrayButton;

        //true or false IsRight

        GestureGameControl.gestureGameControl.IsRight = RightButton ;
        Debug.Log(GestureGameControl.gestureGameControl.IsRight);

        ArmGameControl.instance.IsRight = RightButton;
        Debug.Log(ArmGameControl.instance.IsRight);
    }
}
