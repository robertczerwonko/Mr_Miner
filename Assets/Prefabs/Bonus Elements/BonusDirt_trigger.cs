using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDirt_trigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject temp = EffectPool.Instance.SpawnFromPool(GroundType.GROUND, new Vector3(transform.position.x, transform.position.y, transform.position.z));
            temp.GetComponent<ParticleSystem>().Play();

            Destroy(this.gameObject);
        }
    }
}
