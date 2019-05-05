using System.Collections.Generic;
using UnityEngine;

public class dropletManager : MonoBehaviour {
    public GameObject dropletPrefab;
    public Vector3 bucketPos;

    public Vector3 offset;

    public float numberOfDroplets;
    public List<droplet> droplets;
    private int count;
    private int MAXSIZE = 150;
    
    void createDroplet(Vector3 position)
    {
        GameObject dropletObject = GameObject.Instantiate(dropletPrefab);
        droplet dropletComponent = dropletObject.GetComponent<droplet>();
        dropletObject.transform.parent = this.transform;
        dropletObject.transform.position = position;
        droplets.Add(dropletComponent);
    }
	// Use this for initialization
	void Start () {
        bucketPos = GameObject.Find("Bucket").GetComponent<Transform>().position;
        droplets = new List<droplet>();
        count = 0;
        /*for (int i = 0; i < numberOfDroplets; i++)
        {
            createDroplet(new Vector3(0, 2f, 0) * (i + 1));
        }
        Debug.Log("" + droplets.Count); */
    }

    // Update is called once per frame
    void Update()
    {
        if (count < MAXSIZE)
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                bucketPos = GameObject.Find("Bucket").GetComponent<Transform>().position;
                createDroplet(bucketPos + offset);
                count++;
            }

            //Debug.Log("" + droplets.Count);
        }
        if(OVRInput.Get(OVRInput.Button.Two)){
            foreach (droplet d in droplets)
            {
                Destroy(d);
            }
            droplets.Clear();
            count = 0;
        }
    }
}
