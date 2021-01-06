using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPlacer : MonoBehaviour
{

    Vector3 temp;
    Vector3 position;
    public Body body;

    public LineRenderer line;
    public GameObject nose, leftEye, rightEye, leftEar,
                rightEar, leftShoulder, rightShoulder, leftElbow,
                rightElbow, leftWrist, rightWrist, leftHip,
                rightHip, leftKnee, rightKnee, leftAnkle, rightAnkle;


    public void SetPosition()
    {
        temp.Set(body.nose.x/body.width, body.nose.y/body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        nose.transform.position = position;

        temp.Set(body.leftEye.x / body.width, body.leftEye.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftEye.transform.position = position;

        temp.Set(body.rightEye.x / body.width, body.rightEye.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightEye.transform.position = position;

        temp.Set(body.leftEar.x / body.width, body.leftEar.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftEar.transform.position = position;

        temp.Set(body.rightEar.x / body.width, body.rightEar.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightEar.transform.position = position;

        temp.Set(body.leftShoulder.x / body.width, body.leftShoulder.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftShoulder.transform.position = position;

        temp.Set(body.rightShoulder.x / body.width, body.rightShoulder.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightShoulder.transform.position = position;

        temp.Set(body.leftElbow.x / body.width, body.leftElbow.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftElbow.transform.position = position;

        temp.Set(body.rightElbow.x / body.width, body.rightElbow.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightElbow.transform.position = position;

        temp.Set(body.leftWrist.x / body.width, body.leftWrist.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftWrist.transform.position = position;

        temp.Set(body.rightWrist.x / body.width, body.rightWrist.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightWrist.transform.position = position;

        temp.Set(body.leftHip.x / body.width, body.leftHip.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftHip.transform.position = position;

        temp.Set(body.rightHip.x / body.width, body.rightHip.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightHip.transform.position = position;

        temp.Set(body.leftKnee.x / body.width, body.leftKnee.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftKnee.transform.position = position;

        temp.Set(body.rightKnee.x / body.width, body.rightKnee.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightKnee.transform.position = position;

        temp.Set(body.leftAnkle.x / body.width, body.leftAnkle.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        leftAnkle.transform.position = position;

        temp.Set(body.rightAnkle.x / body.width, body.rightAnkle.y / body.height, 0);
        position = Camera.main.ViewportToWorldPoint(temp);
        position.z = 0;
        rightAnkle.transform.position = position;

    }

    public void DrawLine()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        temp = new Vector3();
        position = new Vector3();
        body = new Body();
        line = FindObjectOfType<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
