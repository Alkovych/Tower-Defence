using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    public Point GridPosition { get; private set; }

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y- (GetComponent<SpriteRenderer>().bounds.size.y/2 ));
        }
    }

    // Use this for initialization
    void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(Point gridPos,Vector3 worldPos,Transform parent)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;

        transform.SetParent(parent); // для иерархии в юнити когда запускаешь программу , чтобы все созданные обьекты были внутри одного тела , для удобства

        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null) // Для того , чтобы башни не ставились ссази кнопки и без нажатия на кнопку
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    public void PlaceTower() // Размещение бащень
    {


        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedButton.TowerGameObject, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y; //чтобы башни не накладывались друг на друга , берем по оси У каждую линию и устанавливаем значение линии 1 или 2 и тд в layer
        tower.transform.SetParent(transform); // для компоновки объектов на сцене . Башни теперь как дочерние обхекты плиток

        GameManager.Instance.BuyTower();
    }
}
