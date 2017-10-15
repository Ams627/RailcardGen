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
    internal class RJISFileMapping
    {
        public string Filename { get; set; }
        public string Prefix { get; set; }
        public RJISFileMapping(string filename, string prefix)
        {
            Filename = filename;
            Prefix = prefix;
        }
    }
}
