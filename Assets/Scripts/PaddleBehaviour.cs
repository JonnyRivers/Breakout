using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    private const float MovementSpeed = 2.5f;

    private BulletBehaviour m_bulletBehaviour;

    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        m_bulletBehaviour = Bullet.GetComponent<BulletBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        bool ballIsLost = Bullet.transform.position.z < -5;// TODO - derive size of world

        if(ballIsLost)
        {
            Bullet.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z + 0.1f// TODO - derive size of bat
            );
            m_bulletBehaviour.IsStuck = true;
        }

        bool moveLeft = Input.GetKey(KeyCode.A);
        bool moveRight = Input.GetKey(KeyCode.D);
        bool fire = Input.GetKey(KeyCode.Space);

        float changeInX = 0;
        if (moveLeft && !moveRight)
        {
            changeInX = Time.deltaTime * -MovementSpeed;
        }
        else if (moveRight && !moveLeft)
        {
            changeInX = Time.deltaTime * MovementSpeed;
        }

        if(changeInX + transform.position.x < -3.5f)// TODO - derive size of world
        {
            changeInX = -3.5f - transform.position.x;// TODO - derive size of world
        }

        if (changeInX + transform.position.x > 3.5f)// TODO - derive size of world
        {
            changeInX = 3.5f - transform.position.x;// TODO - derive size of world
        }

        transform.position = new Vector3(
            transform.position.x + changeInX,
            transform.position.y,
            transform.position.z);

        if(m_bulletBehaviour.IsStuck)
        {
            m_bulletBehaviour.transform.position = new Vector3(
                m_bulletBehaviour.transform.position.x + changeInX,
                m_bulletBehaviour.transform.position.y,
                m_bulletBehaviour.transform.position.z
            );

            if(fire)
            {
                m_bulletBehaviour.Launch();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == Bullet && !m_bulletBehaviour.IsStuck)
        {
            float velocityChangeInX = 0;
            float contactDistanceFromCentreX = collision.contacts[0].point.x - transform.position.x;
            if(contactDistanceFromCentreX > .1f)
            {
                velocityChangeInX = 2.0f;
                // TODO - some scaling
            }
            else if(contactDistanceFromCentreX < -.1f)
            {
                velocityChangeInX = -2.0f;
                // TODO - some scaling
            }

            if (velocityChangeInX != 0)
            {
                float speed = collision.rigidbody.velocity.magnitude;
                collision.rigidbody.velocity = collision.rigidbody.velocity + new Vector3(velocityChangeInX, 0, 0);
                collision.rigidbody.velocity = collision.rigidbody.velocity * (speed / collision.rigidbody.velocity.magnitude);
            }
        }
    }
}
