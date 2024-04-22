using Robust.Shared.GameObjects;

namespace Robust.Client.GameObjects
{
    internal interface IClientEntityManagerInternal : IClientEntityManager
    {
        // These methods are used by the Game State Manager.

        EntityUid CreateEntity(string? prototypeName, out MetaDataComponent metadata);

        new void InitializeEntity(EntityUid entity, MetaDataComponent? meta = null);

        new void StartEntity(EntityUid entity);
    }
}
