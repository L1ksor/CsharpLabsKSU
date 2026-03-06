using System;
using System.Collections.Generic;

namespace CsharpControlWork1
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    class TestCollections<TKey, TValue>
    {
        private List<TKey> _listKeys;
        private List<TValue> _listValues;
        private Dictionary<TKey, TValue> _dictTKey;
        private Dictionary<string, TValue> _dictString;
        private GenerateElement<TKey, TValue> _generateElement;

        public TestCollections(int count, GenerateElement<TKey, TValue> generator)
        {
            _generateElement = generator;
            _listKeys = new List<TKey>();
            _listValues = new List<TValue>();
            _dictTKey = new Dictionary<TKey, TValue>();
            _dictString = new Dictionary<string, TValue>();

            for (int i = 0; i < count; i++)
            {
                KeyValuePair<TKey, TValue> pair = _generateElement(i);

                _listKeys.Add(pair.Key);
                _listValues.Add(pair.Value);
                _dictTKey.Add(pair.Key, pair.Value);
                _dictString.Add(pair.Key.ToString(), pair.Value);
            }
        }

        public (string Name, TKey Key)[] GetTestKeys()
        {
            if (_listKeys.Count == 0)
                throw new InvalidOperationException("Коллекция пуста");

            return new (string, TKey)[]
            {
                ("Первый", _listKeys[0]),
                ("Центральный", _listKeys[_listKeys.Count / 2]),
                ("Последний", _listKeys[_listKeys.Count - 1]),
                ("Несуществующий", _generateElement(_listKeys.Count + 1000).Key)
            };
        }

        public (string Name, TValue Value)[] GetTestValues()
        {
            return new (string, TValue)[]
            {
                ("Первое", _listValues[0]),
                ("Центральное", _listValues[_listValues.Count / 2]),
                ("Последнее", _listValues[_listValues.Count - 1]),
                ("Несуществующее", _generateElement(_listKeys.Count + 1000).Value)
            };
        }

        public bool ListKeysContains(TKey key) => _listKeys.Contains(key);

        public bool StringListContains(string key) => _listKeys.Select(k => k.ToString()).Contains(key);

        public bool DictTKeyContainsKey(TKey key) => _dictTKey.ContainsKey(key);

        public bool DictStringContainsKey(string key) => _dictString.ContainsKey(key);

        public bool DictTKeyContainsValue(TValue value) => _dictTKey.ContainsValue(value);

        public override string ToString()
        {
            return $"List<TKey> кол-во: {_listKeys.Count}\n" +
                   $"List<TValue> кол-во: {_listValues.Count}\n" +
                   $"Dictionary<TKey, TValue> кол-во: {_dictTKey.Count}\n" +
                   $"Dictionary<string, TValue> кол-во: {_dictString.Count}";
        }


    }
}