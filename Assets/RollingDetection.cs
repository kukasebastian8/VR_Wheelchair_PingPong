using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollingDetection : MonoBehaviour
{
    private float tempPosit = -5;
    private bool identicellasttime = false;
    private float temptnum;
    private bool checktime =true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checktime)
        if (transform.position.y < 0.5f) {
            temptnum = tempPosit - transform.position.y;
            if (temptnum < 0.001)
            {
                if (temptnum > -0.001) { 
                    if (identicellasttime)
                    {
                        if (gameObject.GetComponent<ballcollision>().turretball)
                        {
                            Destroy(gameObject);
                        }
                        else
                        {
                            Debug.Log("reloading scene because ball rolled");
                            string currentSceneName = SceneManager.GetActiveScene().name;
                            SceneManager.LoadScene(currentSceneName);
                        }
                    }
                identicellasttime = true;
                }
                 else
                    {
                identicellasttime = false;
                 }
            }
            else { 
            identicellasttime=false;
            }
            tempPosit = transform.position.y;
            checktime = false;
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        checktime = true;
    }
    }
