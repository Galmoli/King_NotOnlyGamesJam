using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerController : MonoBehaviour
{
    public int numOfArrows = 2;
    [SerializeField] private Transform canvasParent;
    [SerializeField] private float initialSpawnTime = 4;
    [SerializeField] private float minSpawnTime = 2;
    [SerializeField] private float spawnTimeDecrement = 0.1f;
    [SerializeField] private int offsetBetweenArrows = 33;

    //Prefabs
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject leftArrow;
    
    //SpawnPositions
    [SerializeField] private RectTransform[]  positionsVector;

    private GameObject[] arrowVector;
    
    private float currentTime;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        arrowVector = new GameObject[numOfArrows];
        spawnTime = initialSpawnTime;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= spawnTime)
        {
            GetNextArrowVector();
            SpawnOrder();
            currentTime = 0;
            if(spawnTime > minSpawnTime + spawnTimeDecrement) spawnTime -= spawnTimeDecrement;
        }
        currentTime += Time.deltaTime;
    }

    private void SpawnOrder()
    {
        foreach (var go in arrowVector)
        {
            go.SetActive(true);
        }
    }

    private void GetNextArrowVector()
    {
        GameObject[] vector = new GameObject[numOfArrows];

        for (int i = 0; i < arrowVector.Length; i++)
        {
            int randomN = Random.Range(0, 4);
            switch (randomN)
            {
                case 0:
                {
                    vector[i] = Instantiate(upArrow, canvasParent);
                    break;
                }
                case 1:
                {
                    vector[i] = Instantiate(downArrow, canvasParent);
                    break;
                }
                case 2:
                {
                    vector[i] = Instantiate(rightArrow, canvasParent);
                    break;
                }
                case 3:
                {
                    vector[i] = Instantiate(leftArrow, canvasParent);
                    break;
                }
            }
            vector[i].SetActive(false);
        }

        arrowVector = vector;
        SetPositions();
        foreach (var c in arrowVector)
        {
            c.GetComponent<Arrow>().arrowVector = GetArrowComponents();
        }
    }

    private void SetPositions()
    {
        var offset = 0;
        if (numOfArrows == 2) offset = offsetBetweenArrows;
        else offset = 0;
        for (int i = 0; i < arrowVector.Length; i++)
        {
            var arrow = arrowVector[i].GetComponent<Arrow>();
            arrow.rect.position = new Vector2(positionsVector[i].position.x + offset, positionsVector[i].position.y);
            arrow.SetPlayer(i);
        }
    }

    private Arrow[] GetArrowComponents()
    {
        Arrow[] components = new Arrow[numOfArrows];
        for(int i = 0; i < arrowVector.Length; i++)
        {
            components[i] = arrowVector[i].GetComponent<Arrow>();
        }

        return components;
    }
}
