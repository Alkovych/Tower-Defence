using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebuger : MonoBehaviour
{
    [SerializeField]
    private TileScript goal;
    [SerializeField]
    private TileScript start;

    [SerializeField]
    private Sprite BlankSprite;

	void Start () {
		
	}
	
	void Update ()
	{
	    ClickTile();

	}

    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider !=null) // if we hit something we have to make reference
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();

                if (tmp !=null)
                {
                    if (start == null)
                    {
                        start = tmp;
                        start.Debugging = true;
                        start.SpriteRenderer.sprite = BlankSprite;
                        start.SpriteRenderer.color = new Color32(255,132,0,255);
                    }
                    else if (goal == null)
                    {
                        goal = tmp;
                        goal.Debugging = true;
                        goal.SpriteRenderer.sprite = BlankSprite;
                        goal.SpriteRenderer.color = new Color32(255, 0,0,255);
                    }
                }
            }
        }
    }
}
