using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content;

public delegate void ContainerEventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e);
public delegate void ContainerEventHandler<TSender>(TSender sender, EventArgs e);

public class ContentContainer<TContent>
    : IList<TContent>, IReadOnlyList<TContent>, IContentController<TContent>, ICollection
    where TContent : ICodeContent<TContent>
{
    public event ContainerEventHandler<ContentContainer<TContent>, TContent>? OnAdded;
    public event ContainerEventHandler<ContentContainer<TContent>>? OnCleared;
    public event ContainerEventHandler<ContentContainer<TContent>, TContent>? OnRemoved;
    public event ContainerEventHandler<ContentContainer<TContent>, bool>? OnIsAllowedControlChanged;

    // TODO: Use Array
    private readonly List<TContent> _contents;

    bool _canOperate = true;
    public bool CanOperate
    {
        get => _canOperate;
        set
        {
            if (_canOperate != value)
            {
                _canOperate = value;
                OnIsAllowedControlChanged?.Invoke(this, value);
            }
        }
    }

    public ContentContainer()
    {
        _contents = new();
    }

    public int Count => _contents.Count;
    public bool IsReadOnly => false;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => ((ICollection)_contents).SyncRoot;

    public TContent this[int index]
    {
        get => _contents[index];
        set => _contents[index] = value;
    }

    public void Add(TContent item)
    {
        ThrowHelper.ThrowInvalidOperationExceptionIf(!CanOperate);
        _contents.Add(item);
        OnAdded?.Invoke(this, item);
    }

    public void Clear()
    {
        ThrowHelper.ThrowInvalidOperationExceptionIf(!CanOperate);
        _contents.Clear();
        OnCleared?.Invoke(this, EventArgs.Empty);
    }

    public bool Contains(TContent item)
        => _contents.Contains(item);

    public void CopyTo(TContent[] array, int arrayIndex)
        => _contents.CopyTo(array, arrayIndex);

    public bool Remove(TContent item)
    {
        ThrowHelper.ThrowInvalidOperationExceptionIf(!CanOperate);
        bool removed = _contents.Remove(item);
        if (removed)
        {
            OnRemoved?.Invoke(this, item);
        }
        return removed;
    }

    public IEnumerator<TContent> GetEnumerator()
        => _contents.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int IndexOf(TContent item)
    {
        return _contents.IndexOf(item);
    }

    public void Insert(int index, TContent item)
    {
        ThrowHelper.ThrowInvalidOperationExceptionIf(!CanOperate);
        _contents.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        ThrowHelper.ThrowInvalidOperationExceptionIf(!CanOperate);

        TContent item = _contents[index];
        _contents.RemoveAt(index);

        // TODO: Deleted item is not necessarily the same as `item`.
        OnRemoved?.Invoke(this, item);
    }

    void ICollection.CopyTo(Array array, int index) => ((ICollection)_contents).CopyTo(array, index);
}
