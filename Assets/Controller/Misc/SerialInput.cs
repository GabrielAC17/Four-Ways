using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using UnityEngine;
using System.Threading;

namespace Assets.Controller.Misc
{
    class SerialInput : MonoBehaviour
    {
        private static SerialPort serial;
        private static int step = 0;

        private static int startButton;
        private static int dirUpButton;
        private static int dirDownButton;
        private static int dirLeftButton;
        private static int dirRightButton;
        private static int actionButton;
        private static int action2Button;
        private static int cUpButton;
        private static int cDownButton;
        private static int cLeftButton;
        private static int cRightButton;
        private static float analogX;
        private static float analogY;

        Thread readThread;

        private void Start()
        {
            RunSerial("COM7");
        }

        public void RunSerial(string port)
        {
            try
            {
                readThread = new Thread(readData);
                serial = new SerialPort(port, 115200, Parity.None, 8, StopBits.One);
                serial.Open();
                readThread.Start();
            }
            catch(Exception e)
            {
                Debug.Log("No serial controller connected!");
            }
            
        }

        private void readData()
        {
            // Show all the incoming data in the port's buffer
            while (true)
            {
                int value = serial.ReadChar();
                if (value > 1 && step == 0)
                {
                    step++;
                }
                else if (value <= 1)
                {
                    startButton = value;
                }
                switch (step)
                {
                    case 1:
                        startButton = ((value >> 2) & 1);
                        dirUpButton = ((value >> 3) & 1);
                        dirDownButton = ((value >> 4) & 1);
                        dirLeftButton = ((value >> 5) & 1);
                        dirRightButton = ((value >> 6) & 1);
                        step++;
                        break;
                    case 2:
                        actionButton = ((value >> 2) & 1);
                        action2Button = ((value >> 3) & 1);
                        cUpButton = ((value >> 4) & 1);
                        cDownButton = ((value >> 5) & 1);
                        cLeftButton = ((value >> 6) & 1);
                        cRightButton = ((value >> 7) & 1);
                        step++;
                        break;
                    case 3:
                       if (value > 150 || value < 100)
                        {
                            analogX = MapInterval(value);
                        }
                        step++;
                        break;
                    case 4:
                        if (value > 150 || value < 100)
                        {
                            analogY = MapInterval(value);
                        }
                        step = 0;
                        break;
                }
            }
        }

        private static float MapInterval(float val)
        {
            if (val >= 255) return 1;
            if (val <= 0) return -1;
            return -1 + (val - 0) / (255 - 0) * (1 - -1);
        }

        private void OnApplicationQuit()
        {
            this.readThread.Interrupt();
            serial.Close();
            GameObject.Destroy(gameObject);
        }

        public static int StartButton
        {
            get
            {
                return startButton;
            }

            set
            {
                startButton = value;
            }
        }

        public static int DirUpButton
        {
            get
            {
                return dirUpButton;
            }

            set
            {
                dirUpButton = value;
            }
        }

        public static int DirDownButton
        {
            get
            {
                return dirDownButton;
            }

            set
            {
                dirDownButton = value;
            }
        }

        public static int DirLeftButton
        {
            get
            {
                return dirLeftButton;
            }

            set
            {
                dirLeftButton = value;
            }
        }

        public static int DirRightButton
        {
            get
            {
                return dirRightButton;
            }

            set
            {
                dirRightButton = value;
            }
        }

        public static int ActionButton
        {
            get
            {
                return actionButton;
            }

            set
            {
                actionButton = value;
            }
        }

        public static int Action2Button
        {
            get
            {
                return action2Button;
            }

            set
            {
                action2Button = value;
            }
        }

        public static int CUpButton
        {
            get
            {
                return cUpButton;
            }

            set
            {
                cUpButton = value;
            }
        }

        public static int CDownButton
        {
            get
            {
                return cDownButton;
            }

            set
            {
                cDownButton = value;
            }
        }

        public static int CLeftButton
        {
            get
            {
                return cLeftButton;
            }

            set
            {
                cLeftButton = value;
            }
        }

        public static int CRightButton
        {
            get
            {
                return cRightButton;
            }

            set
            {
                cRightButton = value;
            }
        }

        public static float AnalogX
        {
            get
            {
                return analogX;
            }

            set
            {
                analogX = value;
            }
        }

        public static float AnalogY
        {
            get
            {
                return analogY;
            }

            set
            {
                analogY = value;
            }
        }
    }
}
