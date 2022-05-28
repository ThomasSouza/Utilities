using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : Bullet
{
    [SerializeField] private float _debuffPercentage;
    public override void OnTriggerEnter(Collider coll)
    {
        IDamageable enemy = coll.gameObject.GetComponent<IDamageable>();
        if (enemy != null)
        {
            coll.gameObject.GetComponent<Enemy>().DebuffSpeed(_debuffPercentage);
            enemy.TakeDamage(_damage);
            GameObject newVfx = Instantiate(vfxOnDestroy, coll.ClosestPoint(transform.position), Quaternion.identity);
            Destroy(newVfx.gameObject, newVfx.GetComponent<ParticleSystem>().main.duration);
            WizardBulletFactory.Instance.ReturnBullet(this);
        }
    }
}
