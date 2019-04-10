using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class viewObject : MonoBehaviour
{
    public Button butt;
    bool clickedOnce = false;
    string prevName;
    Vector3 prevLocation;
    public Text text; //dont forget to drag the child text from the button to the inspector!

    //To be honest i feel like we should move the info panel itself to a random location and the rssi score canvas to a random location so we can do a 360 view like the video
    //maybe do a new scene. if you want to access the info panel location do butt.gameObject.transform.parent.position... for the rssi score canvas its public Canvas canv then drag
    //the canvas on inspector and change the location and dont forget to save the previous positions if you do that. I can help you make a new button for that if you want to do this option 
    //to return back to normal


    //Another good idea is to do a new scene and see if you can just render the same object in a new scene to just get a 360 and then give a button to change it back... again i can help 
    //with this way too! just tell me what you think is best!


    public void ViewObject()
        {
        if(clickedOnce == false)
            {
            //Get name of object
            var nameOfObj = butt.gameObject.transform.parent.parent;             prevLocation = nameOfObj.transform.position;             //Move the objects position              float xCord = 0;             float yCord = 10;             float zCord = -20;             nameOfObj.transform.position = new Vector3(xCord, yCord, zCord);                             //Makes the object bigger             float xScale = nameOfObj.transform.localScale.x;             float yScale = nameOfObj.transform.localScale.y;             float zScale = nameOfObj.transform.localScale.z;             nameOfObj.localScale = new Vector3(xScale * 2, yScale * 2, zScale * 2);
            prevName = text.text;
            text.text = "Go Back";
            clickedOnce = true;
            }
        else
            {
            //Get name of object
            var nameOfObj = butt.gameObject.transform.parent.parent;                      //Move the objects position              nameOfObj.position = prevLocation;                             //Makes the object bigger             float xScale = nameOfObj.transform.localScale.x;             float yScale = nameOfObj.transform.localScale.y;             float zScale = nameOfObj.transform.localScale.z;             nameOfObj.localScale = new Vector3(xScale / 2, yScale / 2, zScale / 2);
            text.text = prevName;
            clickedOnce = false;
            }
           
        }
    }


    
