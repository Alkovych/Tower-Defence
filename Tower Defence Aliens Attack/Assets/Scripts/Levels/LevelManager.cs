using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>

    // Данный клас создает поле битвы для игры 

{
    [SerializeField] // для сокрытия данных , но по прежнему доступно в редакторе
    private GameObject[] tilePrefabs; //замля которая идет по координатам х и y 

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private Transform map; // также для иерархии как и в TileScript продолжение

    private Point mapSize;

    public Dictionary<Point , TileScript> Tiles { get; set; }

    private Point greenSpown, redSpown; // порталы

    [SerializeField]
    private GameObject greenPortal;
    [SerializeField]
    private GameObject redPortal;


    // подсчет на сколько большие кусочки нашего пола для компоновки их вместе без стыков // Свойство
    public float TileSize
    {
       get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Start()
    {
        
        CreateLevel();
    }

    void Update()
    {

    }

    //Test
    private void TestDictionary()
    {
        Dictionary<string, int> testDictionary = new Dictionary<string, int>();
        testDictionary.Add("Age", 24);
        testDictionary.Add("Strenght", 300);
        testDictionary.Add("Special", 100);

    }


    private void CreateLevel() // Данная функция создает уровень , нарисовка карты с помощью квадратов.
    {

        Tiles = new Dictionary<Point, TileScript>();



        string[] mapData = ReadLevelText();

        mapSize = new Point(mapData[0].ToCharArray().Length, mapData.Length);

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        // подсчет на сколько большие кусочки нашего пола для компоновки их вместе без стыков
        //float tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); // для нахождения левого верхнего угла для правильного построения карты 

        for (int y = 0; y < mapYSize; y++)
        {

            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapXSize; x++)
            {
               PlaceTile(newTiles[x].ToString(), x,y, worldStart);
            }
        }

        maxTile = Tiles[new Point(mapXSize - 1, mapYSize - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x+TileSize,maxTile.y -TileSize));

        SpawnPortals();
    }

    //Размещает кусочки земли
    private void PlaceTile(string tileType, int x, int y , Vector3 willStart)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
        //newTile.transform.position = new Vector3(willStart.x + (TileSize * x), willStart.y - (TileSize * y), 0); // разметка полей , на данном цыкле 25 квадратов вместе.

        newTile.Setup(new Point(x, y), new Vector3(willStart.x + (TileSize * x), willStart.y - (TileSize * y), 0), map);



        //return newTile.transform.position;
    }


    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;//загрузка карты с файла

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-'); 
    }

    private void SpawnPortals()
    {
        greenSpown = new Point(0, 0); // задаем расположение порталов

        Instantiate(greenPortal, Tiles[greenSpown].GetComponent<TileScript>().WorldPosition, Quaternion.identity);// создаеться портал вверху слева


        redSpown = new Point(20, 10);// задаем расположение порталов

        Instantiate(redPortal, Tiles[redSpown].GetComponent<TileScript>().WorldPosition, Quaternion.identity);// создаеться портал вверху слева
    }

    public bool InBounds(Point position )
    {
        return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y; // проверка чтобы не размещались за пределами карты 
    }
}
