using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float cameraSpeed = 0;

    private float xMax;
    private float yMin;

    private void Update () {

        GetInput();

    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime); // передвежение камеры при нажатии на букву w сос коростью задаваемой в инспекторе
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime); // передвежение камеры при нажатии на букву w сос коростью задаваемой в инспекторе
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime); // передвежение камеры при нажатии на букву w сос коростью задаваемой в инспекторе
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime); // передвежение камеры при нажатии на букву w сос коростью задаваемой в инспекторе
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y,yMin+1,1),-10);
    }

    public void SetLimits(Vector3 maxTile)
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1,0)); //чтобы камера не уходила за пределы границы карты

        xMax = maxTile.x - wp.x; // сколько мы можем двигать камеру , мчитаем по камере правого нижнего угла и отнимаем растояние от последнего куска квадрата
        yMin = maxTile.y - wp.y;
    }
}
