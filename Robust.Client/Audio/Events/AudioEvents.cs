using Robust.Shared.GameObjects;
using Robust.Shared.Map;

namespace Robust.Client.Audio.Events;

public sealed class AudioEvents : EntitySystem
{
    [ByRefEvent]
    public readonly struct AudioPlayEntityEvent
    {

        public readonly EntityUid Entity;

        public AudioPlayEntityEvent(EntityUid entity)
        {
            Entity = entity;
        }
    }

    [ByRefEvent]
    public readonly struct AudioPlayCoordinatesEvent
    {

        public readonly EntityCoordinates Coordinates;

        public AudioPlayCoordinatesEvent(EntityCoordinates entity)
        {
            Coordinates = entity;
        }
    }
}
