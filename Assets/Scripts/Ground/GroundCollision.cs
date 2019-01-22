using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GroundCollision : MonoBehaviour {


    private Ground gInfo;
    private groundInfo groundInfoComponent;
    private Row Owner;

    public void Start()
    {

        groundInfoComponent = GetComponent<groundInfo>();
        gInfo = groundInfoComponent.ground;
        Owner = groundInfoComponent.owner;
        
    }

	private void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject.tag == "Player")
            {
                //Allow player to catch ground and diamond with profit for him
                if (GameManager.Instance.CurrentType == GroundType.EMPTY || GameManager.Instance.CurrentType == GroundType.GROUND)
                {
                    GameManager.Instance.CurrentType = gInfo.type;

                }

                if (gInfo.isValuable)
                {
                    col.gameObject.GetComponent<HoistManager>().activeType(gInfo.type);
                    gameObject.SetActive(false);
                    BonusUI.Instance.createFadeText(this.transform.position, gInfo.type);
                    Owner.tookValuable();
                }
                else
                {
                    gameObject.SetActive(false);
                }

                GameObject temp = EffectPool.Instance.SpawnFromPool(gInfo.type, new Vector3(transform.position.x, transform.position.y, transform.position.z));
                temp.GetComponent<ParticleSystem>().Play();



                if (GameManager.Instance.CurrentType == GroundType.GROUND)
                {
                    CameraShaker.Instance.ShakeOnce(2, 2, 1, 1);//TODO:(C) Shaking camera if Ground type is "Ground". In this method I control which ground type is triggered and sending messange to hoistManager
                }

            }
    }
}





