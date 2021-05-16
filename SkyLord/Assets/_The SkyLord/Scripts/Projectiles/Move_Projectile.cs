using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Projectile : MonoBehaviour
{
    public float _speed = 2f, _fireRate = 2f;
    [SerializeField] private GameObject m_muzzleFlash, m_hitImpact;

    void Start()
    {
        if(m_muzzleFlash != null)
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
        }
    }

    void FixedUpdate()
    {
        if(_speed != 0)
        {
            transform.position += transform.forward * (_speed * Time.deltaTime);
        }

        else
        {
            Debug.Log("No Speed!!!!!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _speed = 0;

        ContactPoint _contactPoint = collision.contacts[0];
        Quaternion _rotation = Quaternion.FromToRotation(Vector3.up, _contactPoint.normal);
        Vector3 _position = _contactPoint.point;

        if(m_hitImpact != null)
        {
            var hitEffect = Instantiate(m_hitImpact, _position, _rotation);

            var hitParticle = hitEffect.GetComponent<ParticleSystem>();

            if (hitParticle != null)
                Destroy(hitEffect, hitParticle.main.duration);

            else
            {
                var hitParticleChild = hitEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitEffect, hitParticleChild.main.duration);
            }
        }
            
        Destroy(gameObject);
    }
}
