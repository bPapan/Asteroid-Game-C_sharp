using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Sprite asteroidSprite0;
    [SerializeField]
    Sprite asteroidSprite1;
    [SerializeField]
    Sprite asteroidSprite2;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        // set random sprite for asteroid
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber < 1)
        {
            spriteRenderer.sprite = asteroidSprite0;
        }
        else if (spriteNumber < 2)
        {
            spriteRenderer.sprite = asteroidSprite1;
        }
        else
        {
            spriteRenderer.sprite = asteroidSprite2;
        }
	}

    /// <summary>
    /// Starts the asteroid moving in the given direction
    /// </summary>
    /// <param name="direction">direction for the asteroid to move</param>
    /// <param name="position">position for the asteroid</param>
    public void Initialize(Direction direction, Vector3 position)
    {
        // set asteroid position
        transform.position = position;

        // set random angle based on direction
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

        // apply impulse force to get asteroid moving
        /*const float MinImpulseForce = 2f;
        const float MaxImpulseForce = 4f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);*/
        StartMoving(angle);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Play(AudioClipName.AsteroidHit);
            //Destroy(gameObject);
            if (transform.localScale.x >= 0.5)
            {
                Vector3 scale = transform.localScale;
                scale.x = scale.x / 2;
                scale.y = scale.y / 2;
                transform.localScale = scale;

                CircleCollider2D asteroidCollider = gameObject.GetComponent<CircleCollider2D>();
                float rad = asteroidCollider.radius;
                rad = rad / 2;
                asteroidCollider.radius = rad;

                //Vector2 Position = new Vector2(0.0f, 5.0f);
                GameObject newAsteroid1 = Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
                newAsteroid1.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));

                GameObject newAsteroid2 = Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
                newAsteroid2.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI));

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            Destroy(coll.gameObject);
        }
    }

    public void StartMoving(float angle)
    {
        const float MinImpulseForce = 2f;
        const float MaxImpulseForce = 4f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }
}
