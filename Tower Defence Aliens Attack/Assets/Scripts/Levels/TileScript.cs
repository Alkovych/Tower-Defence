using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    public Point GridPosition { get; private set; }

    private Color32 fullColor = new Color32(255,118,118,255);// если что-то будет на плитке , она будет красная

    private Color32 emptyColor = new Color32(96,255,90,255);


    public bool IsEmpty { get; private set; }

    public bool Debugging { get; set; }



    public SpriteRenderer SpriteRenderer { get; set; }


    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y- (GetComponent<SpriteRenderer>().bounds.size.y/2 ));
        }
    }

    // Use this for initialization
    void Start ()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(Point gridPos,Vector3 worldPos,Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;

        transform.SetParent(parent); // для иерархии в юнити когда запускаешь программу , чтобы все созданные обьекты были внутри одного тела , для удобства

        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
       

        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null) // Для того , чтобы башни не ставились ссази кнопки и без нажатия на кнопку
        {

            if (IsEmpty && Debugging)
            {
                ColorTile(emptyColor);
            }
            if(!IsEmpty && Debugging)
            {
                ColorTile(fullColor);
            }

            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        } 
    }

    private void OnMouseExit()
    {
        if (!Debugging)
        {
            ColorTile(Color.white);
        }

    }

    public void PlaceTower() // Размещение бащень
    {


        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedButton.TowerGameObject, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y; //чтобы башни не накладывались друг на друга , берем по оси У каждую линию и устанавливаем значение линии 1 или 2 и тд в layer
        tower.transform.SetParent(transform); // для компоновки объектов на сцене . Башни теперь как дочерние обхекты плиток


        ColorTile(Color.white);
        IsEmpty = false;


        GameManager.Instance.BuyTower();
    }

    private void ColorTile(Color32 newColor)
    {
        SpriteRenderer.color = newColor;
    }
}
