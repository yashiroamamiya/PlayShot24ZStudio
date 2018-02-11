using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IllusionPlugin;
using UnityEngine;
using System.IO;

namespace ClassLibrary4
{
    public class Class1 : IEnhancedPlugin
    {
        private Action m_resultHandler;        // キャプチャーが完了したときに呼び出すハンドラー
        private Texture2D m_texture;              // テクスチャー
        private bool m_isSavedScreenCapture; // キャプチャー画像を保存済みか？
        ScreenShot sc;

        public void OnApplicationStart()
        {
            m_texture = null;
            m_isSavedScreenCapture = false;

                        GameObject gameObject = new GameObject("ScreenShot");
            sc = gameObject.AddComponent<ScreenShot>();
        }

        public string[] Filter
        {
            get
            {
//                string[] array = { "PlayHome64bit", "PlayHome32bit" };
                string[] array = { "PlayHomeStudio32bit", "PlayHomeStudio64bit", "PlayHome64bit", "PlayHome32bit" };
                return array;
            }
        }

        public string Name
        {
            get
            {
                return "PlayShot24ZHNeo";
            }
        }

        public string Version
        {
            get
            {
                return "2.1.2.0";
            }
        }

        public void OnApplicationQuit()
        {

        }

        public void OnFixedUpdate()
        {
        }

        public void OnLateUpdate()
        {
        }

        public void OnLevelWasInitialized(int level)
        {

        }

        public void OnLevelWasLoaded(int level)
        {
        }

        public void OnUpdate()
        {

            if (Input.GetKeyDown(KeyCode.F10))
            {
                try
                {
                    int size = 1;

                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        size = size * 2;
                    }
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        size = size * 2;
                    }
                    // キャプチャー
                    Take(size,false);

                }
                catch(Exception e)
                {
                    logSave(e.Message);
                }
            }
            
            else if (Input.GetKeyDown(KeyCode.F9))
            {
                try
                {
                    int size = 1;

                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    {
                        size = size * 2;
                    }
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        size = size * 2;
                    }
                    // キャプチャー
                    Take(size, true);
                }
                catch (Exception e)
                {
                    logSave(e.Message);
                }
            }
            
        }

        public void logSave(string txt)
        {
            /*
            StreamWriter sw;
            FileInfo fi;
            fi = new FileInfo(Application.dataPath + "/errorLog.txt");
            sw = fi.AppendText();
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();
            */
        }

        //-------------------------------------------------------------
        //! 画面をキャプチャーします.
        //-------------------------------------------------------------
        private void Take(int size, Boolean doTransparent)
        {
            logSave("take");
            if(doTransparent)
            {
                sc.myScreenShotTransparent(size);
            }
            else
            {
                sc.myScreenShot(size);
            }
            System.GC.Collect();
            /*
            m_isSavedScreenCapture = true;

            int cameraNum = Camera.allCamerasCount;


                m_texture = new Texture2D(Screen.width * size, Screen.height * size, TextureFormat.RGB24, false);
                //            Texture2D screenShot = new Texture2D(Screen.width*4, Screen.height*4, TextureFormat.RGB24, false);
                RenderTexture rt = new RenderTexture(m_texture.width, m_texture.height, 24);

            Camera cam = Camera.main.GetComponent<Camera>();


                RenderTexture prev = cam.targetTexture;
                cam.targetTexture = rt;
                cam.Render();
                cam.targetTexture = prev;


                RenderTexture.active = rt;
                m_texture.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
                m_texture.Apply();
                RenderTexture.active = null; // JC: added to avoid errors
            var bytes = m_texture.EncodeToPNG();
                
                // Encode texture into PNG
                String fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName, bytes);
                m_texture = null;
            Application.CaptureScreenshot(fileName + ".0");


            m_isSavedScreenCapture = false;

            GameObject bg = GameObject.Find("BGcanvas");

            Canvas canvas = bg.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
*/
        }
    }
}
