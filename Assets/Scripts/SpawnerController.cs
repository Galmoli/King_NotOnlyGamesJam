using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerController : MonoBehaviour
{
    public int numOfArrows = 2;
    [SerializeField] private Transform canvasParent;
    [SerializeField] private float initialSpawnTime = 4;
    [SerializeField] private float minSpawnTime = 1.5f;
    [SerializeField] private float spawnTimeDecrement = 0.1f; //Each time an arrow spawns it decrements this value.
    [SerializeField] private float decrementOnWinStreak = 0.5f; //Every 3 times wins in a row it decrements this value.
    [SerializeField] private float incrementOnLoseStreak = 0.6f; //Every 3 loses in a row it increments this value.
    [SerializeField] private float maxDelay = 0.6f; //Every 3 loses in a row it increments this value.
    [SerializeField] private int offsetBetweenArrows = 33;

    //Prefabs
    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject downArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject leftArrow;
    
    //Streaks
    private int winStreak;
    private int loseStreak;
    
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
        GameManager.OnCorrectPos += OnCorrect;
        GameManager.OnIncorrectPos += OnIncorrect;
    }

    // Update is called once per frame
    void Update()
    {
        if (winStreak == 3)
        {
            if(spawnTime > minSpawnTime + decrementOnWinStreak) spawnTime -= decrementOnWinStreak;
            winStreak = 0;
        }
        if (loseStreak == 3)
        {
            if(spawnTime < initialSpawnTime - incrementOnLoseStreak) spawnTime += incrementOnLoseStreak;
            loseStreak = 0;
        }
        
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
            var delay = Random.Range(0, maxDelay);
            StartCoroutine(SpawnDelay(go, delay));
        }
    }

    private IEnumerator SpawnDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(true);
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

    private void OnCorrect()
    {
        loseStreak = 0;
        winStreak++;
    }

    private void OnIncorrect()
    {
        winStreak = 0;
        loseStreak++;
    }
}
