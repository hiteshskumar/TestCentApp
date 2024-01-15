using System;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using Blazored.LocalStorage;
using System.Text.RegularExpressions;

namespace ChargesWFM.UI.Services
{

    public interface IValidationService
    {
        bool _AlphaValidation(string txtvalue);
        bool _AlphaNumericValidation(string txtvalue);
        bool _Textboxlengthvalidation(string txtvalue, int txtmaxlength);
    }

    public class ValidationService : IValidationService
    {
        // private readonly ILocalStorageService _localService;
        // public ValidationService(ILocalStorageService localService)
        // {
        //     _localService = localService;
        // }


        public bool _AlphaValidation(string txtvalue)
        {
            string strRegex = @"/^[a-zA-Z]+$/";
            Regex regex = new Regex(strRegex);
            bool ReturnVal = true;
            if (!(regex.IsMatch(txtvalue)))
            {
                ReturnVal = false;
            }
            return ReturnVal;
        }

        public bool _AlphaNumericValidation(string txtvalue)
        {
            string strRegex = @"/^[a-zA-Z0-9]+$/i";
            Regex regex = new Regex(strRegex);
            bool ReturnVal = true;
            if (!(regex.IsMatch(txtvalue)))
            {
                ReturnVal = false;
            }
            return ReturnVal;
        }
        public bool _Textboxlengthvalidation(string txtvalue, int txtmaxlength)
        {
            int txtcurrentlength = 0;
            bool ReturnVal = true;
            if (!string.IsNullOrEmpty(txtvalue.Trim()))
            {
                txtcurrentlength = txtvalue.Trim().Length + 1;
                if (txtcurrentlength > txtmaxlength)
                {
                    ReturnVal = false;
                }
            }
            return ReturnVal;
        }
    }
}