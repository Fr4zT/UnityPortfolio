using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Controller : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float stopY;
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
        mCamera.backgroundColor = skyColor.Evaluate(0);
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

            var curY = transform.position.y + movement * speed * Time.deltaTime;

            if (curY > minY && curY < maxY) rocket.localEulerAngles += new Vector3(0, movement * speed, 0);

            mCamera.backgroundColor = skyColor.Evaluate(curY / maxY);
            if (curY > stopY)
            {
                mCamera.transform.parent = null;
                if (curY > stopY + 10)
                {
                    rocket.transform.localPosition = new Vector3(0, 0.059f, -3.2f);
                    var cameraPos = mCamera.transform.position;
                    mCamera.transform.position = Vector3.MoveTowards(cameraPos, new Vector3(cameraPos.x, transform.position.y, cameraPos.z), 1);
                }
                else
                {
                    rocket.transform.localPosition = new Vector3(0, 0.059f, 2);
                }
            }
            else
            {
                mCamera.transform.parent = transform;
            }
            transform.position += new Vector3(0, curY, 0);
            transform.position = new Vector3(0, Mathf.Clamp(curY, minY, maxY), transform.position.z);
        }
    }

    //IEnumerator MoveCamera()
    //{
    //    while(mCamera.transform.position!=)
    //}
}
