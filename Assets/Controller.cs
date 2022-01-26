using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] ParticleSystem smoke;
    ParticleSystem.MainModule mainSmoke;
    [SerializeField] Transform rocket;
    Camera mCamera;
    [SerializeField] Gradient skyColor;
    // Start is called before the first frame update
    void Start()
    {
        mainSmoke = smoke.main;
        mCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var movement = -Input.mouseScrollDelta.y;
        if (movement == 0)
        {
            mainSmoke.simulationSpeed = 0;
            return;
        }
        else
        {
            mainSmoke.simulationSpeed = Mathf.Abs(movement) * speed;
            if (movement > 0) smoke.Emit(5);
        }
        transform.position += new Vector3(0, movement * speed * Time.deltaTime, 0);

        var curY = transform.position.y;

        if (curY > minY && curY < maxY) rocket.localEulerAngles += new Vector3(0, movement * speed, 0);
        transform.position = new Vector3(0, Mathf.Clamp(curY, minY, maxY), 0);

        mCamera.backgroundColor = skyColor.Evaluate(curY / maxY);
    }
}
