using UnityEngine;

public class Hover : Singleton<Hover>
{

    private SpriteRenderer spriteRenderer;


	void Start ()
	{
	    this.spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		
        FollowMouse();
	}

    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition); // отслеживает позицию мышки
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void Activate(Sprite sprite)// для спрайтов башен чтобы высветился текущяя башня с точным спрайтои
    {
        this.spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
    }

    public void Deactivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedButton = null;
    }
}
