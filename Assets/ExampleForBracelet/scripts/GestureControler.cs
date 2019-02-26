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
using UnityEngine.UI;
using Gesture = gf.Device.Gesture;

public class GestureControler : MonoBehaviour {

    //Gestures animation
    public Animator AnimationLeft;
    public Animator AnimationRight;

    //
    public GameObject Hand_left;
    public GameObject Hand_right;

    public GameObject gforce = null;   
    public Quaternion quater; // for display in editor only
    public Gesture lastGesture = Gesture.Undefined;
    public Gesture gestures;
    public Quaternion Quater;

    private DateTime relaxStart = DateTime.Now;
    private const int RelaxDelay = 300;

    public static GestureControler gesturecontroler;

    //Gesture Name
    private string[] GesturesName = { "Fist", "Spread", "WaveIn", "WaveOut", "Pinch", "Shoot", "Relax" };
    private enum Gesturex
    {
        Fist,
        Spread,
        WaveIn,
        WaveOut,
        Pinch,
        Shoot,
        Relax
    }

    // Use this for initialization
    void Start () {
        gesturecontroler = this;

    }
	
	// Update is called once per frame
	void Update () {

    }


    //Gesture Control
    //isright : Judgment is left hand or right hand

    public void GestureControl(bool isright)
    {
        GForceDevice gforceDevice = gforce.GetComponent<GForceDevice>();
        if (gforceDevice.gesture != lastGesture)
        {
            lastGesture = gforceDevice.gesture;

            switch (lastGesture)
            {
                case Gesture.Relax:
                    // seems Relax is always coming fast, so setup a delay timer,
                    // only when Relax keeps for a while, then render relaxMaterial.
                    //GetComponent<Renderer>().material = relaxMaterial;
                    relaxStart = DateTime.Now;
                    GestureAnimationControl(isright, (int)Gesturex.Relax);  
                    break;
                case Gesture.Fist:
                    GestureAnimationControl(isright, (int)Gesturex.Fist);
                    break;
                case Gesture.SpreadFingers:
                    GestureAnimationControl(isright, (int)Gesturex.Spread);
                    break;
                case Gesture.WaveIn:
                    GestureAnimationControl(isright, (int)Gesturex.WaveIn);
                    break;
                case Gesture.WaveOut:
                    GestureAnimationControl(isright, (int)Gesturex.WaveOut);
                    break;
                case Gesture.Pinch:
                    GestureAnimationControl(isright, (int)Gesturex.Pinch);
                    break;
                case Gesture.Shoot:
                    GestureAnimationControl(isright, (int)Gesturex.Shoot);
                    break;
                case Gesture.Undefined:
                    GestureAnimationControl(isright, (int)Gesturex.Relax);
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (Gesture.Relax == lastGesture)
            {
                TimeSpan ts = DateTime.Now - relaxStart;
                if (ts.Milliseconds > RelaxDelay)
                    GestureAnimationControl(isright, (int)Gesturex.Relax);
            }
        }
    }


    //Gesture animation control
    //isright : Judgment is left hand or right hand
    //GestureNum : The serial number of the gesture received

    public void GestureAnimationControl(bool isright,int GestureNum)
    {

        if (isright)
        {
            //Right Arm
            AnimationRight.SetInteger("RightGesture", GestureNum); //Gesture Control
            GameObject.Find("GameCanvas/Image/Text").GetComponent<Text>().text = GesturesName[GestureNum]; //Show Gesture Name
        }
        else
        {
            //Left Arm
            AnimationLeft.SetInteger("LeftGesture", GestureNum); //Gesture Control
            GameObject.Find("GameCanvas/Image/Text").GetComponent<Text>().text = GesturesName[GestureNum]; //Show Gesture Name
        }

        Debug.LogFormat("GestureNum:{0}", GestureNum);
    }


    //Control arm rotation
    //isright : Judgment is left hand or right hand

    public void ArmQuaterControl(bool isright)
    {
        GForceDevice gforceDevice = gforce.GetComponent<GForceDevice>(); //Get gforce data
        quater = gforceDevice.quater;        //Get Quater

        if (isright)
        {
            //Right Arm
            Quater = new Quaternion(-quater.z, quater.x, -quater.y, quater.w); //Correct rotation direction
            Hand_right.transform.localRotation = Quater;
        }
        else
        {
            //Left Arm
            Quater = new Quaternion(quater.z, -quater.x, -quater.y, quater.w); //Correct rotation direction
            Hand_left.transform.rotation = Quater;
        }
    }
}
