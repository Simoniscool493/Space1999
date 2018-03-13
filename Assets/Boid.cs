using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public float Mass;
    public float MaxSpeed;

    public bool IsSeeking;
    public bool IsOffsetPursue;
    public bool IsCameraFocusedOn;

    private Vector3 Force;
    private Vector3 Acceleration;
    private Vector3 Velocity;

    public GameObject Target;
    public Vector3 OffSet;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Force = Calculate();
        //rb.AddForce(Force);

        Acceleration = Force / Mass;

        var newVelocity = Acceleration * Time.deltaTime;

        Velocity = Vector3.Lerp(Velocity, newVelocity, 0.15f);
        Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);

        //if (IsSeeking) { Debug.Log(Velocity.magnitude); }

        transform.position += (Velocity * Time.deltaTime);

        if(IsCameraFocusedOn)
        {
            var camera = FindObjectOfType<Camera>();
            camera.transform.position = transform.position + new Vector3(0,20,0);
            camera.transform.rotation = Quaternion.EulerAngles(90, 0, 0);
        }
	}

    Vector3 Calculate()
    {
        Vector3 total = Vector3.zero;

        if(IsSeeking)
        {
            total += Seek(Target.transform.position);
        }
        if (IsOffsetPursue)
        {
            total += OffsetPursue(OffSet,Target.transform.position);
        }

        return total;
    }

    Vector3 Seek(Vector3 targetPos)
    {
        var direction = (targetPos- transform.position).normalized;
        var desiredVelocity = direction * MaxSpeed;

        return desiredVelocity;
    }

    Vector3 OffsetPursue(Vector3 offSet,Vector3 target)
    {
        return Seek(target + offSet);
    }
}
