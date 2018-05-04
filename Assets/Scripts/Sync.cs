using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : Photon.PunBehaviour
{
    Vector3 trueLoc;
    Vector3 originLoc;
    Quaternion trueRot;
    PhotonView pv;
    float timeSync;
    float startTime;
    float timeDifference;
    float syncPercentage;
    void Start ()
    {
        timeSync = 0.5f;
        pv = GetComponent<PhotonView>();
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //we are reicieving data
        if (stream.isReading)
        {
            //receive the next data from the stream and set it to the truLoc varible
            if (!pv.isMine)
            {//do we own this photonView?????
                this.trueLoc = (Vector3)stream.ReceiveNext(); //the stream send data types of "object" we must typecast the data into a Vector3 format
                this.originLoc = transform.position;
                this.startTime = Time.time;
            }
        }
        //we need to send our data
        else
        {
            //send our posistion in the data stream
            if (pv.isMine)
            {
                stream.SendNext(transform.position);
            }
        }
    }

    void Update()
    {
        if (!pv.isMine)
        {
          
                timeDifference = Time.time - startTime;
                syncPercentage = timeDifference / timeSync;
                transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime);
            

            
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime);
        }

    }
}
