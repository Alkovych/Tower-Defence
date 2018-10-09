using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    public void PlaceTower() // Размещение бащень
    {
        Instantiate(GameManager.Instance.TowerPrefab, transform.position, Quaternion.identity);
    }
}
