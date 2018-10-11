using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{

    [SerializeField]
    private GameObject towerGameObject;

    public GameObject TowerGameObject {
        get { return towerGameObject; }
    }
}
