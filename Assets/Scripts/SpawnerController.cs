using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private int numOfOrderCubes = 2;
    [SerializeField] private float initialSpawnTime = 4;
    [SerializeField] private float minSpawnTime = 2;
    [SerializeField] private float spawnTimeDecrement = 0.1f;

    //Prefabs
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject leftArrow;
    
    //SpawnPositions
    [SerializeField] private Transform[]  positionsVector;

    private GameObject[] orderCubeVector;
    
    private float currentTime;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        orderCubeVector = new GameObject[numOfOrderCubes];
        spawnTime = initialSpawnTime;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= spawnTime)
        {
            GetNextCubeOrderVector();
            SpawnOrder();
            currentTime = 0;
            if(spawnTime > minSpawnTime + spawnTimeDecrement) spawnTime -= spawnTimeDecrement;
        }
        currentTime += Time.deltaTime;
    }

    private void SpawnOrder()
    {
        foreach (var go in orderCubeVector)
        {
            go.SetActive(true);
        }
    }

    private void GetNextCubeOrderVector()
    {
        GameObject[] vector = new GameObject[numOfOrderCubes];

        for (int i = 0; i < orderCubeVector.Length; i++)
        {
            int randomN = Random.Range(0, 4);
            switch (randomN)
            {
                case 0:
                {
                    vector[i] = Instantiate(upArrow, transform);
                    break;
                }
                case 1:
                {
                    vector[i] = Instantiate(downArrow, transform);
                    break;
                }
                case 2:
                {
                    vector[i] = Instantiate(rightArrow, transform);
                    break;
                }
                case 3:
                {
                    vector[i] = Instantiate(leftArrow, transform);
                    break;
                }
            }
            vector[i].SetActive(false);
        }

        orderCubeVector = vector;
        SetPositions();
        foreach (var c in orderCubeVector)
        {
            c.GetComponent<CubeOrder>().cubeOrderVector = GetCubeOrderComponents();
        }
    }

    private void SetPositions()
    {
        int k;
        if (numOfOrderCubes == 2) k = 1;
        else k = 0;
        for (int i = k; i < orderCubeVector.Length; i++)
        {
            orderCubeVector[i].transform.position = positionsVector[i].position;
        }
    }

    private CubeOrder[] GetCubeOrderComponents()
    {
        CubeOrder[] components = new CubeOrder[numOfOrderCubes];
        for(int i = 0; i < orderCubeVector.Length; i++)
        {
            components[i] = orderCubeVector[i].GetComponent<CubeOrder>();
        }

        return components;
    }
}
