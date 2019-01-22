using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDiamond : MonoBehaviour {

    public GroundType type;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.Instance.updateScore();
            BonusUI.Instance.createFadeText(this.transform.position, type);
            GameObject temp = EffectPool.Instance.SpawnFromPool(type, new Vector3(transform.position.x, transform.position.y, transform.position.z));
            temp.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
        }
    }
}
