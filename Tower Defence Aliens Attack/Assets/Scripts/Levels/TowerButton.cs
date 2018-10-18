using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{

    [SerializeField]
    private GameObject towerGameObject;

    [SerializeField]
    private Sprite sprite;

    public GameObject TowerGameObject {
        get { return towerGameObject; }
    }

    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceText;

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }

    }

    private void Start()
    {
        priceText.text = Price + "$";
    }


}
