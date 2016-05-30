namespace Miharu.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 日付をキーとする辞書
    /// </summary>
    /// <typeparam name="TValue">格納するデータの型</typeparam>
    public class DailyCollection<TValue> : IDictionary<DateTime, TValue>
    {
        /// <summary>
        /// 実際に値を保存する辞書
        /// </summary>
        private Dictionary<int, TValue> dictionary;

        /// <summary>
        /// コレクションを初期化します。
        /// </summary>
        public DailyCollection()
        {
            this.dictionary = new Dictionary<int, TValue>();
        }

        /// <summary>
        /// キーの一覧を列挙します
        /// </summary>
        public ICollection<DateTime> Keys
        {
            get
            {
                var intkeys = this.dictionary.Keys;

                return intkeys.Map(DateHash.ToDateTime).ToList();
            }
        }

        /// <summary>
        /// 格納されている値の一覧
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return this.dictionary.Values;
            }
        }

        /// <summary>
        /// 格納されている要素の数を取得します。
        /// </summary>
        public int Count
        {
            get
            {
                return this.dictionary.Count;
            }
        }

        /// <summary>
        /// コレクションが読み取り専用かどうか
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// インデクサを用いて、値を設定、取得します
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>値</returns>
        public TValue this[DateTime key]
        {
            get
            {
                return this.dictionary[DateHash.ToHash(key)];
            }

            set
            {
                this.dictionary[DateHash.ToHash(key)] = value;
            }
        }

        /// <summary>
        /// コレクションに要素を追加します
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        public void Add(DateTime key, TValue value)
        {
            this.dictionary[DateHash.ToHash(key)] = value;
        }

        /// <summary>
        /// キーが含まれるかどうかを確認します
        /// </summary>
        /// <param name="key">確認するキー</param>
        /// <returns>含まれているかどうか</returns>
        public bool ContainsKey(DateTime key)
        {
            return this.dictionary.ContainsKey(DateHash.ToHash(key));
        }

        /// <summary>
        /// キーを指定して要素を削除します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>削除できたかどうか</returns>
        public bool Remove(DateTime key)
        {
            if (this.ContainsKey(key))
            {
                return this.dictionary.Remove(DateHash.ToHash(key));
            }

            return false;
        }

        /// <summary>
        /// 要素を取得できるか確認しながら取得します
        /// なかった場合は、何を返すべきなんだろうか？
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">値を戻す先</param>
        /// <returns>値を取得できたかどうか</returns>
        public bool TryGetValue(DateTime key, out TValue value)
        {
            if (this.ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        /// <summary>
        /// キーバリューのペアで値を設定します。
        /// </summary>
        /// <param name="item">キーバリューのペア</param>
        public void Add(KeyValuePair<DateTime, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// 格納されている要素を全て削除します
        /// </summary>
        public void Clear()
        {
            this.dictionary.Clear();
        }

        /// <summary>
        /// 指定したキーバリューのペアが含まれるか確認します。
        /// </summary>
        /// <param name="item">キーバリューのペア</param>
        /// <returns>含まれていれば true</returns>
        public bool Contains(KeyValuePair<DateTime, TValue> item)
        {
            if (this.ContainsKey(item.Key))
            {
                if (this[item.Key].Equals(item.Value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 多分、配列にコピーするんだと思うけど
        /// </summary>
        /// <param name="array">値を入れる先の配列</param>
        /// <param name="arrayIndex">配列の開始位置</param>
        public void CopyTo(KeyValuePair<DateTime, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException("何するメソッドなのかよくわからん");
        }

        /// <summary>
        /// コクレクションから指定されたキーバリューのペアを削除します
        /// </summary>
        /// <param name="item">キーバリューのペア</param>
        /// <returns>削除されたかどうか</returns>
        public bool Remove(KeyValuePair<DateTime, TValue> item)
        {
            if (this.Contains(item))
            {
                return this.Remove(item.Key);
            }

            return false;
        }

        /// <summary>
        /// KeyValuePair のイテレーターを取得します
        /// </summary>
        /// <returns>イテレーター</returns>
        public IEnumerator<KeyValuePair<DateTime, TValue>> GetEnumerator()
        {
            foreach (var kv in this.dictionary)
            {
                yield return new KeyValuePair<DateTime, TValue>(DateHash.ToDateTime(kv.Key), kv.Value);
            }
        }

        /// <summary>
        /// イテレーターを取得します
        /// </summary>
        /// <returns>イテレーター</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
