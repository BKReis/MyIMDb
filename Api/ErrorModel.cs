using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;

namespace Api {
    public class ErrorModel {
        public string Message { get; set; }
        public string Code { get; set; }
        public static ErrorModel Create(ErrorCodes code, params object[] detailArgs) {
        return new ErrorModel() {
            Code = code.ToString(),
            Message = formatMessage(code, detailArgs)
        };
        }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string formatMessage(ErrorCodes code, params object[] detailArgs) {

            try {
                var resourceType = typeof(Api.Resources.ErrorCodes);
                var rm = new ResourceManager(resourceType.FullName, resourceType.Assembly);
                var errorName = Enum.GetName(code.GetType(), code);
                var message = rm.GetString(errorName);
                if (detailArgs.Length > 0) {
                    return string.Format(message, detailArgs);
                }
                return message;
            }
            catch (Exception ex) {
                logger.Error(ex, "Error obtaining string for error code '{0}'", code);
                return code.ToString();
            }
        }
    }
}