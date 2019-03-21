using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    //Si alguien quiere saber que esto, digame y lo explico :PS (Pedro)
    [SerializeField]
    private int poolSize = 20;
    [SerializeField]
    private bool isGrowable = true;

    [SerializeField]
    private GameObject projectileBase;
    private List<GameObject> projectilePool;

    protected void Start()
    {
        //Initializing pool
        projectilePool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectileBase);
            obj.SetActive(false);
            projectilePool.Add(obj);
        }
    }

    public GameObject GetFromPool()
    {
        foreach(GameObject obj in projectilePool)
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        if(isGrowable)
        {
            GameObject obj = Instantiate(projectileBase);
            projectilePool.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        return null;
    }
}
