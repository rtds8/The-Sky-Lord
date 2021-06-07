using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Projectile : MonoBehaviour
{
    public float _speed = 2f, _fireRate = 2f;
    [SerializeField] private GameObject m_muzzleFlash, m_hitImpact;
    Vector3 m_startPoint;
 
    void Start()
    {
        m_startPoint = transform.position;

        /*if (m_muzzleFlash != null)
        {
            var muzzleEffect = Instantiate(m_muzzleFlash, transform.position, Quaternion.identity);
            muzzleEffect.transform.forward = gameObject.transform.forward;

            var muzzleParticle = muzzleEffect.GetComponent<ParticleSystem>();

            if (muzzleParticle != null)
                Destroy(muzzleEffect, muzzleParticle.main.duration);

            else
            {
                var muzzleParticleChild = muzzleEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleEffect, muzzleParticleChild.main.duration);
            }
        }*/
    }

    void Update()
    {
        if(_speed != 0)
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }

        else
        {
            Debug.Log("No Speed!!!!!");
        }

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Vector3 currentPos = transform.position;
        if (Vector3.Distance(m_startPoint, currentPos) >= 300f && this.gameObject.activeInHierarchy)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Guard Ship")
        {
            /*ContactPoint _contactPoint = collision.contacts[0];
            Quaternion _rotation = Quaternion.FromToRotation(Vector3.up, _contactPoint.normal);
            Vector3 _position = _contactPoint.point;*/
/*
            if (m_hitImpact != null)
            {
                var hitEffect = Instantiate(m_hitImpact, other.transform.position, other.transform.rotation.normalized);

                var hitParticle = hitEffect.GetComponent<ParticleSystem>();

                if (hitParticle != null)
                    Destroy(hitEffect, hitParticle.main.duration);

                else
                {
                    var hitParticleChild = hitEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitEffect, hitParticleChild.main.duration);
                }
            }*/

            Destroy(gameObject);
        }

        else
            return;
    }
}
