using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using System.Collections;
using Windows.Kinect;
using UnityEngine.UI;
using System.Linq;

public class CharacterSidewaysMovement : MonoBehaviour
{
	private KinectSensor _sensor;
    private BodyFrameReader _bodyFrameReader;
    private Body[] _bodies = null;
	
	public GameObject kinectAvailableText;
    public Text handXText;

    public bool IsAvailable;
    public float CharacterPosition;
	public bool IsJump;
	public int test;



    public static CharacterSidewaysMovement instance = null;

    public Body[] GetBodies()
    {
        return _bodies;
    }

    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20f;
    private CharacterController controller;
    private Animator anim;

    private bool isChangingLane = false;
    private Vector3 locationAfterChangingLane;

    //distance character will move sideways
    private Vector3 sidewaysMovementDistance = Vector3.right * 2f;

    public float SideWaysSpeed = 5.0f;

    public float JumpSpeed = 8.0f;
    public float Speed = 6.0f;
    //Max gameobject
    public Transform CharacterGO;
    
    IInputDetector inputDetector = null;
	
	void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            DestroyImmediate(gameObject);
    }

    // Use this for initialization
    void Start()
    {



		UIManager.Instance.ResetScore();
		UIManager.Instance.SetStatus(Constants.StatusTapToStart);

		GameManager.Instance.GameState = GameState.Start;

		anim = CharacterGO.GetComponent<Animator>();
		inputDetector = GetComponent<IInputDetector>();
		controller = GetComponent<CharacterController>();
		_sensor = KinectSensor.GetDefault();

		moveDirection = transform.forward;
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= Speed;

        if (_sensor != null)
        {
            IsAvailable = _sensor.IsAvailable;

            kinectAvailableText.SetActive(IsAvailable);
            
            _bodyFrameReader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
            }

            _bodies = new Body[_sensor.BodyFrameSource.BodyCount];
        }


       
    }

	static float RescalingToRangesB(float scaleAStart, float scaleAEnd, float scaleBStart, float scaleBEnd, float valueA)
	{
		return (((valueA - scaleAStart) * (scaleBEnd - scaleBStart)) / (scaleAEnd - scaleAStart)) + scaleBStart;
	}

    // Update is called once per frame
    void Update()
    {
		switch (GameManager.Instance.GameState)
		{
		case GameState.Start:
			if (Input.GetMouseButtonUp(0)) /* || JointType.Foot_Right*/
			{
				anim.SetBool(Constants.AnimationStarted, true);
				var instance = GameManager.Instance;
				instance.GameState = GameState.Playing;

				UIManager.Instance.SetStatus(string.Empty);
			}
			break;
		case GameState.Playing:
			UIManager.Instance.IncreaseScore(0.001f);

			CheckHeight();



			DetectJumpOrSwipeLeftRight();

			//apply gravity
			moveDirection.y -= gravity * Time.deltaTime;

			if (isChangingLane)
			{
				if (Mathf.Abs(transform.position.x - locationAfterChangingLane.x) < 0.1f)
				{
					isChangingLane = false;
					moveDirection.x = 0;
				}
			}

			//move the player
			controller.Move(moveDirection * Time.deltaTime);

			break;
		case GameState.Dead:
			anim.SetBool(Constants.AnimationStarted, false);
			if (Input.GetMouseButtonUp(0))
			{
				//restart
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
			break;
		default:
			break;
		}

	
	 if (_bodyFrameReader != null)
        {
            var frame = _bodyFrameReader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.GetAndRefreshBodyData(_bodies);

                foreach (var body in _bodies.Where(b => b.IsTracked))
                {
                    IsAvailable = true;

					if (body.HandRightConfidence == TrackingConfidence.High && body.HandRightState == HandState.Closed)
                    {
						SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else
                    {
                        CharacterPosition = RescalingToRangesB(-1, 1, -3, 5, body.Lean.X);
                        handXText.text = CharacterPosition.ToString();
						test = (int)CharacterPosition;
                    }


					if (body.HandLeftConfidence == TrackingConfidence.High && body.HandLeftState == HandState.Open)
					{
						moveDirection.y = JumpSpeed;
						anim.SetBool(Constants.AnimationJump, true);
					}
					else
					{
						anim.SetBool(Constants.AnimationJump, false);
					} 

					if (body.HandLeftConfidence == TrackingConfidence.High && body.HandLeftState == HandState.Closed)
					{
						anim.SetBool(Constants.AnimationStarted, true);
						var instance = GameManager.Instance;
						instance.GameState = GameState.Playing;

						UIManager.Instance.SetStatus(string.Empty);
					}

				
				}

                frame.Dispose();
                frame = null;
            }
        

		}
		
			

       

    }

    private void CheckHeight()
    {
        if (transform.position.y < -10)
        {
            GameManager.Instance.Die();
        }
    }

    private void DetectJumpOrSwipeLeftRight()
    {
		var inputDirection = inputDetector.DetectInputDirection(test);
        if (controller.isGrounded && inputDirection.HasValue && inputDirection == InputDirection.Top
            && !isChangingLane)
        {
            moveDirection.y = JumpSpeed;
            anim.SetBool(Constants.AnimationJump, true);
        }
        else
        {
            anim.SetBool(Constants.AnimationJump, false);
        }


        if (controller.isGrounded && inputDirection.HasValue && !isChangingLane)
        {
            isChangingLane = true;

            if (inputDirection == InputDirection.Left)
            {
                locationAfterChangingLane = transform.position - sidewaysMovementDistance;
                moveDirection.x = -SideWaysSpeed;
            }
            else if (inputDirection == InputDirection.Right)
            {
                locationAfterChangingLane = transform.position + sidewaysMovementDistance;
                moveDirection.x = SideWaysSpeed;
            }
        }


    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if we hit the left or right border
        if(hit.gameObject.tag == Constants.WidePathBorderTag)
        {
            isChangingLane = false;
            moveDirection.x = 0;
        }
    }

    

}
