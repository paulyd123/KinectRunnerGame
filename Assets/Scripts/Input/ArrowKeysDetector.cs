using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using System.Collections;
using Windows.Kinect;
using UnityEngine.UI;
using System.Linq;

public class ArrowKeysDetector : MonoBehaviour, IInputDetector

{
	/*public float playerspeed = 6f;

    private Vector3 playerPos = new Vector3(0, -9.5f, 0);

    void Update()
    {

        float xPos = transform.position.x;

        if (CharacterSidewaysMovement.instance.IsAvailable)
        {
            xPos = CharacterSidewaysMovement.instance.CharacterPosition;

        }
        else
        {
            xPos = transform.position.x + (Input.GetAxis("Horizontal") * playerspeed);
        }
        
        playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -9.5f, 0f);

        transform.position = playerPos;
		
    }*/
    

	public InputDirection? DetectInputDirection(int test)
    {
		if (Input.GetKeyUp (KeyCode.UpArrow))
			return InputDirection.Top;
		else if (Input.GetKeyUp (KeyCode.DownArrow))
			return InputDirection.Bottom;
		else if (Input.GetKeyUp (KeyCode.RightArrow) || test > 0) {
			Debug.Log ("Right");
			return InputDirection.Right;
		} else if (Input.GetKeyUp (KeyCode.LeftArrow) || test < 0	) {
			Debug.Log ("Left");
			return InputDirection.Left;
		}
        else
            return null;
    } 
}