using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace TrainpainHelper
{
    public class Trainpain
    {
        public static ValidateReceivedDataResult ValidateReceivedData(GetUserDataResult result, List<string> keys)
        {
            Dictionary<string, UserDataRecord> allData = result.Data;
            List<string> missingKeys = new List<string>();

            foreach (var key in keys)
            {
                if (!allData.ContainsKey(key))
                {
                    missingKeys.Add(key);
                }
            }

            ValidateReceivedDataResult missingResult = new ValidateReceivedDataResult();
            missingResult.missingKeys = missingKeys;
            if (missingKeys.Count == 0)
            {
                missingResult.IsMissingKeys = false;
            }
            else
            {
                missingResult.IsMissingKeys = true;
            }
                
            return missingResult;
        }

        public class ValidateReceivedDataResult
        {
            public List<string> missingKeys;
            public bool IsMissingKeys;
        }
    }

}
