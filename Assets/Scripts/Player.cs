using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D rigidBody;
	public Camera camera2D;	

	public float speedX;
	public float speedY;
	public float takeOffSpeed;
	public float maxSpeed;
	public float offset = 0f;
	public bool lockCamFront = false;
	
	public Vector3 speed;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		speed = rigidBody.velocity;
		if (Input.GetMouseButton(0))
		{
			Vector2 force2D = Vector2.zero;
			if (rigidBody.velocity.x > takeOffSpeed && transform.localPosition.y <= 2f)
			{
				if (rigidBody.velocity.x < maxSpeed)
				{
					force2D = new Vector2( speedX, speedY);
				}
				else
				{
					force2D = new Vector2( 0f, speedY);
				}
			}
			else
			{
				rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
				if (rigidBody.velocity.x < maxSpeed)
				{
					force2D = new Vector2( speedX, 0f);
				}
			}
			rigidBody.AddForce( force2D );
		}
		else if (rigidBody.velocity.x > 0)
		{
			//rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
			rigidBody.AddForce( new Vector2( -speedX*0.5f, -speedY ) );
		}
		else if (rigidBody.velocity.x < 0)
		{
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
			rigidBody.velocity = new Vector2(0f,0f);
		}

		Vector3 pos = camera2D.WorldToViewportPoint( transform.localPosition );
		if (pos.x > 0.6f && !lockCamFront)
		{
			lockCamFront = true;
			offset = camera2D.transform.localPosition.x - rigidBody.transform.localPosition.x;
		}
		if (lockCamFront)
		{
			Vector3 newPos = camera2D.transform.localPosition;
			newPos.x = transform.localPosition.x + offset;
			camera2D.transform.localPosition = newPos;
		}
	}
}
