
using System.Collections.Generic;

namespace Gemmob.Api.Analytics {
    public partial class ParameterBuilder {
        private readonly Dictionary<string, object> parameters = new Dictionary<string, object>();
        
        public static ParameterBuilder Create() {
            return new ParameterBuilder();//.Add("appversion", Application.version);
        }

        public static ParameterBuilder Create(string paraName, object paraValue) {
            return ParameterBuilder.Create().Add(paraName, paraValue);
        }

        public ParameterBuilder Add(string parameterName, object parameterValue) {
            if (!parameters.ContainsKey(parameterName)) {
                parameters.Add(parameterName, parameterValue);
            }

            return this;
        }

        public Dictionary<string, object> BuildDictObject() {
            return parameters;
        }

        public Dictionary<string, string> BuildDictString() {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (var item in parameters) {
                temp.Add(item.Key, item.Value.ToString());
            }
            return temp;
        }

    }
}
