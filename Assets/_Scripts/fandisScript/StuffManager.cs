using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class StuffManager : MonoBehaviour 
{
    public static StuffManager instance;

    public ReaderManager readerManager;

    public GameObject cube; //to store the gameobject of cube that will be instantiated

    public int currentCubeIdx; //which idx of the cube we are currently at

    public bool execute;

    private int initIdxFrontText = 0;
    private int initIdxRightText = 1;
    private int initIdxBackText = 2;
    private int initIdxLeftText = 3;
    private int initIdxBoxSize = 4;
    private int initIdxSpaceSize = 5;
    private int initIdxNewCube = 6;

    private int idxSpaceBetweenCubes = 7;

    //private bool wordsFilled;

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
        /*if (ReaderManager.instance.words.Count > 0)
        {
            if (!wordsFilled)
            {
                wordsFilled = true;
                GenerateCubes();
            } 
        }*/
        
        if (execute)
        {
            execute = false;
            GenerateCubes();
        }
	}   

    // Instantiate cubes over and over again based on what we have in text files
    public void GenerateCubes ()
    {
        currentCubeIdx = 0;

        /*int forDestroyIdx = 1;
        for (int i = 0; i < cube.transform.parent.gameObject.transform.childCount; i++)
        {
            DestroyImmediate(cube.transform.parent.gameObject.transform.GetChild(forDestroyIdx).gameObject);
            //cube.transform.parent.gameObject.transform.GetChild(forDestroyIdx).gameObject.transform.parent = null;
        }*/

        foreach (Transform child in cube.transform.parent.gameObject.transform) 
        {
            if (child.name != "Cube")
            {
                child.gameObject.SetActive(false);
            }
        }

        GameObject newCube = Instantiate(cube);
        newCube.transform.parent = cube.transform.parent;
        newCube.transform.position = cube.transform.position;
        newCube.transform.localScale = cube.transform.localScale;
        newCube.name = "Cube " + currentCubeIdx; 
        newCube.SetActive(true);

        float scale = 0f;
        float space = 0f;

        for (int i = 0; i < readerManager.words.Count; i++)
        {
            Debug.Log("Word Get!");

            //get the word
            string word = readerManager.words[i];

            //if the front text
            if (i == initIdxFrontText + (currentCubeIdx * idxSpaceBetweenCubes))
            {
                Text frontText = newCube.transform.FindChild("Front").gameObject.transform.FindChild("Canvas").gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>();

                //Debug.Log("Text Get!");

                //set the front text
                if (word == "<null>")
                {
                    //set nothing
                }
                else
                {
                    //set with the text
                    frontText.text = word;
                }
            }
            else
            //if the right text
            if (i == initIdxRightText + (currentCubeIdx * idxSpaceBetweenCubes))
            {
                Text rightText = newCube.transform.FindChild("Right").gameObject.transform.FindChild("Canvas").gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>();

                //Debug.Log("Text Get!");

                //set the right text
                if (word == "<null>")
                {
                    //set nothing
                }
                else
                {
                    //set with the text
                    rightText.text = word;
                }
            }
            else
            //if the back text
            if (i == initIdxBackText + (currentCubeIdx * idxSpaceBetweenCubes))
            {
                Text backText = newCube.transform.FindChild("Back").gameObject.transform.FindChild("Canvas").gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>();

                //Debug.Log("Text Get!");

                //set the back text
                if (word == "<null>")
                {
                    //set nothing
                }
                else
                {
                    //set with the text
                    backText.text = word;
                }
            }
            else
            //if the left text
            if (i == initIdxLeftText + (currentCubeIdx * idxSpaceBetweenCubes))
            {
                Text leftText = newCube.transform.FindChild("Left").gameObject.transform.FindChild("Canvas").gameObject.transform.FindChild("Text").gameObject.GetComponent<Text>();

                //Debug.Log("Text Get!");

                //set the left text
                if (word == "<null>")
                {
                    //set nothing
                }
                else
                {
                    //set with the text
                    leftText.text = word;
                }
            }
            else
            //if it's about the box size
            if (i == initIdxBoxSize + (currentCubeIdx * idxSpaceBetweenCubes))
            {
                scale = float.Parse(word);

                //Debug.Log("Text Get!");

                //set the box size
                newCube.transform.localScale = new Vector3( 
                newCube.transform.localScale.x,
                scale,
                newCube.transform.localScale.z);

                for (int j = 1; j < newCube.transform.childCount; j++)
                {
                    //Transform initParent = newCube.transform;
                    //int initSiblingIdx = newCube.transform.GetChild(j).GetSiblingIndex();
                    //newCube.transform.GetChild(j).parent = null;
                    float newScale = 1f / scale * newCube.transform.GetChild(j).transform.FindChild("Canvas").GetComponent<RectTransform>().localScale.x;

                    newCube.transform.GetChild(j).transform.FindChild("Canvas").GetComponent<RectTransform>().localScale = new Vector3(
                    newCube.transform.GetChild(j).transform.FindChild("Canvas").GetComponent<RectTransform>().localScale.x,
                    newScale,
                    newCube.transform.GetChild(j).transform.FindChild("Canvas").GetComponent<RectTransform>().localScale.z);
                    //newCube.transform.GetChild(j).parent = initParent;
                    //newCube.transform.GetChild(j).SetSiblingIndex(initSiblingIdx);
                }
            }
            else
            //if it's about the space
            if (i == initIdxSpaceSize + (currentCubeIdx * idxSpaceBetweenCubes))
            {
                space = float.Parse(word);

                //Debug.Log("Text Get!");

                //get the correct position
                float yPosition = newCube.transform.position.y - scale;

                currentCubeIdx += 1;

                //generate new cube
                newCube = Instantiate(cube);
                newCube.transform.parent = cube.transform.parent;
                newCube.transform.position = new Vector3(
                cube.transform.position.x,
                yPosition - space,
                cube.transform.position.z);
                newCube.transform.localScale = cube.transform.localScale;
                newCube.name = "Cube " + currentCubeIdx; 
                newCube.SetActive(true);
            }
        }
    }
}
