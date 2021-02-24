using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public PoolObject[] enemy;
    bool control;
    public float enemySpawnWaiterTime;

    private void Start()
    {
        control = true;
    }

    private void Update()
    {
        if (control)
        {
            control = false;
            StartCoroutine(enemySpawn());
        }
    }

    public IEnumerator enemySpawn()
    {
        //Debug.Log("x: "+ Random.Range(-23, 23)+ "y,:"+ Random.Range(-23, 23));

        Vector3 vec = new Vector3(Random.Range(-23, 23), 5, Random.Range(-23, 23));
        //Debug.Log(vec.ToString());
        PoolManager.Instance.UseObject(enemy[0], vec, Quaternion.identity);

        vec = new Vector3(Random.Range(-23, 23), 5, Random.Range(-23, 23));
        //Debug.Log(vec.ToString());
        PoolManager.Instance.UseObject(enemy[1], vec, Quaternion.identity);


        yield return new WaitForSeconds(enemySpawnWaiterTime);
        control = true;
    }


}
