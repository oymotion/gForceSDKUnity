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
﻿using System;
using System.Text;

namespace gf
{

    public class Device
    {
        public const int MAX_DEVICE_STR_LEN = 64;

        public enum Gesture
        {
            Relax = 0x00,
            Fist = 0x01,
            SpreadFingers = 0x02,
            WaveIn = 0x03,
            WaveOut = 0x04,
            Pinch = 0x05,
            Shoot = 0x06,
            Undefined = 0xFF
        };

        public enum Status
        {
            None = 0,
            ReCenter = 1,
            UsbPlugged = 2,
            UsbPulled = 3,
            Motionless = 4,
        };

        public enum ConnectionStatus
        {
            Disconnected,
            Disconnecting,
            Connecting,
            Connected
        };

        public enum DataType
        {
            Invalid,
            Accelerate,
            Gyroscope,
            Magnetometer,
            Eulerangle,
            Quaternion,
            Rotationmatrix,
            Gesture,
            Emgraw,
            Hidmouse,
            Hidjoystick,
            Devicestatus,
        };

        public Device(IntPtr handle)
        {
            hD = handle;
        }

        public uint getAddrType()
        {
            return libgforce.device_get_addr_type(hD);
        }

        public RetCode getAddress(out string address)
        {
            StringBuilder str = new StringBuilder(MAX_DEVICE_STR_LEN);
            RetCode ret = libgforce.device_get_address(hD, str, (uint)str.Capacity);
            if (RetCode.GF_SUCCESS == ret)
            {
                address = str.ToString();
            }
            else
            {
                address = "";
            }
            return ret;
        }

        public string getAddress()
        {
            string address;
            getAddress(out address);
            return address;
        }

        public RetCode getName(out string name)
        {
            StringBuilder str = new StringBuilder(MAX_DEVICE_STR_LEN);
            RetCode ret = libgforce.device_get_name(hD, str, (uint)str.Capacity);
            if (RetCode.GF_SUCCESS == ret)
            {
                name = str.ToString();
            }
            else
            {
                name = "E" + ret;
            }
            return ret;
        }

        public string getName()
        {
            string name;
            getName(out name);
            return name;
        }

        public uint getRssi()
        {
            return libgforce.device_get_rssi(hD);
        }

        public ConnectionStatus getConnectionStatus()
        {
            return libgforce.device_get_connection_status(hD);
        }

        public RetCode connect()
        {
            return libgforce.device_connect(hD);
        }

        public RetCode disconnect()
        {
            return libgforce.device_disconnect(hD);
        }

        public RetCode setNotification(uint notifSwitch)
        {
            return libgforce.device_set_notification(hD, notifSwitch);
        }

        private IntPtr hD;
    }
}
