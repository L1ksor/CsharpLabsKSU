
using System.Collections;

namespace Task2
{
    class DynamicArray<T> : IEnumerable<T>, IEnumerable where T : IEquatable<T>
    {
        private int _capacity;
        private int _count;
        private T[] _array;

        public int Length => _count;

        public int Capacity => _capacity;

        public int Count => _count;

        public DynamicArray()
        {
            _capacity = 8;
            _array = new T[_capacity];
            _count = 0;
        }

        public DynamicArray(int capacity)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(capacity);

            _capacity = capacity;
            _array = new T[_capacity];
            _count = 0;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            _array = collection.ToArray();
            _count = _array.Length;
            _capacity = _count;
        }

        public void Add(T item)
        {
            if (_count == 0)
            {
                _capacity = 1;
                _array = new T[_capacity];

            }

            if (_count >= _capacity)
            {
                int newCapacity = _capacity * 2;
                ExtensionCapacity(newCapacity);
            }

            _array[_count] = item;
            _count++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            T[] itemoAdd = collection.ToArray();
            int newItemsCount = itemoAdd.Length;

            if (newItemsCount == 0)
            {
                return;
            }

            int requiredCapacity = _count + newItemsCount;
            if (requiredCapacity > _capacity)
            {
                ExtensionCapacity(Math.Max(_capacity * 2, requiredCapacity));
            }

            Array.Copy(itemoAdd, 0, _array, _count, newItemsCount);
            _count += newItemsCount;
        }

        public bool Remove(T item)
        {
            int index = -1;

            for (int i = 0; i < _count; i++)
            {
                if (_array[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return false;
            }

            for (int i = index; i < _count - 1; i++)
            {
                _array[i] = _array[i + 1];
            }

            _array[_count - 1] = default(T);
            _count--;

            return true;
        }

        public bool Insert(T item, int index)
        {


            if (index < 0 || index > _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (_capacity == 0)
            {
                _capacity = 1;
                _array = new T[_capacity];

            }

            if (_count == _capacity)
            {
                ExtensionCapacity(_capacity * 2);
            }

            for (int i = _count; i > index; i--)
            {
                _array[i] = _array[i - 1];
            }

            _array[index] = item;
            _count++;

            return true;
        }

        public bool Equals(DynamicArray<T> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this._count != other._count) return false;

            for (int i = 0; i < _count; i++)
            {
                if (!this._array[i].Equals(other._array[i]))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DynamicArray<T> otherArray)
            {
                return Equals(otherArray);
            }

            if (obj is IEnumerable<T> otherCollection)
            {
                return EqualsCollection(otherCollection);
            }
            return false;
        }

        private bool EqualsCollection(IEnumerable<T> collection)
        {
            if (collection is null) return false;

            int index = 0;
            foreach (var item in collection)
            {
                if (index >= _count)
                {
                    return false;
                }

                if (!_array[index].Equals(item))
                {
                    return false;
                }

                index++;
            }

            return index == _count;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        private void ExtensionCapacity(int minCapacity)
        {
            if (minCapacity <= _capacity)
            {
                return;
            }

            T[] newArray = new T[minCapacity];

            Array.Copy(_array, newArray, _count);

            _array = newArray;
            _capacity = minCapacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                _array[index] = value;
            }
        }
    }
}