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
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gesture = gf.Device.Gesture;

public class JointOrientation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public GameObject gforce = null;
    public Quaternion quater; // for display in editor only
    public Gesture lastGesture = Gesture.Undefined;
    public Quaternion Quater;
    // Update is called once per frame.
    void Update()
    {
        GForceDevice gforceDevice = gforce.GetComponent<GForceDevice>(); //Get gforce data

        lastGesture = gforceDevice.gesture;  //Get Gesture
        quater = gforceDevice.quater;        //Get Quater

        
        Quater = new Quaternion(quater.y, -quater.x, quater.z, quater.w); //Correct rotation direction
        transform.rotation = Quater;    //Control arm rotation
    }

    //Show Quater 
    void OnGUI()
    {
        GUI.Label(new Rect(12, 68, Screen.width, Screen.height),  "Quater.W : " + quater.w);
        GUI.Label(new Rect(12, 98, Screen.width, Screen.height),  "Quater.X : " + quater.x);
        GUI.Label(new Rect(12, 128, Screen.width, Screen.height), "Quater.Y : " + quater.y);
        GUI.Label(new Rect(12, 158, Screen.width, Screen.height), "Quater.Z : " + quater.z);
    }

}
