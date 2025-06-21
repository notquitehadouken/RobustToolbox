using System.Collections;
using System.Collections.Generic;
using Linguini.Syntax.Ast;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;

namespace Robust.Client.UserInterface.CustomControls;

public abstract class ContentCollection<T> : ICollection<Control>, IReadOnlyCollection<Control> where T : Control
{
    private readonly T Owner;

    public ContentCollection(T owner)
    {
        Owner = owner;
    }

    public void Add(Control item) => Owner.AddChild(item);

    public void Clear() => Owner.RemoveAllChildren();

    public bool Contains(Control item) => item?.Parent == Owner;

    public void CopyTo(Control[] array, int arrayIndex) => Owner.Children.CopyTo(array, arrayIndex);

    public bool Remove(Control item)
    {
        if (!Contains(item))
            return false;

        DebugTools.AssertNotNull(Owner);
        Owner!.RemoveChild(item);

        return true;
    }

    int ICollection<Control>.Count => Owner.ChildCount;
    int IReadOnlyCollection<Control>.Count => Owner.ChildCount;

    public bool IsReadOnly => false;

    public Enumerator GetEnumerator() => new(Owner);

    IEnumerator<Control> IEnumerable<Control>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : IEnumerator<Control>
    {
        private Control.OrderedChildCollection.Enumerator _enumerator;

        internal Enumerator(Control owner)
        {
            _enumerator = owner.Children.GetEnumerator();
        }

        public bool MoveNext() => _enumerator.MoveNext();

        public void Reset() => _enumerator.Reset();

        public void Dispose() => _enumerator.Dispose();

        public Control Current => _enumerator.Current;

        object IEnumerator.Current => Current;
    }
}
