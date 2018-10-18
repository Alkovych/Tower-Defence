using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

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

    [SerializeField]
    private Text currecncyText;

    private int currency;


    private void Start()
    {
        currency = 5;
    }

    void Update()
    {
        HandleEscape();
    }

    public TowerButton ClickedButton { get; set; }

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;

            this.currecncyText.text = value.ToString() + "<color=lime>$</color>";
        }
    }

    public void PickTower(TowerButton towerButton)
    {
        if (Currency >= towerButton.Price)
        {
            this.ClickedButton = towerButton; // устанавливает башню на которую кликнули
            Hover.Instance.Activate(towerButton.Sprite);
        }
    }

    public void BuyTower()
    {
        if (Currency <= ClickedButton.Price)
        {
            Currency -= ClickedButton.Price;

            Hover.Instance.Deactivate();
        }

       // ClickedButton = null;
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }
}
