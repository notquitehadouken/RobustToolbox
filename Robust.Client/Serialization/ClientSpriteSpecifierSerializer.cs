using Robust.Client.ResourceManagement;
using Robust.Shared.IoC;
using Robust.Shared.Serialization.Manager;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.Serialization.Markdown.Mapping;
using Robust.Shared.Serialization.Markdown.Validation;
using Robust.Shared.Serialization.Markdown.Value;
using Robust.Shared.Serialization.TypeSerializers.Implementations;

namespace Robust.Client.Serialization;

[TypeSerializer]
public sealed class ClientSpriteSpecifierSerializer : SpriteSpecifierSerializer
{
    public override ValidationNode ValidateRsi(ISerializationManager serializationManager,
            MappingDataNode node,
            IDependencyCollection dependencies,
            ISerializationContext? context)
    {
        if (!node.TryGet("sprite", out var pathNode) || pathNode is not ValueDataNode valuePathNode)
        {
            return new ErrorNode(node, "Sprite specifier has missing/invalid sprite node");
        }

        if (!node.TryGet("state", out var stateNode) || stateNode is not ValueDataNode valueStateNode)
        {
            return new ErrorNode(node, "Sprite specifier has missing/invalid state node");
        }

        var res = dependencies.Resolve<IClientResourceCache>();
        var rsiPath = TextureRoot / valuePathNode.Value;
        if (!res.TryGetResource(rsiPath, out RSIResource? resource))
        {
            return new ErrorNode(node, "Failed to load RSI");
        }

        if (!resource.RSI.TryGetState(valueStateNode.Value, out _))
        {
            return new ErrorNode(node, "Invalid RSI state");
        }

        return new ValidatedMappingNode(new()
        {
            { new ValidatedValueNode(new ValueDataNode("sprite")), new ValidatedValueNode(pathNode)},
            { new ValidatedValueNode(new ValueDataNode("state")), new ValidatedValueNode(valueStateNode)},
        });
    }
}
