using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ScreenShot : MonoBehaviour
{

    protected bool m_isSavedScreenCapture = false;

    void Start()
    {



    }


    public void myScreenShot(int size)
    {
        if (m_isSavedScreenCapture)
            return;

        try
        {
            m_isSavedScreenCapture = true;
            logSave("my");
            //        StartCoroutine(TakeScreenShot(size));
            Texture2D m_texture = new Texture2D(Screen.width * size, Screen.height * size, TextureFormat.RGB24, false);
            //            Texture2D screenShot = new Texture2D(Screen.width*4, Screen.height*4, TextureFormat.RGB24, false);
            RenderTexture rt = new RenderTexture(m_texture.width, m_texture.height, 24);

            //        Camera cam = null;

            //        foreach (Camera cam in Camera.allCameras)
            //        {
            Camera cam = Camera.main;
            if (cam.isActiveAndEnabled && !(cam.targetTexture != null))
            {
                RenderTexture prev = cam.targetTexture;
                cam.targetTexture = rt;
                cam.Render();
                cam.targetTexture = prev;


                RenderTexture.active = rt;
                m_texture.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
                m_texture.Apply();
                RenderTexture.active = null; // JC: added to avoid errors

                //Color[] pixelsExt = m_texture.GetPixels();

                //var bytesS = m_texture.EncodeToPNG();


                var bytes2 = m_texture.EncodeToPNG();

                // Encode texture into PNG
                String fileName3 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName3, bytes2);

                m_isSavedScreenCapture = false;
                logSave("end");

            }
            else
            {
                foreach (Camera came in Camera.allCameras)
                {
                    if (came.isActiveAndEnabled && !(came.targetTexture != null))
                    {
                        RenderTexture prev = came.targetTexture;
                        came.targetTexture = rt;
                        came.Render();
                        came.targetTexture = prev;


                        RenderTexture.active = rt;
                        m_texture.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
                        m_texture.Apply();
                        RenderTexture.active = null; // JC: added to avoid errors

                        //Color[] pixelsExt = m_texture.GetPixels();

                        //var bytesS = m_texture.EncodeToPNG();


                        var bytes2 = m_texture.EncodeToPNG();

                        // Encode texture into PNG
                        String fileName3 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                        File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName3, bytes2);

                        m_isSavedScreenCapture = false;
                        logSave("end");


                        break;
                    }
                }
            }
            //        }

        }
        catch(Exception e)
        {
            logSave(e.ToString());
        }


    }

    public void myScreenShotTransparent(int size)
    {
        if (m_isSavedScreenCapture)
            return;

        m_isSavedScreenCapture = true;
        logSave("my");
        //        StartCoroutine(TakeScreenShot(size));
        Texture2D m_texture = new Texture2D(Screen.width * size, Screen.height * size, TextureFormat.RGB24, false);
        Texture2D m_textureBlack = new Texture2D(Screen.width * size, Screen.height * size, TextureFormat.RGB24, false);
        Texture2D textureTransparentBackground = new Texture2D(Screen.width * size, Screen.height * size, TextureFormat.ARGB32, false);
        //            Texture2D screenShot = new Texture2D(Screen.width*4, Screen.height*4, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(m_texture.width, m_texture.height, 24);
        RenderTexture rtBlack = new RenderTexture(m_texture.width, m_texture.height, 24);

        //        Camera cam = null;

//        foreach (Camera cam in Camera.allCameras)
//        {
            Camera cam = Camera.main;
            if (cam.isActiveAndEnabled && !(cam.targetTexture != null))
            {
                Color beforeColor = cam.backgroundColor;
                RenderTexture prev = cam.targetTexture;
                cam.backgroundColor = Color.white;
                cam.targetTexture = rt;
                cam.Render();
                cam.targetTexture = prev;


                RenderTexture.active = rt;
                m_texture.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
                m_texture.Apply();
                
                /*
                Color[] pixelsExt1 = m_texture.GetPixels();

                //var bytesS = m_texture.EncodeToPNG();
                

                var bytes21 = m_texture.EncodeToPNG();

                // Encode texture into PNG
                String fileName31 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName31, bytes21);
                */
                

                cam.backgroundColor = Color.black;
                cam.targetTexture = rtBlack;
                cam.Render();
                cam.targetTexture = prev;
            
                RenderTexture.active = rtBlack;
                m_textureBlack.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
                m_textureBlack.Apply();

                RenderTexture.active = null; // JC: added to avoid errors

                Color color;
                for (int y = 0; y < textureTransparentBackground.height; y++)
                {
                    // each row
                    for (int x = 0; x < textureTransparentBackground.width; ++x)
                    {
                        // each column
                        float alpha = m_texture.GetPixel(x, y).r - m_textureBlack.GetPixel(x, y).r;
                        alpha = 1.0f - alpha;
                        if (alpha == 0)
                        {
                            color = Color.clear;
                        }
                        else
                        {
                            color = m_textureBlack.GetPixel(x, y) / alpha;
                        }
                        color.a = alpha;
                        textureTransparentBackground.SetPixel(x, y, color);
                    }
                }

                //Color[] pixelsExt = textureTransparentBackground.GetPixels();

                //var bytesS = m_texture.EncodeToPNG();



                var bytes2 = textureTransparentBackground.EncodeToPNG();

                // Encode texture into PNG
                String fileName3 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName3, bytes2);

                m_isSavedScreenCapture = false;
                logSave("end");
                cam.backgroundColor = beforeColor;

            }
            else
            {
            foreach (Camera came in Camera.allCameras)
            {
                if (came.isActiveAndEnabled && !(came.targetTexture != null))
                {
                    Color beforeColor = came.backgroundColor;
                    RenderTexture prev = came.targetTexture;
                    came.backgroundColor = Color.white;
                    came.targetTexture = rt;
                    came.Render();
                    came.targetTexture = prev;


                    RenderTexture.active = rt;
                    m_texture.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
                    m_texture.Apply();
                    /*
                    Color[] pixelsExt1 = m_texture.GetPixels();

                    //var bytesS = m_texture.EncodeToPNG();


                    var bytes21 = m_texture.EncodeToPNG();

                    // Encode texture into PNG
                    String fileName31 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                    File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName31, bytes21);

                    */




                    came.backgroundColor = Color.black;
                    came.targetTexture = rtBlack;
                    came.Render();
                    came.targetTexture = prev;


                    RenderTexture.active = rtBlack;
                    m_textureBlack.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
                    m_textureBlack.Apply();

                    RenderTexture.active = null; // JC: added to avoid errors

                    Color color;
                    for (int y = 0; y < textureTransparentBackground.height; y++)
                    {
                        // each row
                        for (int x = 0; x < textureTransparentBackground.width; ++x)
                        {
                            // each column
                            float alpha = m_texture.GetPixel(x, y).r - m_textureBlack.GetPixel(x, y).r;
                            alpha = 1.0f - alpha;
                            if (alpha == 0)
                            {
                                color = Color.clear;
                            }
                            else
                            {
                                color = m_textureBlack.GetPixel(x, y) / alpha;
                            }
                            color.a = alpha;
                            textureTransparentBackground.SetPixel(x, y, color);
                        }
                    }

                    //Color[] pixelsExt = textureTransparentBackground.GetPixels();

                    //var bytesS = m_texture.EncodeToPNG();



                    var bytes2 = textureTransparentBackground.EncodeToPNG();

                    // Encode texture into PNG
                    String fileName3 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                    File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName3, bytes2);

                    m_isSavedScreenCapture = false;
                    logSave("end");
                    came.backgroundColor = beforeColor;

                    break;
                }
            }

        }
        //       }


    }

    /*
        IEnumerator TakeScreenShot(int size)
        {

            yield return new WaitForEndOfFrame();


            var width = Screen.width * 1;
            var height = Screen.height * 1;
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);


            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            var bytes = tex.EncodeToPNG();

            // Encode texture into PNG
            String fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")+ ".png";
            File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName, bytes);

        }
    */

    IEnumerator TakeScreenShot(int size)
    {
        /*
                GameObject bg = GameObject.Find("BGcanvas");
                Canvas canvas=null;
                RenderMode before= RenderMode.ScreenSpaceCamera;

                if ( bg != null )
                {
                    canvas = bg.GetComponent<Canvas>();
                    if( canvas != null)
                    {
                        before = canvas.renderMode;
                        canvas.renderMode = RenderMode.WorldSpace;
                    }
                }
        */
        logSave("きたよ");
        yield return new WaitForEndOfFrame();

        Texture2D m_texture = new Texture2D(Screen.width * size, Screen.height * size, TextureFormat.RGB24, false);
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

        Color[] pixelsExt = m_texture.GetPixels();

        //var bytesS = m_texture.EncodeToPNG();


        var bytes2 = m_texture.EncodeToPNG();

        // Encode texture into PNG
        String fileName3 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
        File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName3, bytes2);
        /*
        if (canvas != null)
        {
            canvas.renderMode = before;
        }
        */
        /*
                try
                {
                    // add
                    Color[] change_pixels = new Color[pixelsExt.Length];
                    saveLog("before" + pixelsNormal.Length + " " + pixelsExt.Length);
                    for (int i = 0; i < m_texture.height; i++)
                    {
                        for (int j = 0; j < m_texture.width; j++)
                        {
                            Color pixel = pixelsExt[i * m_texture.width + j];
                            if (pixel.r + pixel.g + pixel.b < 0.1f )
                            {
                                change_pixels.SetValue(pixelsNormal[i / size * m_texture.width/size + j / size], i* m_texture.width+j);
                            }
                            else
                            {
                                change_pixels.SetValue(pixel, i * m_texture.width + j);
                            }
                        }
                    }

                    saveLog("ot");
                    Texture2D outputTexture = new Texture2D(Screen.width * size, Screen.height * size, TextureFormat.RGB24, false);
                    outputTexture.filterMode = FilterMode.Point;
                    outputTexture.SetPixels(change_pixels);
                    outputTexture.Apply();

                    saveLog("enc");
                    var bytes = outputTexture.EncodeToPNG();

                    saveLog("file");

                    // Encode texture into PNG
                    String fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
                    File.WriteAllBytes(Application.dataPath + "/../UserData/cap/" + fileName, bytes);
                }
                catch(Exception e)
                {
                    saveLog(e.ToString());
                }
                */
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

    public void saveLog(String text)
    {
        /*
        StreamWriter sw = new System.IO.StreamWriter(
    Application.dataPath + "/../UserData/cap/log.txt",
    true,
    System.Text.Encoding.Default);
        //TextBox1.Textの内容を書き込む
        sw.WriteLine(text);
        //閉じる
        sw.Close();
        */
    }
}