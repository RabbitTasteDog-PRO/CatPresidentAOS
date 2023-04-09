using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StandAloneDemo : MonoBehaviour
{
    void Start()
    {
        StandaloneReaderDemo();
        StandaloneUrlDemo();
        //WritingDemo(); //Don't run this in a webplayer due to "GetTempPath()" not working properly there (probably security issue).
    }

    #region Standalone Reader Demo

    void StandaloneReaderDemo()
    {
        /***********************************************************************************/
        /* This is a demonstration of the usage of standalone XML parsing.                 */
        /* The XmlParser does not need to be attached to a GameObject or anything,         */
        /* just keep it somewhere in your Assets folder.                                   */
        /*                                                                                 */
        /* It works for any class type with a public empty constructor.                    */
        /*                                                                                 */
        /* It can be called with either 1, 2 or 3 parameters:                              */
        /*                                                                                 */
        /* XmlParser.Read<T>(string filename);                                             */
        /* XmlParser.Read<T>(string filename, bool caseSensitive);                         */
        /* XmlParser.Read<T>(string filename, bool caseSensitive, bool showDebugLogging);  */
        /*                                                                                 */
        /* Parameter <T> is the type you want to parse from the XML.                       */
        /*                                                                                 */
        /* Default caseSensitive = false and showDebugLogging = false.                     */
        /*                                                                                 */
        /* Case sensitivity is relevant when you have multiple fields or properties with   */
        /* the same name but with different case (not a good idea anyway).                 */
        /*                                                                                 */
        /* As you can see, very easy to use!                                               */
        /* Enjoy :)                                                                        */
        /***********************************************************************************/

        List<MyObject> myObjects = XmlParser.Read<MyObject>("StandAloneDemo");

        //Show the output of this demo
        foreach (MyObject myObject in myObjects)
        {
            GameObject.Find("GUI Text").GetComponent<GUIText>().text += "\nXmlParser StandAlone Demo: MyObject: Test (" + myObject.Test + "), Name (" + myObject.Name + ")";
        }
    }

    #endregion

    #region Standalone Url Demo

    void StandaloneUrlDemo()
    {
        /**************************************************************************************/
        /* This shows how to download and process and xml file from a URL.                    */
        /*                                                                                    */
        /* The download is asynchronous so it won't block your main thread.                   */
        /*                                                                                    */
        /* Because of the asynchronous download, you should provide the method that actually  */
        /* processes the gameobjects as an input parameter ("ShowMyDownloadedObjects").       */
        /*                                                                                    */
        /* Enjoy :)                                                                           */
        /**************************************************************************************/
        
        StartCoroutine(XmlParser.ReadFromUrl<MyObject>("http://www.stinkyrhino.com/unity/xml_parser_demo/StandaloneUrlDemo.xml", ShowMyDownloadedObjects));
    }

    void ShowMyDownloadedObjects(List<MyObject> myObjects)
    {
        //Show the output of the downloaded data
        foreach (MyObject myObject in myObjects)
        {
            GameObject.Find("GUI Text").GetComponent<GUIText>().text += "\nXmlParser StandAlone URL Demo: MyObject: Test (" + myObject.Test + "), Name (" + myObject.Name + ")";
        }
    }

    #endregion

    #region Writing Demo

    void WritingDemo()
    {
        /*********************************************************************************************************/
        /* The following part shows XML writing.                                                                 */
        /*                                                                                                       */
        /* It can be called with either 1, 2 or 3 parameters:                                                    */
        /*                                                                                                       */
        /* XmlParser.Write(objects, string filename, bool overwrite);                                            */
        /* XmlParser.Write(objects, string filename, bool overwrite, bool onlyPublic);                           */
        /* XmlParser.Write(objects, string filename, bool overwrite, bool onlyPublic, bool showDebugLogging);    */
        /*                                                                                                       */
        /* Default onlyPublic = true and showDebugLogging = false.                                               */
        /*                                                                                                       */
        /* When overwrite is true, the file will be overwritten if it already exists. When overwrite is false    */
        /* and the file already exists, the data will be appended to the root xml node present. If the existing  */
        /* file is not an xml document, writing will do nothing and log an error.                                */
        /*                                                                                                       */
        /* When onlyPublic is true, only the public properties and fields of your object will be written.        */
        /* When onlyPublic is false, all other properties and fields (private, internal, protected) will be      */
        /* written as well.                                                                                      */
        /*                                                                                                       */
        /* Enjoy :)                                                                                              */
        /*********************************************************************************************************/

        List<MyObject> myObjects = new List<MyObject>();
        myObjects.Add(new MyObject(1, "first"));
        myObjects.Add(new MyObject(2, "second"));
        myObjects.Add(new MyObject(3, "third"));

        string filename = Path.GetTempPath() + "XmlParserDemoOutput.xml";

        XmlParser.Write(myObjects, filename, false);

        GameObject.Find("GUI Text").GetComponent<GUIText>().text += "\nData written to file: " + filename;
    }

    #endregion
}