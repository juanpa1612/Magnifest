using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTest : Photon.MonoBehaviour {

    private PhotonView PhotonView;
    

	/*private Vector3 LastPackedPosition;
	public double currentTime = 0.0;
	public double currentPacketTime = 0.0;
	public double lastPacketTime = 0.0;
	public double timeToReachGoal = 0.0;*/

	// Use this for initialization
	void Start () {
        PhotonView = GetComponent<PhotonView>();
	}
	
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        } else
        {
			/*currentTime = 0.0;
			LastPackedPosition = transform.position;
			lastPacketTime = currentTime;
			currentPacketTime = info.timestamp;*/

            TargetPosition = (Vector3)stream.ReceiveNext();
            TargetRotation = (Quaternion)stream.ReceiveNext();
        }
    }

	private Vector3 TargetPosition = Vector3.zero;
	private Quaternion TargetRotation = Quaternion.identity;

	// Update is called once per frame
	void Update () {
		if (!PhotonView.isMine)
        {
			//timeToReachGoal = currentPacketTime - lastPacketTime;
			//currentTime += Time.deltaTime;

			transform.position = Vector3.Lerp(transform.position, TargetPosition, 1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, 1f);
        }
	}
}
