using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTest : Photon.MonoBehaviour {

    private PhotonView PhotonView;
    private Vector3 TargetPosition;
    private Quaternion TargetRotation;

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
            TargetPosition = (Vector3)stream.ReceiveNext();
            TargetRotation = (Quaternion)stream.ReceiveNext();

        }
    }

	// Update is called once per frame
	void Update () {
		if (!PhotonView.isMine)
        {
            SmoothMove();
        }
	}

   

    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime*5);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Time.deltaTime*5);

    }
}
