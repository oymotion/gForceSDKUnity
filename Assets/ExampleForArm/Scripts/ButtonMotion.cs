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
using UnityEngine;

public class ButtonMotion : MonoBehaviour {

    [HideInInspector]
    public bool lightButton = false;
    bool doingDownMotion=false;

    Vector3 orgPosition;
    Vector3 aimPosition;

    float motionSpeed = 0.006f;

    public Material redMat;
    public Material greenMat;
    public Material normalMat;


	// Use this for initialization
	void Start () {
        orgPosition = this.gameObject.transform.localPosition;
        aimPosition = orgPosition - new Vector3(0,0.06f, 0);
    }
	
	// Update is called once per frame
	void Update () {
    }


    //Press the button by hand
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="hand")
        {
            StartCoroutine(DownMotion());
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        
    }


    //Change the button to green
    public void Lighting()
    {
        lightButton = true;
        gameObject.GetComponent<MeshRenderer>().material = greenMat;
    }


    //When the button is Down
    IEnumerator DownMotion()
    {
        if(!doingDownMotion)
        {

            //It's not the green button
            if (!lightButton)
            {
                gameObject.GetComponent<MeshRenderer>().material = redMat;  //Press the wrong button to show red
                PointsAndTimes.instance.error += 100;                       //Error score
            }


            //Press the button to follow the hand action
            doingDownMotion = true;

            while (this.gameObject.transform.localPosition.y >= 0.02f)
            {
                this.gameObject.transform.localPosition += new Vector3(0, -motionSpeed, 0);
                yield return 0;
            }
            this.gameObject.transform.localPosition = aimPosition;

            while (this.gameObject.transform.localPosition.y <= 0.08f)
            {
                this.gameObject.transform.localPosition += new Vector3(0, motionSpeed, 0);
                yield return 0;
            }

            this.gameObject.transform.localPosition = orgPosition;
            doingDownMotion = false;

            //Press the button to restore the normal color
            gameObject.GetComponent<MeshRenderer>().material = normalMat;

            //Press the correct button
            if (lightButton)
            {
                lightButton = false;
                PointsAndTimes.instance.score += 100;     //Increase correct scoring
                ArmGameControl.instance.ChangeLightButton(); //Change the green button
            }
        }

    }
}
