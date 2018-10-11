using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    //[SerializeField]
    //private GameObject towerPrefab; // just for testing // tmp will be replace later

    //public GameObject TowerPrefab
    //{
    //    get
    //    {
    //        return towerPrefab;
    //    }

    //}

    public TowerButton ClickedButton { get; private set; }

    public void PickTower(TowerButton towerButton)
    {
        this.ClickedButton = towerButton; // устанавливает башню на которую кликнули
    }

    public void BuyTower()
    {
        ClickedButton = null;
    }
}
