using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDropObj : MonoBehaviour
{
    public GameObject myHands; //reference to your hands/the position where you want your object to go
    public Vector3 offset;
    public float putInFront;
    Vector3 direction;
    bool canpickup; //a bool to see if you can or cant pick up the item
    GameObject ObjectIwantToPickUp; // the gameobject onwhich you collided with
    bool hasItem; // a bool to see if you have an item in your hand
    // Start is called before the first frame update
    void Start()
    {
        canpickup = false;    //setting both to false
        hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.forward);
        if (canpickup == true) // if you enter thecollider of the objecct
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                ObjectIwantToPickUp.GetComponent<Rigidbody>().isKinematic = true;   //makes the rigidbody not be acted upon by forces
                ObjectIwantToPickUp.transform.position = myHands.transform.position + offset*(transform.childCount + 1); // sets the position of the object to your hand position
                ObjectIwantToPickUp.transform.parent = myHands.transform; //makes the object become a child of the parent so that it moves with the hands
                canpickup=false;
                //hasItem = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.K) && hasItem == true) // if you have an item and get the key to remove the object
        {
            gameObject.transform.GetChild(transform.childCount-1).gameObject.GetComponent<Rigidbody>().isKinematic = false; // make the rigidbody work again
            gameObject.transform.GetChild(transform.childCount-1).gameObject.transform.position =  gameObject.transform.GetChild(transform.childCount-1).gameObject.transform.position - offset*(transform.childCount) + putInFront*transform.forward;
            gameObject.transform.GetChild(transform.childCount-1).gameObject.transform.parent = null; // make the object no be a child of the hands
            //hasItem = false;
        }
        if(transform.childCount != 0)
        {
            hasItem = true;
        }
        else
        {
            hasItem = false;
        }
        
    }
    private void OnTriggerStay(Collider other) // to see when the player enters the collider
    {
        if (other.gameObject.tag == "Object") //on the object you want to pick up set the tag to be anything, in this case "object"
        {
            canpickup = true;  //set the pick up bool to true
            ObjectIwantToPickUp = other.gameObject; //set the gameobject you collided with to one you can reference
            Debug.Log(ObjectIwantToPickUp.name);
            canpickup=true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //canpickup = false; //when you leave the collider set the canpickup bool to false

    }
}