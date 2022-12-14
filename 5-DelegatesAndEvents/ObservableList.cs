namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private readonly IList<TItem> _list = new List<TItem>();

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
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _list.Add(item);
            ElementInserted?.Invoke(this, item, Count - 1);
        }
        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear() {
            var clone = new List<TItem>(_list);
            _list.Clear();
            for (int i = 0; i < clone.Count; i++)
            {
                this.ElementRemoved?.Invoke(this, clone[i], i);
            }
        }

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
            return _list.Equals(olist._list);
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
        public override int GetHashCode() => _list.GetHashCode();

        /// <inheritdoc cref="object.ToString" />
        public override string ToString() => _list.ToString();
        
    }
}
