// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#nullable enable
namespace LurkingNinja.RandomExtensions.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using RandomExtensions;

    public interface IReadOnlyWeightedList<T> : IWeightedCollection<T>, IReadOnlyList<WeightedValue<T>> {}

    public class WeightedList<T> : IReadOnlyWeightedList<T>, IList<WeightedValue<T>>
    {
        public sealed class ValueCollection : IReadOnlyList<T>
        {
            private readonly WeightedList<T> _list;

            public ValueCollection(WeightedList<T> list) => _list = list;

            public T this[int index] => _list[index].Value;
            public int Count => _list.Count;
            public bool IsReadOnly => true;

            public Enumerator GetEnumerator() => new(_list);

            IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public struct Enumerator : IEnumerator<T>
            {
                public int Offset;
                
                private readonly WeightedList<T> _list1;

                public Enumerator(WeightedList<T> list)
                {
                    _list1 = list;
                    Offset = 0;
                    Current = default!;
                }

                object? IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    if (Offset == _list1.Count) return false;
                    Current = _list1[Offset].Value;
                    Offset++;
                    return true;
                }

                public void Dispose() {}

                public void Reset() => throw new NotSupportedException();

                public T Current { get; private set; }
            }
        }

        public WeightedList(int capacity)
        {
            _list = new List<WeightedValue<T>>(capacity);
            Values = new ValueCollection(this);
        }

        public WeightedList()
        {
            _list = new List<WeightedValue<T>>();
            Values = new ValueCollection(this);
        }

        private readonly List<WeightedValue<T>> _list;

        internal double TotalWeight { get; private set; }

        public WeightedValue<T> this[int index]
        {
            get => _list[index];
            set
            {
                TotalWeight -= _list[index].Weight;
                _list[index] = value;
                TotalWeight += value.Weight;
            }
        }

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public ValueCollection Values { get; }

        public void Add(WeightedValue<T> item)
        {
            _list.Add(item);
            TotalWeight += item.Weight;
        }

        public void Add(T value, double weight) => Add(new WeightedValue<T>(value, weight));

        public void Clear() => _list.Clear();

        public bool Contains(WeightedValue<T> item) => _list.Contains(item);

        public bool Contains(T item) => IndexOf(item) != -1;

        public void CopyTo(WeightedValue<T>[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public Enumerator GetEnumerator() => new(this);

        public T GetItem(IRandom random)
        {
            if (_list.Count == 0) throw new InvalidOperationException("Empty list");

            var r = random.NextDouble() * TotalWeight;
            var current = 0.0;

            for (var i = 0; i < _list.Count; i++)
            {
                current += _list[i].Weight;
                if (r <= current)
                {
                    return _list[i].Value;
                }
            }

            return _list[^1].Value;
        }

        public void GetItems(IRandom random, Span<T> destination)
        {
            for (var i = 0; i < destination.Length; i++) destination[i] = GetItem(random);
        }

        public int IndexOf(WeightedValue<T> item) => _list.IndexOf(item);

        public int IndexOf(T item)
        {
            for (var i = 0; i < _list.Count; i++)
                if (EqualityComparer<T>.Default.Equals(_list[i].Value, item)) return i;

            return -1;
        }

        public void Insert(int index, WeightedValue<T> item)
        {
            _list.Insert(index, item);
            TotalWeight += item.Weight;
        }

        public void Insert(int index, T item, double weight) => Insert(index, new WeightedValue<T>(item, weight));

        public bool Remove(WeightedValue<T> item)
        {
            if (!_list.Remove(item)) return false;
            
            TotalWeight -= item.Weight;
            return true;

        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index == -1) return false;

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            var value = _list[index];
            _list.RemoveAt(index);
            TotalWeight -= value.Weight;
        }

        public void RemoveRandom(out T item) => RemoveRandom(RandomEx.Shared, out item);

        public void RemoveRandom(IRandom random, out T item)
        {
            if (_list.Count == 0) throw new InvalidOperationException("Empty list");

            var r = random.NextDouble() * TotalWeight;
            var current = 0.0;

            for (var i = 0; i < _list.Count; i++)
            {
                var wv = _list[i];
                current += wv.Weight;

                if (!(r <= current)) continue;
                
                item = wv.Value;
                _list.RemoveAt(i);
                TotalWeight -= wv.Weight;
                return;
            }

            var lastIndex = _list.Count - 1;
            var lastWv = _list[lastIndex];
            item = lastWv.Value;
            _list.RemoveAt(lastIndex);
            TotalWeight -= lastWv.Weight;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<WeightedValue<T>> IEnumerable<WeightedValue<T>>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<WeightedValue<T>>
        {
            private int _offset;
            private readonly WeightedList<T> _list;

            public Enumerator(WeightedList<T> list)
            {
                _list = list;
                _offset = 0;
                Current = default;
            }

            public WeightedValue<T> Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_offset == _list.Count) return false;
                Current = _list[_offset];
                _offset++;
                return true;
            }

            public void Dispose() {}

            public void Reset() => throw new NotSupportedException();
        }
    }
}