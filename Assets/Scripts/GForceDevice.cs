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
﻿using UnityEngine;

using gf;

using Gesture = gf.Device.Gesture;

public class GForceDevice : MonoBehaviour
{
    public Gesture gesture = Gesture.Undefined;
    public Quaternion quater = new Quaternion();
    public byte[] EMGRawdata;
    public bool recentered = false; // handled and reset outside by client

    void Start()
    {
    }

    void Update()
    {
        lock (mLock)
        {
            transform.rotation = quater;
            gesture = internalGest;
            EMGRawdata = internalEMGRaw;
        }
    }

    public Device device
    {
        get { return mDevice; }
        set
        {
            mDevice = value;
        }
    }

    private Device mDevice;

    public void onOrientationData(float w, float x, float y, float z)
    {
        lock (mLock)
        {
            quater.w = w;
            quater.x = z;
            quater.y = -x;
            quater.z = -y;
        }
    }
    public void onGestureData(Device.Gesture gest)
    {
        Debug.LogFormat("onGestureData: {0} - {1}", mDevice, gest);

        lock (mLock)
        {
            internalGest = gest;
        }
    }
    public void onDeviceStatusChanged(Device.Status status)
    {
        Debug.LogFormat("onDeviceStatusChanged: {0}: {1}", mDevice, status);
        if (Device.Status.ReCenter == status)
        {
            lock (mLock)
            {
                recentered = true;
            }
        }
    }
    public void onExtendedDeviceData(Device.DataType type, byte[] data)
    {
        if (0 < data.Length)
            Debug.LogFormat("onExtendedDeviceData: {0}: {1}, length = {2}, data: first byte: {3}, last byte: {4}",
                mDevice, type, data.Length, data[0], data[data.Length - 1]);
        else
            Debug.LogFormat("onExtendedDeviceData: {0}: {1}, length = {2}", mDevice, type, data.Length);

        lock (mLock)
        {
            internalEMGRaw = data;
        }
    }

    private Object mLock = new Object();
    private Gesture internalGest = Gesture.Undefined;
    private byte[] internalEMGRaw;
}
