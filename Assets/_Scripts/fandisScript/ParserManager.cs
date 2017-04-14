using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParserManager : MonoBehaviour 
{
    public static ParserManager instance;

    public string fileName;
    public string fullText;

    public List<string> words = new List<string>();

	// Use this for initialization
    void Awake ()
    {
        instance = this;
    }
        
	void Start () 
    {
		//read from Resources folder
        var textGet = Resources.Load(fileName) as TextAsset;
        fullText = textGet.text;

        //remove return and new line
        fullText = fullText.Replace("\r"," ").Replace("\n"," ");

        //split the string
        string[] wordsArray = fullText.Split(" "[0]);

        //copy that into a list of words
        for (int i = 0; i < wordsArray.Length; i++)
        {
            //trim it
            wordsArray[i] = wordsArray[i].Trim();

            //if this is empty string
            if (wordsArray[i] != "")
            {
                words.Add(wordsArray[i]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
