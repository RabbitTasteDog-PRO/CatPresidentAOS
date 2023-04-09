using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JHDataReader : MonoBehaviour
{
    public struct STDataReadInfo
    {
        public string dataPath;
        public Action<string> ACTION_READ_LINE;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ReadTextData(string dataName, Action<string> _ACTION_READ_LINE)
    {
        // Debug.Log("############################");
        TextAsset data = Resources.Load(string.Format("Data/TextData/{0}", dataName), typeof(TextAsset)) as TextAsset;
        // Debug.Log("############################ : " + data.text);
        string decryptData = data.text;

        if (string.IsNullOrEmpty(decryptData))
        {
            return;
        }

        using (StringReader sr = new StringReader(decryptData))
        {
            string readLine;

            while ((readLine = sr.ReadLine()) != null)
            {
                if (!readLine.StartsWith("#") && !readLine.StartsWith("\t"))
                {
                    _ACTION_READ_LINE(readLine);
                }
            }
        }
    }





    public IEnumerator IE_ReadData(Action _action)
    {
        _action();
        yield return YieldHelper.waitForEndOfFrame();
    }
}