using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOrder : MonoBehaviour
{
    [SerializeField] private float speed;

    private bool onMove;

    public bool instanced;

    public CubeOrder[] cubeOrderVector;
    // Start is called before the first frame update
    private void OnEnable()
    {
        instanced = true;
    }

    private void Update()
    {
        if (!onMove && AreAllCubesInstanced())
        {
            onMove = true;
            StartCoroutine(Move());
        }
        if(transform.position.x >= 28.5) DisableCube();
    }

    private IEnumerator Move()
    {
        while (onMove)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            yield return null;
        }
    }

    public void DisableCube()
    {
        onMove = false;
        gameObject.SetActive(false);
    }

    private bool AreAllCubesInstanced() //Sees if all cubes are instantiated. It makes them start at the same time.
    {
        foreach (var c in cubeOrderVector)
        {
            if (!c.instanced) return false;
        }
        return true;
    }
}
