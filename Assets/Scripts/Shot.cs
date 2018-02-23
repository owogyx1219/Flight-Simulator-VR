using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shot : MonoBehaviour {
    public float fireRate = 0.5f;
    public float weaponRange = 100.0f;
    public float triggerDead = 0.5f;

    public Vector3 offset = new Vector3(0.0f, 2.5f, -2.5f);

    public AudioSource laser;
    public Text countText;
    private Transform plane;
    private int count;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.075f);
    private WaitForSeconds reappearDuration = new WaitForSeconds(5.0f);
    private LineRenderer laserLine;
    private float nextFire;

    private GameObject target;

	// Use this for initialization
	void Start () {
        laserLine = GetComponent<LineRenderer>();
        GameObject planeObj = GameObject.FindGameObjectWithTag("plane");
        plane = planeObj.transform;
        count = 0;
        countText.text = "SCORE: " + count.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = plane.position + offset;
        if (Mathf.Abs(Input.GetAxis("Trigger")) > triggerDead && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            RaycastHit hit;
            laserLine.SetPosition(0, plane.position);

            if (Physics.Raycast(plane.position, plane.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.collider.tag == "Target")
                {
                    count = count + 1;
                    countText.text = "SCORE: " + count.ToString();
                    target = hit.transform.gameObject;
                    if (target != null)
                    {
                        target.SetActive(false);
                        StartCoroutine(resetTarget());
                    }
                }
            }
            else {
                laserLine.SetPosition(1, plane.position + plane.forward * weaponRange);
            }
        }

	}

    private IEnumerator ShotEffect() {
        laserLine.enabled = true;
        yield return shotDuration;
        laser.Play();
        laserLine.enabled = false;
    }

    private IEnumerator resetTarget()
    {
        yield return reappearDuration;
        target.SetActive(true);
    }
}
