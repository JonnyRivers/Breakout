using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    private const float PaddleMovementSpeed = 3.0f;
    private const float BulletLaunchForce = 20f;
    private const float PaddleSkewMaxForce = 10f;

    private BulletBehaviour m_bulletBehaviour;
    private Rigidbody m_bulletRigidbody;

    public GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        m_bulletBehaviour = Bullet.GetComponent<BulletBehaviour>();
        m_bulletRigidbody = Bullet.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moveLeft = Input.GetKey(KeyCode.A);
        bool moveRight = Input.GetKey(KeyCode.D);
        bool fire = Input.GetKey(KeyCode.Space);

        float changeInX = 0;
        if (moveLeft && !moveRight)
        {
            changeInX = Time.deltaTime * -PaddleMovementSpeed;
        }
        else if (moveRight && !moveLeft)
        {
            changeInX = Time.deltaTime * PaddleMovementSpeed;
        }

        if(changeInX + transform.position.x < -4.45f)// TODO - derive size of world
        {
            changeInX = -4.45f - transform.position.x;// TODO - derive size of world
        }

        if (changeInX + transform.position.x > 4.45f)// TODO - derive size of world
        {
            changeInX = 4.45f - transform.position.x;// TODO - derive size of world
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
                float launchAngle = Random.Range(-Mathf.PI / 6, Mathf.PI / 6);// 30° either side of +Z
                float forceX = Mathf.Sin(launchAngle);
                float forceZ = Mathf.Cos(launchAngle);
                m_bulletRigidbody.AddForce(new Vector3(forceX, 0, forceZ) * BulletLaunchForce);
                m_bulletBehaviour.IsStuck = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == Bullet && !m_bulletBehaviour.IsStuck)
        {
            float forceInX = 0;
            float contactDistanceFromCentreX = collision.contacts[0].point.x - transform.position.x;
            if(contactDistanceFromCentreX > .1f)
            {
                float skewRatio = (contactDistanceFromCentreX - .1f) / .4f;
                forceInX = PaddleSkewMaxForce * skewRatio;
            }
            else if(contactDistanceFromCentreX < -.1f)
            {
                float skewRatio = (-contactDistanceFromCentreX - .1f) / .4f;
                forceInX = -PaddleSkewMaxForce * skewRatio;
            }

            if (forceInX != 0)
            {
                collision.rigidbody.AddForce(new Vector3(forceInX, 0, 0));
            }
        }
    }
}
