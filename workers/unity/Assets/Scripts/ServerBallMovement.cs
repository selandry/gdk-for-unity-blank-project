using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.Subscriptions;
//using Improbable.Gdk.StandardTypes;
using Balls;
using Improbable.Gdk.Movement;


namespace BlankProject
{
    [WorkerType(WorkerUtils.UnityGameLogic)]
    public class ServerBallMovement : MonoBehaviour
    {
        [Require] private BallWriter ballWriter; //BallWriter healthPickupWriter;

        public float speed = 10.0f;
        public Text countText;

        private Rigidbody rb;
        private int count;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            count = 0;
            SetCountText();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddForce(movement * speed);


            Vector3 position = new Vector3(10, 10, 10);

            // Send the update using the new position.
            //var positionUpdate = new Position.Update { Coords = position.ToSpatialCoordinates() };
            //ballWriter.SendUpdate(positionUpdate);

        }

        // Destroy everything that enters the trigger
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                count++;
                SetCountText();
            }
        }

        void SetCountText()
        {
        //    countText.text = "Hello World !! : " + count.ToString();

        }
    }
}