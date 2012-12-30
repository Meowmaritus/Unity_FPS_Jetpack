using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;

public class StringToFile : MonoBehaviour {
	private string path;
	public string folderName = "";
	public string fileName = "MyTXT.txt";
	public bool flush = false;
	private string guiData = "";
   // Use this for initialization
   //~ void Start () {
      //~ fileName = "/MyTXT.txt";   
   //~ }
   
   // Update is called once per frame
    void Update () {
	    if (flush){
			if(folderName != "")
				folderName+="/";
			path = Application.dataPath + "/" + folderName + fileName;
		    //~ Debug.Log(path);
			using (FileStream fs = File.Create(path)){
			AddText(fs, guiData);
			flush = !flush;
			}
		}
	}
	
	void SetData(string d){
		guiData = d;
		//~ Debug.Log(d);
	}


	private static void AddText(FileStream fs, string value) 
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
}