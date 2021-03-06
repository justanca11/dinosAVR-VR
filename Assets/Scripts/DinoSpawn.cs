﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DinoSpawn : MonoBehaviour
{

    private GameObject dino;

    private int dinoOption = 2;
    private int counter = 0;

    private float participantNr;
    public bool switchInput = false;

    public GameObject[] spawnPositions;

    [SerializeField]
    private GameObject realDinoPrefab; // 0
    [SerializeField]
    private GameObject unrealDinoPrefab; // 1

    // Start is called before the first frame update
    void Start()
    {
        participantNr = PlayerPrefs.GetFloat("UserNumber", 0.0f);
        WriteString("New User", participantNr);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Dinosaur") == null && counter < 2)
        {
            //there was no previous dino
            if(dinoOption == 2)
            {
                dinoOption = Random.Range(0, 2); // returns 0 or 1
                SpawnRandom();
            }
            //previous dino was real
            else if(dinoOption == 0)
            {
                dinoOption = 1;
                SpawnRandom();
            }
            //previous dino was unreal
            else if(dinoOption == 1)
            {
                dinoOption = 0;
                SpawnRandom();
            }
            
        }
        
    }

    private void SpawnRandom()
    {
        counter++;
        int spawnPos = Random.Range(0, spawnPositions.Length);

        if (dinoOption == 0)
        {
            // Debug.Log("Spawning real");
            WriteString("Real dinosaur", 0.0f);
            dino = (GameObject)Instantiate(realDinoPrefab, spawnPositions[spawnPos].transform.position, transform.rotation);
        }
        else
        {
            // Debug.Log("Spawning unreal");
            WriteString("Unreal dinosaur", 0.0f);
            dino = (GameObject)Instantiate(unrealDinoPrefab, spawnPositions[spawnPos].transform.position, transform.rotation);
        }
    }

    //Prinf function to log action and time
    static void WriteString(string action, float timeOfAction)
    {
        string path = "Assets/Resources/log.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(action + " " + timeOfAction);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load<TextAsset>("log");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

    public void OnDestroy()
    {
        print("hello");
        participantNr += 1.0f;
        PlayerPrefs.SetFloat("UserNumber", participantNr);
    }
}
