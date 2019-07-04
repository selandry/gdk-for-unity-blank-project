using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using UnityEngine;
using Snapshot = Improbable.Gdk.Core.Snapshot;

namespace BlankProject.Editor
{
    internal static class SnapshotGenerator
    {
        public struct Arguments
        {
            public string OutputPath;
        }

        public static void Generate(Arguments arguments)
        {
            Debug.Log("Generating snapshot.");
            var snapshot = CreateSnapshot();

            Debug.Log($"Writing snapshot to: {arguments.OutputPath}");
            snapshot.WriteToFile(arguments.OutputPath);
        }

        private static Snapshot CreateSnapshot()
        {
            var snapshot = new Snapshot();

            AddPlayerSpawner(snapshot);
            AddCube(snapshot);
            AddBall(snapshot);
            AddTankUnit(snapshot);
            return snapshot;
        }

        private static void AddPlayerSpawner(Snapshot snapshot)
        {
            var serverAttribute = UnityGameLogicConnector.WorkerType;

            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot(), serverAttribute);
            template.AddComponent(new Metadata.Snapshot { EntityType = "PlayerCreator" }, serverAttribute);
            template.AddComponent(new Persistence.Snapshot(), serverAttribute);
            template.AddComponent(new PlayerCreator.Snapshot(), serverAttribute);

            template.SetReadAccess(UnityClientConnector.WorkerType, UnityGameLogicConnector.WorkerType, MobileClientWorkerConnector.WorkerType);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, serverAttribute);

            snapshot.AddEntity(template);
        }

        private static void AddCube(Snapshot snapshot)
        {
            // Invoke our static function to create an entity template of our health pack with 100 heath.
            var cube = EntityTemplates.Cube(new Vector3f(-11, 6, 30));

            // Add the entity template to the snapshot.
            snapshot.AddEntity(cube);
        }

        private static void AddBall(Snapshot snapshot)
        {
            // Invoke our static function to create an entity template of our ball.
            var ball = EntityTemplates.Ball(new Vector3f(-5, 5, 25));

            // Add the entity template to the snapshot.
            snapshot.AddEntity(ball);

        }

        private static void AddTankUnit(Snapshot snapshot)
        {
            // Invoke our static function to create an entity template of our health pack with 100 heath.
            var tankUnit = EntityTemplates.TankUnit(new Vector3f(5, 5, 5), 110, 16);

            // Add the entity template to the snapshot.
            snapshot.AddEntity(tankUnit);
        }


    }
}
