using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour {

    const float leftBorderX = -1.5f;


    public GameObject dirtPreFab;

    public GameObject diamondPreFab;
    public List<Sprite> diamondSprites;

    public GameObject rubinPreFab;
    public List<Sprite> rubinSprites;

    public GameObject yellowPreFab;
    public List<Sprite> yellowSprites;

    public GameObject greenPreFab;
    public List<Sprite> greenSprites;

    public GameObject ObjectTypeToSpawn;
    public List<Sprite> SpritesTypeToSpawn;


    public int rowNumber;
    public int valuableGrounds;


    private bool atLeastOneValuable;

    private GameObject ground;
    private List<GameObject> groundsInThisRow = new List<GameObject>();

    #region private methods
    private void Start()
    {            
            SpawnBlocks();
    }

    private void disableGrounds()
    {
        StartScreen.instance.CheckForRows();
        foreach (GameObject ground in groundsInThisRow)
        {
            if (ground.activeSelf)
            {
                if(!ground.GetComponent<groundInfo>().ground.isValuable)
                {
                    GameObject temp = EffectPool.Instance.SpawnFromPool(GroundType.GROUND, new Vector3(ground.gameObject.transform.position.x, ground.gameObject.transform.position.y, ground.gameObject.transform.position.z));
                    temp.GetComponent<ParticleSystem>().Play();
                }
                ground.SetActive(false);
            }
        }
        Destroy(gameObject);
    }

    private void spawnBorderGrounds()
    {
        ground = Instantiate(dirtPreFab) as GameObject;
        ground.transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
        ground.GetComponent<groundInfo>().owner = this;
        groundsInThisRow.Add(ground);
        ground = Instantiate(dirtPreFab) as GameObject;
        ground.transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
        ground.GetComponent<groundInfo>().owner = this;
        groundsInThisRow.Add(ground);
    }

    #endregion

    #region public methods
    public void SpawnBlocks()
    {
        if (ObjectTypeToSpawn != null)
        {

            int WhereIsValuable = Random.Range(0, 4);
            for (int i = 0; i < 4; i++)
            {
                if (i == WhereIsValuable && !atLeastOneValuable)
                {
                    ground = Instantiate(ObjectTypeToSpawn) as GameObject;
                    ground.GetComponent<SpriteRenderer>().sprite = SpritesTypeToSpawn[Random.Range(0, SpritesTypeToSpawn.Count)];
                    ground.transform.position = new Vector3(leftBorderX + i, transform.position.y, transform.position.z);
                    valuableGrounds++;

                }
                else if (Random.Range(1, 5) == 1) //Chance for valuable
                {
                    ground = Instantiate(ObjectTypeToSpawn) as GameObject;
                    ground.GetComponent<SpriteRenderer>().sprite = SpritesTypeToSpawn[Random.Range(0, SpritesTypeToSpawn.Count)];
                    ground.transform.position = new Vector3(leftBorderX + i, transform.position.y, transform.position.z);
                    valuableGrounds++;
                    atLeastOneValuable = true;
                }
                else
                {
                    ground = Instantiate(dirtPreFab) as GameObject;
                    ground.transform.position = new Vector3(leftBorderX + i, transform.position.y, transform.position.z);
                }

                ground.GetComponent<groundInfo>().owner = this;
                groundsInThisRow.Add(ground);
            }
            spawnBorderGrounds();

            foreach (GameObject childer in groundsInThisRow)
            {
                childer.transform.parent = this.transform;
            }
        }
    }


    public void tookValuable()
    {
        valuableGrounds--;
        if(valuableGrounds <= 0)
        {
            disableGrounds();
        }
    }

    public void SetPrefabs(int a)
    {

        switch(a)
        {
            case 0:
                {
                    ObjectTypeToSpawn = diamondPreFab;
                    SpritesTypeToSpawn = diamondSprites;
                }
                break;
            case 1:
                {
                    ObjectTypeToSpawn = rubinPreFab;
                    SpritesTypeToSpawn = rubinSprites;
                }
                break;
            case 2:
                {
                    ObjectTypeToSpawn = yellowPreFab;
                    SpritesTypeToSpawn = yellowSprites;
                }
                break;
            case 3:
                {
                    ObjectTypeToSpawn = greenPreFab;
                    SpritesTypeToSpawn = greenSprites;
                }
                break;
            default:
                {
                    ObjectTypeToSpawn = diamondPreFab;
                    SpritesTypeToSpawn = diamondSprites;
                }
                break;
        }
    }
    #endregion
}
