using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ReaderManager : MonoBehaviour 
{
    public static ReaderManager instance;

    public string fileName;
    public string fullText;

    public List<string> words = new List<string>();

    public bool execute;

	// Use this for initialization
    void Awake ()
    {
        instance = this;
    }
        
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (execute)
        {
            execute = false;

            words.Clear();

            //read from Resources folder
            var textGet = Resources.Load(fileName) as TextAsset;
            fullText = textGet.text;
    
            //remove return and new line
            //fullText = fullText.Replace("\r"," ").Replace("\n"," ");
    
            //split the string
            string[] wordsArray = fullText.Split("\n"[0]);
    
            //copy that into a list of words
            for (int i = 0; i < wordsArray.Length; i++)
            {
                /*//trim it
                wordsArray[i] = wordsArray[i].Trim();
    
                //if this is empty string
                if (wordsArray[i] != "")
                {
                    words.Add(wordsArray[i]);
                }*/
    
                words.Add(wordsArray[i]);
            }
        }
	}
}
