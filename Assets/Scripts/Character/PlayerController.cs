using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	// Components
	private Rigidbody2D mRigidbody;
	//private Animator mAnimator;
	//private MusicManager mMusicManager;

	// Movements
	public float mJumpForce = 8f;
	private float distanceToGround;
	public float horizontalSpeed = 2.5f;
	private bool isFalling = false;
	private bool isInAir = false;
    private bool isOnGround = false;
	private float initialGravityScale = 0.0f;

    // Jump
    private bool leftTapJumpEnabled = false;
    private bool rightTapJumpEnabled = false;
	
	#region LifeCycle
	// Use this for initialization
	void Start () {
		mRigidbody = this.GetComponent<Rigidbody2D>();
		distanceToGround = this.GetComponent<BoxCollider2D>().bounds.max.y - mRigidbody.transform.position.y;
		//mAnimator = this.GetComponent<Animator>();
		//mMusicManager = GameObject.FindGameObjectWithTag("musicManager").GetComponent<MusicManager>();
		mRigidbody.velocity = new Vector2(horizontalSpeed, 0.0f);
	}
	
	void FixedUpdate() {
		isOnGround = isGrounded();

		checkPlayerJumpingAnimation();

		checkDeath();

        //Debug.Log(Time.timeSinceLevelLoad);
    }

	// Update is called once per frame
	void Update() {
		isOnGround = isGrounded();
		
		handleKeyBoardInput();
	}

	#endregion

	#region PlayerMovement
	void checkPlayerJumpingAnimation() {
		if (isInAir && mRigidbody.velocity.y < -0.1f) {
			isFalling = true;
			//mAnimator.SetBool("isFalling", isFalling);
		} else if (isFalling && isOnGround) {
			//mAnimator.SetTrigger("touchGround");
			mRigidbody.velocity.Set(mRigidbody.velocity.x, 0);
			isFalling = false;
			isInAir = false;
			//mAnimator.SetBool("isFalling", isFalling);
		}
	}

	public void handleKeyBoardInput () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			playerJump();
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			playerRestart();
		}

        if (leftTapJumpEnabled && Input.GetKeyDown(KeyCode.Z)) {
            playerJump();
        }

        if (rightTapJumpEnabled && Input.GetKeyDown(KeyCode.X)) {
            playerJump();
        }
	}

	public void setVerticalSpeedTowardsTarget(Vector3 targetPos) {
		float xDistance = Mathf.Abs(targetPos.x - transform.position.x);
		float yDistance = targetPos.y - transform.position.y;
		float time = xDistance / mRigidbody.velocity.x;
		mRigidbody.velocity = new Vector2(horizontalSpeed, yDistance / time);
		initialGravityScale = mRigidbody.gravityScale;
		mRigidbody.gravityScale = 0.0f;
	}

	public void setVerticalSpeedToZero() {
		mRigidbody.velocity = new Vector2(horizontalSpeed, 0.0f);
		mRigidbody.gravityScale = initialGravityScale;
	}

	public void playerJump() {
		if (isOnGround && !isInAir) {
			float jumpForce = mJumpForce;
			mRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			isInAir = true;
		}
	}

	public bool isGrounded() {
		RaycastHit2D hit = Physics2D.Raycast(mRigidbody.transform.position, Vector2.down, distanceToGround + 0.5f);
		return hit.collider != null;
	}

	#endregion

	#region PlayerLogic
	private void playerRestart() {
		death();
	}
	
	private void checkDeath() {
		if (mRigidbody.transform.position.y <= -100) {
			death();
		}
	}

	public void death() {
		enabled = false;
		//mMusicManager.PlayDeathSound();
		StartCoroutine(reloadAfterTime(0.1f));
	}

	 IEnumerator reloadAfterTime(float time) {
		 yield return new WaitForSeconds(time);
     	 Scene loadedLevel = SceneManager.GetActiveScene();
    	 SceneManager.LoadScene (loadedLevel.buildIndex);
		//  mMusicManager.PlayRespwanSound();
	}

    #endregion

    #region PlayerTap

    public void enableLeftTapJump() {
        leftTapJumpEnabled = true;
        Debug.Log("enableLeftTapJump");
    }

    public void disableLeftTapJump() {
        leftTapJumpEnabled = false;
        Debug.Log("disableLeftTapJump");
    }

    public void enableRightTapJump() {
        rightTapJumpEnabled = true;
        Debug.Log("enableRightTapJump");
    }

    public void disableRightTapJump() {
        rightTapJumpEnabled = false;
        Debug.Log("disableRightTapJump");
    }
    #endregion

}
