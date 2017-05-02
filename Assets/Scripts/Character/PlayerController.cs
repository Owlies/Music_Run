using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	// Components
	private Rigidbody2D mRigidbody;
	private GameObject mWaveCollider;
	private Animator mAnimator;
	//private MusicManager mMusicManager;

	// Movements
	public float mJumpForce = 8f;
	private float distanceToGround;
	public float horizontalSpeed = 2.5f;
	private bool isFalling = false;
	private bool isInAir = false;
	
	#region LifeCycle
	// Use this for initialization
	void Start () {
		mRigidbody = this.GetComponent<Rigidbody2D>();
		distanceToGround = this.GetComponent<BoxCollider2D>().bounds.max.y - mRigidbody.transform.position.y;
		//mAnimator = this.GetComponent<Animator>();
		//mMusicManager = GameObject.FindGameObjectWithTag("musicManager").GetComponent<MusicManager>();
	}
	
	void FixedUpdate() {
		bool isOnGround = isGrounded();
		updatePlayerHorizontally();

		checkPlayerJumpingAnimation(isOnGround);

		checkDeath();

        //Debug.Log(Time.timeSinceLevelLoad);
	}

	// Update is called once per frame
	void Update() {
		bool isOnGround = isGrounded();
		
		handleKeyBoardInput(isOnGround);
	}

	#endregion

	#region PlayerMovement
	void checkPlayerJumpingAnimation(bool isOnGround) {
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

	void updatePlayerHorizontally() {
		mRigidbody.velocity = new Vector2(horizontalSpeed, mRigidbody.velocity.y);
	}

	public void handleKeyBoardInput (bool isOnGround) {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			playerJump(isOnGround);
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			playerRestart();
		}
	}

	public void playerJump(bool isOnGround) {
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

}
