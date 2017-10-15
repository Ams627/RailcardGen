using System;
using System.Reflection;

// Vente ID_RAILCARD_NUMERO_VERSION ( 6 ; 6257 ) uint 9
// Vente ID_RAILCARD_IAP ( 6 ; 6258 ) string "TVM01"
// Vente ID_RAILCARD_RAILCARD ( 6 ; 6259 ) array struct
// [
//     Vente ID_RAILCARD_RAILCARD ( 6 ; 6259 ) struct
//     {
//         Vente ID_RAILCARD_CODE ( 6 ; 6260 ) string "YNG" -- s1-3
//         Vente ID_RAILCARD_END_DATE ( 6 ; 6261 ) date 15/04/2047 02:42:07 d4-11
//         Vente ID_RAILCARD_START_DATE ( 6 ; 6262 ) date 05/01/2009 00:00:00 d12-19
//         Vente ID_RAILCARD_QUOTE_DATE ( 6 ; 6263 ) date 05/08/1997 00:00:00 d12-27
//         Vente ID_RAILCARD_HOLDER_TYPE ( 6 ; 6264 ) string "A"              c28
//         Vente ID_RAILCARD_DESCRIPTION ( 6 ; 6265 ) string "16-25 RAILCARD" s29-48
//         Vente ID_RAILCARD_RESTRICTED_BY_ISSUE ( 6 ; 6266 ) uint 1          YN 49-49
//         Vente ID_RAILCARD_RESTRICTED_BY_AREA ( 6 ; 6267 ) uint 0           YN 50-50
//         Vente ID_RAILCARD_RESTRICTED_BY_TRAIN ( 6 ; 6268 ) uint 1          YN 51-51
//         Vente ID_RAILCARD_RESTRICTED_BY_DATE ( 6 ; 6269 ) uint 0           YN 52-52
//         Vente ID_RAILCARD_MASTER_CODE ( 6 ; 6270 ) string "YNG"            s53-55
//         Vente ID_RAILCARD_DISPLAY_FLAG ( 6 ; 6271 ) uint 1                 c56  
//         Vente ID_RAILCARD_MAX_PASSENGERS ( 6 ; 6272 ) uint 8               n
//         Vente ID_RAILCARD_MIN_PASSENGERS ( 6 ; 6273 ) uint 1
//         Vente ID_RAILCARD_MAX_HOLDERS ( 6 ; 6274 ) uint 1
//         Vente ID_RAILCARD_MIN_HOLDERS ( 6 ; 6275 ) uint 1
//         Vente ID_RAILCARD_MAX_ACC_ADULTS ( 6 ; 6276 ) uint 0
//         Vente ID_RAILCARD_MIN_ACC_ADULTS ( 6 ; 6277 ) uint 0
//         Vente ID_RAILCARD_MAX_ADULTS ( 6 ; 6278 ) uint 8
//         Vente ID_RAILCARD_MIN_ADULTS ( 6 ; 6279 ) uint 1
//         Vente ID_RAILCARD_MAX_CHILDREN ( 6 ; 6280 ) uint 8
//         Vente ID_RAILCARD_MIN_CHILDREN ( 6 ; 6281 ) uint 0
//         Vente ID_RAILCARD_DISCOUNTED_PRICE ( 6 ; 6283 ) uint 0
//         Vente ID_RAILCARD_VAL_PERIOD_DAYS ( 6 ; 6290 ) uint 0
//         Vente ID_RAILCARD_VAL_PERIOD_MONTHS ( 6 ; 6284 ) uint 12
//         Vente ID_RAILCARD_LAST_VALID_DATE ( 6 ; 6285 ) date 01/01/1970 01:00:00
//         Vente ID_RAILCARD_CAPRI_TICKET_TYPE ( 6 ; 6286 ) string "ZMA"
//         Vente ID_RAILCARD_ADULT_STATUS ( 6 ; 6287 ) string "003"
//         Vente ID_RAILCARD_CHILD_STATUS ( 6 ; 6288 ) string "001"
//         Vente ID_RAILCARD_AAA_STATUS ( 6 ; 6289 ) string "002"
//     }


namespace RailcardGen
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    class RJISReadAll
    {
        Dictionary<string, Dictionary<LinePrefix, System.Type>> _rjisMasterDictionary = new Dictionary<string, Dictionary<LinePrefix, Type>>();
        Dictionary<System.Type, List<RJISAttribute>> _rjisCreator = new Dictionary<Type, List<RJISAttribute>>();

        public RJISReadAll()
        {
            var thisAssembly = System.Reflection.Assembly.GetCallingAssembly();
            var rjisTypes = thisAssembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IRJISReadable)));

            // loop over types that have the RJISReadable interface:
            foreach (var type in rjisTypes)
            {
                // does the type have an RJISReadInfoAttribute indicating filename extension, prefix and offset - if so, store this information
                // in the double dictionary declared earlier. Extensions map to a list of prefixes which map. Prefixes map one-to-one to type.
                var rjisReadInfoAttribute = (RJISReadInfoAttribute)Attribute.GetCustomAttribute(type, typeof(RJISReadInfoAttribute));
                if (rjisReadInfoAttribute != null)
                {
                    DictUtils.AddDoubleDictEntry(
                        _rjisMasterDictionary,
                        rjisReadInfoAttribute.Extension,
                        new LinePrefix { Prefix = rjisReadInfoAttribute.Prefix, Offset = rjisReadInfoAttribute.PrefixOffset },
                        type
                        );

                    // get a list of public properties that have RJIS attributes:
                    var lineTypes = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(x => Attribute.GetCustomAttribute(x, typeof(RJISAttribute)) != null);

                    // create a list of types and start/end columns:
                    var rjisAttributeList = lineTypes.Select(x => (RJISAttribute)Attribute.GetCustomAttribute(x, typeof(RJISAttribute))).ToList();

                    _rjisCreator.Add(type, rjisAttributeList);

                    var previousEndColumn = 0;
                    var lineLength = 0;
                    foreach (var element in rjisAttributeList)
                    {
                        if (element.StartColumn <= previousEndColumn)
                        {
                            throw new Exception($"Overlapping RJIS element: columns {element.StartColumn} to {element.EndColumn}");
                        }
                        if (element.EndColumn <= element.StartColumn)
                        {
                            throw new Exception($"Zero or negative length RJIS element: columns {element.StartColumn} to {element.EndColumn}");
                        }
                        lineLength = lineLength + 1 + element.EndColumn - element.StartColumn;
                    }
                }
            }

        }

        IRJISReadable ProcessLine(string line, System.Type type)
        {
            return null;
        }

        void Read()
        {
            var filename = "s:/rjfaf628.";
            foreach (var rjisFile in _rjisMasterDictionary)
            {
                var extension = rjisFile.Key;
                var fullname = Path.Combine(filename, extension);

                var allLists = new Dictionary<LinePrefix, List<IRJISReadable>>();
                foreach (var type in rjisFile.Value)
                {
                    var prefix = type.Key;
                    var t = type.Value;
                    allLists.Add(prefix, (List<IRJISReadable>)Activator.CreateInstance(typeof(List<>).MakeGenericType(t)));
                }

                var offset = allLists.First().Key.Offset;
                var prefixLength = allLists.First().Key.Prefix.Length;


                foreach (var line in File.ReadLines(fullname).Where(x => x.Length > 0))
                {
                    if (line[0] != '/')
                    {
                        var prefix = line.Substring(offset, prefixLength);
                        var type = _rjisMasterDictionary[extension][new LinePrefix { Prefix = prefix, Offset = offset }];
                        ProcessLine(line, type);
                    }
                }
            }
        }

    }
}
