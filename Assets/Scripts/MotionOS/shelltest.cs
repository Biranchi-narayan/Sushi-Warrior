using UnityEngine;
using System.Collections;


using System.Diagnostics;



public class shelltest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	static Process shellp(string filename, string arguments) 
	{
    var p = new Process();
    p.StartInfo.Arguments = arguments;
    p.StartInfo.CreateNoWindow = true;
    p.StartInfo.UseShellExecute = false;
    p.StartInfo.RedirectStandardOutput = true;
    p.StartInfo.RedirectStandardInput = true;
    p.StartInfo.RedirectStandardError = true;
    p.StartInfo.FileName = filename;
    p.Start();
    return p;
	}

static string shell(string filename, string arguments) 
	{
    var p = shellp(filename, arguments);
    var output = p.StandardOutput.ReadToEnd();
    p.WaitForExit();
    return output;	
}

	// Update is called once per frame
	void Update () {
	
	}
	public string filename;
	void OnGUI()
	{
		filename = GUI.TextField(new Rect (10,10,150,20),filename);
		if (GUI.Button (new Rect (160,10,20,20), "OK")) {
		shelltest.shell(filename,"");
		}
		
	}
}
