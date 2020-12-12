using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ship
/// </summary>
public class Ship : MonoBehaviour
{
    // thrust and rotation support
    Rigidbody2D rb2D;
    Vector2 thrustDirection = new Vector2(1, 0);
    float rotationAmount;
    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 180;

    [SerializeField]
    GameObject prefabBullet;

    [SerializeField]
    GameObject hud;
    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
		// saved for efficiency
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        // check for rotation input
        float rotationInput = Input.GetAxis("Rotate");
        //float rotationAmount = 0;
        if (rotationInput != 0) {

            // calculate rotation amount and apply rotation
            rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject newBullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            Bullet bullet = newBullet.GetComponent<Bullet>();
            bullet.ApplyForce(thrustDirection);

            AudioManager.Play(AudioClipName.PlayerShot);
        }
	}

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate()
    {
        // thrust as appropriate
        if (Input.GetAxis("Thrust") != 0)
        {
            rb2D.AddForce(ThrustForce * thrustDirection,
                ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Destroys the ship on collision with an asteroid
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Asteroid"))
        {
            AudioManager.Play(AudioClipName.PlayerDeath);
            Destroy(gameObject);
            hud.GetComponent<HUD>().StopGameTimer();
        }
    }

    public float getRotationAmount()
    {
        return(rotationAmount);
    }
}
