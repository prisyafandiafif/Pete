using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class FileIOUtil 
{

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    /// <summary>
    /// Reads the string from file.
    /// </summary>
    public static string ReadStringFromFile(string fileName)
    {
        if(!File.Exists(fileName))
        {
            Debug.Log("WARNING: Attempted to read from file \"" + fileName 
                + "\", file does not exist!");
            return null;    
        }

        StreamReader sr = new StreamReader(fileName);

        string output = sr.ReadToEnd();

        sr.Close();

        return output;
    } 
}
