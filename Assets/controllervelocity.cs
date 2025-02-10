using UnityEngine;

public class controllervelocity : MonoBehaviour
{


    private Vector3 p1;
    private Vector3 v1;

    // Start is called before the first frame update
    void Start()
    {
        
       p1 =transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
       v1 = transform.position-p1;
       p1 = transform.position;
        
    }
    public Vector3 getvelocity() {
      return v1;
    }
 
}
