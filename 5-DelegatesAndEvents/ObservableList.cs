namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private IList<TItem> _list = new List<TItem>();

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count
        {
            get => _list.Count;
        }

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly
        {
            get => _list.IsReadOnly;
        }

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => _list[index];
            set {
                TItem oldValue = _list[index];
                _list[index] = value;
                ElementChanged?.Invoke(this, value, oldValue, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() =>_list.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _list.Add(item);
            ElementInserted?.Invoke(this, item, Count - 1);
        }
        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear() => _list.Clear();

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item) =>_list.Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            if (Contains(item)) {
                int index = IndexOf(item); 
                ElementRemoved?.Invoke(this, item, index);
                return _list.Remove(item);
            }
            return false;
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item) => _list.IndexOf(item);

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            _list.Insert(index, item);
            ElementInserted?.Invoke(this, item, index);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            if (index < Count) {
                TItem item = this[index];
                _list.RemoveAt(index);
                ElementRemoved?.Invoke(this, item, index);
            }
        }

        public bool Equals(ObservableList<TItem> olist)
        {
            if (Count != olist.Count)
            {
                return false;
            }
            for (int i = 0; i < Count; i++)
            {
                if (!this[i].Equals(olist[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is ObservableList<TItem>)
            {
                return Equals(obj as ObservableList<TItem>);
            }
            return false;
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode() => HashCode.Combine(_list);

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            return _list.ToString();
        }
    }
}
