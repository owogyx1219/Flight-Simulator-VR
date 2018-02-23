using UnityEngine;
using System.Collections;

public class RotateTranslate : MonoBehaviour {
    public float translateDead = 0.19f;
    public float translateAccelerationFactor = 7.5f;
    public AudioSource AccAudio;
    public AudioSource DecAudio;

    private float speedMax = 50.0f;
    private float speed = 0.0f;
    private bool start = false;

    public float rotateDead = 0.19f;
    public float rotateSensitive = 2.5f;
	
	// Update is called once per frame
	void Update () {
        //Accelerate&Decelerate
        float cellerate = 0.0f;
        float tempCellerate = Input.GetAxis("Accelerate");
        if (Mathf.Abs(tempCellerate) > translateDead)
            start = true;
            if (tempCellerate < 0)
                DecAudio.Play();
            if (tempCellerate > 0)
                AccAudio.Play();
            cellerate = tempCellerate * translateAccelerationFactor;

        if (start == true) {
            float movement = 0.125f * (speed * Time.deltaTime + 0.5f * cellerate * translateAccelerationFactor * Time.deltaTime * Time.deltaTime);
            speed = Mathf.Max(speedMax, speed + cellerate * Time.deltaTime * translateAccelerationFactor);
            transform.Translate(0, 0, movement);
        }

        //Forward
        float forwardDistance = Input.GetAxis("Translate");
        if (Mathf.Abs(forwardDistance) > translateDead)
            transform.Translate(0, 0, forwardDistance);

        //Rotate
        float rotateX = Input.GetAxis("Mouse X");
        float rotateY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(rotateX) > rotateDead)
            transform.Rotate(0, rotateSensitive * rotateX, 0);

        if (Mathf.Abs(rotateY) > rotateDead)
            transform.Rotate(rotateSensitive * rotateY, 0, 0);
    }
}
