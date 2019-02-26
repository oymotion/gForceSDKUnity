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

using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Gesture = gf.Device.Gesture;

public class ArmControl : MonoBehaviour {
 
    public GameObject Hand_left_A;   //hand left upperarm
    public GameObject Hand_left_B;   //hand left lowerarm
    public GameObject Hand_right_A;  //hand right upperarm
    public GameObject Hand_right_B;  //hand right lowerarm

    public GameObject gforce = null;   
    public Quaternion quater; // for display in editor only
    public byte[] rawdata;

    public int BendAngle;
    int AngleMin = 200;
    int AngleMax = 0;

    public static ArmControl Armcontrol;

    // Use this for initialization
    void Start () {
        Armcontrol = this;

    }
	
	// Update is called once per frame
	void Update () {

    }


    //Perspective transformation,return Angle
    //isright : Judgment is left hand or right hand
    //EMGraw  : The original data
    private int GetBendAngle(bool isright, int EMGraw)
    {
        int Angle = 0;

        //Select upper and lower limits
        AngleMin = EMGraw < AngleMin ? EMGraw : AngleMin;  
        AngleMax = EMGraw > AngleMax ? EMGraw : AngleMax;

        //To calculate the Angle 
        Angle = isright ? ( -EMGraw + AngleMin ) :( EMGraw - AngleMax );

        return Angle;
    }


    //Control arm rotation
    //isright : Judgment is left hand or right hand
    public void ArmQuaterControl(bool isright)
    {
        GForceDevice gforceDevice = gforce.GetComponent<GForceDevice>(); //Get gforce data
        quater = gforceDevice.quater;        //Get Quater
        rawdata = gforceDevice.EMGRawdata;   //Get EMGRawdata

        BendAngle = GetBendAngle(isright, ((rawdata[7] + rawdata[15]) / 2));

        if (isright)
        {
            //Right Arm
            Hand_right_A.transform.localRotation = new Quaternion(quater.y, -quater.x, quater.z, quater.w); //Correct rotation direction
            Hand_right_B.transform.localRotation = Quaternion.Euler(Hand_right_A.transform.localRotation.x, BendAngle, Hand_right_A.transform.localRotation.z);  //Control arm bending
        }
        else
        {
            //Left Arm
            Hand_left_A.transform.rotation = new Quaternion(-quater.y, quater.x, -quater.z, -quater.w); //Correct rotation direction
            Hand_left_B.transform.localRotation = Quaternion.Euler(Hand_left_A.transform.localRotation.x, BendAngle, Hand_left_A.transform.localRotation.z); //Control arm bending
        }
    }
}
