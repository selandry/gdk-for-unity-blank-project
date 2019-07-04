using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using Improbable.Gdk.Subscriptions;
using Improbable;

namespace Game
{

    public class TankClientMovement : MonoBehaviour
    {
        [Require] private TankWriter tankWriter;
        [Require] private PositionWriter tankPosition;

        [SerializeField] private uint fuel = 1;
        [SerializeField] private uint munition = 1;
        [SerializeField] private Vector3 position;

        // Start is called before the first frame update
        void Update()
        {

            //InvokeRepeating("UpdateFuel", 1.0f, 2.0f);
            fuel++;
            munition += 3;
            transform.Translate(Vector3.left * Time.deltaTime/3, Space.World);
            position = transform.position;

            var spatialPosition = new Position.Update
            {
                Coords = new Coordinates(position.x, position.y, position.z)
            };

            tankPosition.SendUpdate(spatialPosition);          //{ Coords = new Coordinates(position.x, position.y, position.z) });
            tankWriter.SendUpdate(new Tank.Update {FuelQuantity = fuel, MunitionQuantity = munition});
        }

        //// UpdateFuel is called every second
        //void UpdateFuel()
        //{
        //    tankWriter.SendUpdate(new Tank.Update { FuelQuantity = ++fuel, MunitionQuantity = 999});
        //    transform.Translate(Vector3.left * Time.deltaTime, Space.World);      
        //}
    }



}


