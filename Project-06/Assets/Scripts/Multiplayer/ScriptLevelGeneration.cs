using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ScriptLevelGeneration : NetworkBehaviour
{

    [Header("Level Settings")]
    public int levelLength = 4;
    public int levelWidth = 4;

    [Header("Prefab Objects")]
    public GameObject floor;
    public GameObject block;

    [Header("Random Placement Options")]
    public GameObject theRandomOne;
    public int howManyObjects = 4;
    public float heightLimit = 3;
    int levelBlockSize = 5;


    public List<GameObject> level;

    [SyncVar]
    string testLevel = "";

    bool levelGenerated = false;

    //List<GameObject> floorObjects = new List<GameObject>();
    //List<GameObject> wallObjects = new List<GameObject>();
    //List<GameObject> randomObjects = new List<GameObject>();

    public void GenerateLevel()
    {
        level = new List<GameObject>();
        //generate floor
        for (int i = 0; i < levelWidth; i++)
        {
            for (int k = 0; k < levelLength; k++)
            {
                GameObject tempFloor =
                    (GameObject)Instantiate(
                        floor,
                        new Vector3(
                            transform.position.x + i * levelBlockSize,
                            transform.position.y,
                            transform.position.z + k * levelBlockSize
                            ),
                        Quaternion.identity
                        );
                tempFloor.transform.parent = transform;

                level.Add(tempFloor);


                //adds boundry walls
                if (i == 0 || i == (levelWidth - 1) || k == 0 || k == (levelLength - 1))
                {
                    GameObject tempWall =
                        (GameObject)Instantiate(
                            block,
                            new Vector3(
                                transform.position.x + i * levelBlockSize,
                                transform.position.y + 3,
                                transform.position.z + k * levelBlockSize
                            ),
                            Quaternion.identity
                        );

                    tempWall.transform.parent = transform;
                    //wallObjects.Add(tempWall);
                    level.Add(tempWall);
                }
            }

        }

        //place random objects
        for (int i = 0; i < howManyObjects; i++)
        {
            float tempX = Random.Range(1, (levelWidth - 1));
            float tempY = Random.Range(1, heightLimit);
            float tempZ = Random.Range(1, (levelLength - 1));

            GameObject tempObject =
                (GameObject)Instantiate(
                    theRandomOne,
                    new Vector3(
                        transform.position.x + tempX * levelBlockSize,
                        transform.position.y + tempY,
                        transform.position.z + tempZ * levelBlockSize
                    ),
                    Quaternion.identity
                );

            tempObject.transform.parent = transform;
            //randomObjects.Add(tempObject);
            level.Add(tempObject);
        }
        foreach(GameObject item in level)
        {
            testLevel += item.name + item.transform.position + "|";
        }
        CmdProvideLevelToServer(testLevel);
    }

    [Command]
    void CmdProvideLevelToServer(string pLevel)
    {
        testLevel = pLevel;
    }

    [Client]
    public string SendLevel()
    {
        return testLevel;
    }
}
