using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Exceptions {
    public class ErrorModelException : Exception {
        public ErrorModel ErrorModel { get; set; }
        public ErrorModelException(ErrorCodes code, params object[] detailArgs) : this(ErrorModel.Create(code, detailArgs)) {
        }
        public ErrorModelException(ErrorModel error) : base(string.Format("Application error: {0} - {1}", error.Code, error.Message)) {
            ErrorModel = error;
        }
    }
}
