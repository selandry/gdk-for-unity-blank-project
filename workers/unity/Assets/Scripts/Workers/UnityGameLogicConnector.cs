﻿using Improbable;
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Gdk.TransformSynchronization;
using Improbable.Gdk.GameObjectCreation;


namespace Game
{
    public class UnityGameLogicConnector : DefaultWorkerConnector
    {
        public const string WorkerType = "UnityGameLogic";
        
        private async void Start()
        {
            PlayerLifecycleConfig.CreatePlayerEntityTemplate = CreatePlayerEntityTemplate;
            await Connect(WorkerType, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            Worker.World.GetOrCreateSystem<MetricSendSystem>();
            PlayerLifecycleHelper.AddServerSystems(Worker.World);
            GameObjectCreationHelper.EnableStandardGameObjectCreation(Worker.World);
        }

        private static EntityTemplate CreatePlayerEntityTemplate(string workerId, byte[] serializedArguments)
        {
            var clientAttribute = EntityTemplate.GetWorkerAccessAttribute(workerId);
            var serverAttribute = WorkerType;

            var template = new EntityTemplate();
            template.AddComponent(new Position.Snapshot(), clientAttribute);
            template.AddComponent(new Metadata.Snapshot("Player"), serverAttribute);
            TransformSynchronizationHelper.AddTransformSynchronizationComponents(template, clientAttribute);
            PlayerLifecycleHelper.AddPlayerLifecycleComponents(template, workerId, serverAttribute);

            template.SetReadAccess(UnityClientConnector.WorkerType, MobileClientWorkerConnector.WorkerType, serverAttribute);
            template.SetComponentWriteAccess(EntityAcl.ComponentId, serverAttribute);

            return template;
        }
    }
}
