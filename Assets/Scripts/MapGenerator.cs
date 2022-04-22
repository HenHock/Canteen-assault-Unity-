using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject wellPrefab;
    [SerializeField] private GameObject personPrefab;

    public static int[,] titleArray;
    public static int countR = 12;
    public static int countC = 10;

    private void Start()
    {
        /*
         * 1 - well, Enemy can not move
         * 0 - ground, Enemy can move
         * 2 - person who damage enemy
         * 3 - enemy spawn possition
         * 4 - finish 
         */
        titleArray = new int[,]
        {
            { 1, 1, 1,1,1,1,1,1,1,1 },
            { 1, 1, 3,1,1,1,2,1,1,1 },
            { 1, 1, 0,1,1,1,1,1,1,1 },
            { 1, 1, 0,1,0,0,0,1,1,1 },
            { 1, 1, 0,0,0,1,0,1,1,1 },
            { 1, 1, 1,1,1,1,0,1,2,1 },
            { 1, 1, 1,1,1,1,0,1,1,1 },
            { 1, 1, 2,1,0,0,0,1,1,1 },
            { 1, 1, 1,1,0,1,0,1,1,1 },
            { 1, 0, 0,0,0,0,0,1,1,1 },
            { 1, 4, 1,1,1,1,1,1,1,1 },
            { 1, 1, 1,1,1,1,1,1,1,1 }
        };

        for (int i = 0; i < countR; i++)
            for (int j = 0; j < countC; j++)
            {
                GameObject title = null;

                if (titleArray[i, j] == 0)
                    title = Instantiate(groundPrefab);
                else if (titleArray[i, j] == 2)
                    title = Instantiate(personPrefab);
                else if (titleArray[i, j] == 3)
                    title = Instantiate(spawnPrefab);
                else if (titleArray[i, j] == 4)
                    title = Instantiate(finishPrefab);
                else title = Instantiate(wellPrefab);

                if (title != null)
                {
                    title.transform.SetParent(transform);
                    title.transform.localPosition = new Vector3(j - Screen.width / 310, i - Screen.height / 310, wellPrefab.transform.position.z);
                }
            }
    }

}
