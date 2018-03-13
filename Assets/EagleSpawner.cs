using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour {

    public float gap;
    public float followers;
    public GameObject prefab;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Awake()
    {
        var eagle = Instantiate(prefab);
        eagle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        var leaderBoid = eagle.GetComponent<Boid>();
        var target = new GameObject();
        target.transform.Translate(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1000));

        leaderBoid.Target = target;
        leaderBoid.IsSeeking = true;
        leaderBoid.IsCameraFocusedOn = true;

        for (int i=1;i<followers;i++)
        {
            var offSet1 = new Vector3(gap * i, 0, gap*i);
            var offSet2 = new Vector3(gap * i, 0, -gap*i);

            CreateEagleWithOffset(offSet1,eagle);
            CreateEagleWithOffset(offSet2,eagle);
        }
    }

    private void CreateEagleWithOffset(Vector3 offset,GameObject target)
    {
        var newEagle = Instantiate(prefab);
        newEagle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        newEagle.transform.Translate(offset);
        var newBoid1 = newEagle.GetComponent<Boid>();
        newBoid1.Target = target;
        newBoid1.OffSet = new Vector3(offset.z, offset.y, -offset.x);
        newBoid1.IsOffsetPursue = true;
    }
}