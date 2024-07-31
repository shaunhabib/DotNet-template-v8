using Core.Application.Extensions;
using Core.Domain.Persistence.SharedModels.General;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Core.Application.Services
{
    public class CommonService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<CommonService> _logger;
        private List<string> _validationError;
        public CommonService(IDateTimeService dateTimeService, ILogger<CommonService> logger)
        {
            _validationError = new List<string>();
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        

        public string GenerateCodeWithoutYear(string existingNumber, string prefix, string numberFormat)
        {
            try
            {
                string Number;
                if (existingNumber == null)
                {
                    Number = prefix + "-" + 1.ToString(numberFormat);
                }
                else
                {
                    string[] arr = existingNumber.Split("-");
                    Number = arr[0];
                    var receiptSerial = Convert.ToInt16(arr[1]) + 1;
                    if (receiptSerial.ToString().Length > 6)
                        Number = prefix + "-" + receiptSerial.ToString();
                    else
                        Number = prefix + "-" + receiptSerial.ToString(numberFormat);
                }
                return Number;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return null;
            }
        }

        public string GenerateCodeWithoutYear(string existingNumber, string prefix, string numberFormat, string separator)
        {
            try
            {
                if (string.IsNullOrEmpty(separator)) separator = "-";

                string Number;
                if (existingNumber == null)
                {
                    Number = prefix + separator + 1.ToString(numberFormat);
                }
                else
                {
                    string[] arr = existingNumber.Split(separator);
                    Number = arr[0];
                    var receiptSerial = Convert.ToInt16(arr[1]) + 1;
                    if (receiptSerial.ToString().Length > 6)
                        Number = prefix + separator + receiptSerial.ToString();
                    else
                        Number = prefix + separator + receiptSerial.ToString(numberFormat);
                }
                return Number;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return null;
            }
        }

        public string GenerateCodeWithYear(string existingNumber, string prefix, string numberFormat)
        {
            try
            {
                string Number;
                string currentYear = DateTime.Parse(_dateTimeService.Now.ToString(CultureInfo.InvariantCulture)).Year.ToString();

                if (existingNumber == null)
                    Number = prefix + "-" + currentYear + "-" + 1.ToString(numberFormat);

                else
                {
                    string[] arr = existingNumber.Split("-");
                    Number = arr[0];

                    if (arr[1].Equals(currentYear))
                        Number = prefix + "-" + arr[1] + "-" + (int.Parse(arr[2]) + 1).ToString(numberFormat);
                    else
                        Number = prefix + "-" + currentYear + "-" + 1.ToString(numberFormat);
                }
                return Number;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return null;
            }
        }

        public string GenerateCodeWithYear(string existingNumber, string prefix, string numberFormat, string separator)
        {
            try
            {
                if (string.IsNullOrEmpty(separator)) separator = "-";

                string Number;
                var year = DateTime.Parse(_dateTimeService.Now.ToString(CultureInfo.InvariantCulture)).Year.ToString();

                if (existingNumber == null)
                {
                    Number = prefix + separator + year + separator + 1.ToString(numberFormat);
                }
                else
                {
                    string[] arr = existingNumber.Split(separator);
                    Number = arr[0];
                    if (arr[1].Equals(year))
                        Number = prefix + separator + arr[1] + separator + (int.Parse(arr[2]) + 1).ToString(numberFormat);
                    else
                        Number = prefix + separator + year + separator + 1.ToString(numberFormat);
                }
                return Number;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetFullMessage());
                _validationError.Add(ex.GetFullMessage());
                return null;
            }
        }
    }
}
