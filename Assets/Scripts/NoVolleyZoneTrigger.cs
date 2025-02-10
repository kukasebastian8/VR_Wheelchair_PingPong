
using UnityEngine;

public class NoVolleyZoneTrigger : MonoBehaviour
{
    public GameObject volleyZoneUI;
    bool playerInZone = false;
    public bool _2v2AIscene = false;
    
    void Start()
    {
        volleyZoneUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = true;
            volleyZoneUI.SetActive(true);
            Debug.Log("Player is in zone");
        }
        else if (other.gameObject.CompareTag("AI"))
        {
            if (_2v2AIscene) {
                other.gameObject.GetComponent<_2v2Positiontargeter>().innovolley = true;
            }
            else
            {

                other.gameObject.GetComponent<positiontargeter>().innovolley = true;

            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInZone = false;
            volleyZoneUI.SetActive(false);
        }
        else if (other.gameObject.CompareTag("AI"))
        {
            if (_2v2AIscene)
            {
                other.gameObject.GetComponent<_2v2Positiontargeter>().innovolley = false;
            }
            else
            {

                other.gameObject.GetComponent<positiontargeter>().innovolley = false;

            }

        }
    }

    public bool GetVollyFlag()
    {
        return playerInZone;
    }
}
