using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private GameObject towerPrefab; // just for testing // tmp will be replace later

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
