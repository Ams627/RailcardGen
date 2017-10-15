using System.Collections.Generic;
using System.Linq;

namespace RailcardGen
{
    class DictUtils
    {
        public static void AddEntry<T, U>(Dictionary<T, List<U>> d, T key, U listEntry)
        {
            if (!d.TryGetValue(key, out var list))
            {
                list = new List<U>();
                d.Add(key, list);
            }
            list.Add(listEntry);
        }

        public static void AddDoubleDictEntry<K1, K2, V>(Dictionary<K1, Dictionary<K2, V>> d, K1 key1, K2 key2, V innerDictionaryEntry)
        {
            if (!d.TryGetValue(key1, out var innerDict))
            {
                innerDict = new Dictionary<K2, V>();
                d.Add(key1, innerDict);
            }
            innerDict.Add(key2, innerDictionaryEntry);
        }





        public static void AddEntry<T, U>(Dictionary<T, HashSet<U>> d, T key, U listEntry)
        {
            if (!d.TryGetValue(key, out var list))
            {
                list = new HashSet<U>();
                d.Add(key, list);
            }
            list.Add(listEntry);
        }

        public static IEnumerable<string> GetCollectionOrEmpty(Dictionary<string, List<string>> dict, string key)
        {
            dict.TryGetValue(key, out var result);
            return result ?? Enumerable.Empty<string>();
        }

        public static IEnumerable<string> GetCollectionOrEmpty(Dictionary<string, HashSet<string>> dict, string key)
        {
            dict.TryGetValue(key, out var result);
            return result ?? Enumerable.Empty<string>();
        }


        public static string GetResult(Dictionary<string, string> dict, string key)
        {
            dict.TryGetValue(key, out var result);
            return result ?? string.Empty;
        }

    }
}
