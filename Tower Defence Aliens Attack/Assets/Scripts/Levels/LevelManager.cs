using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour

    // Данный клас создает поле битвы для игры 

{
    [SerializeField] // для сокрытия данных , но по прежнему доступно в редакторе
    private GameObject[] tilePrefabs; //замля которая идет по координатам х и y 

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


    private void CreateLevel() // Данная функция создает уровень , нарисовка карты с помощью квадратов.
    {

        string[] mapData = ReadLevelText();


        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        // подсчет на сколько большие кусочки нашего пола для компоновки их вместе без стыков
        //float tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); // для нахождения левого верхнего угла для правильного построения карты 

        for (int y = 0; y < mapYSize; y++)
        {

            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(), x,y, worldStart);
            }
        }
    }

    //Размещает кусочки земли
    private void PlaceTile(string tileType, int x, int y , Vector3 willStart)
    {
        int tileIndex = int.Parse(tileType);

        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(willStart.x + (TileSize * x), willStart.y - (TileSize * y), 0); // разметка полей , на данном цыкле 25 квадратов вместе.
    }


    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;//загрузка карты с файла

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-'); 
    }
}
