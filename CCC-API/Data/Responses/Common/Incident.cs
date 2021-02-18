using Newtonsoft.Json;

namespace CCC_API.Data.Responses.Common
{
    public class Incident
    {
        private readonly string _incidentId;
        private readonly string _message;
        private readonly string _errorCode;
        private readonly string _errorData;

        public string IncidentId => _incidentId;
        public string Message => _message;
        public string ErrorCode => _errorCode;
        public string ErrorData => _errorData;

        [JsonConstructor]
        public Incident(string incidentId, string message, string errorCode, string errorData)
        {
            _incidentId = incidentId;
            _message = message;
            _errorCode = errorCode;
            _errorData = errorData;
        }

        /// <summary>
        /// private constructor used to model concrete examples of the <see cref="Incident"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        private Incident(string message)
        {
            _message = message;
        }

        // common error responses from the application
        public static readonly Incident AUTHORIZATION_DENIED = new Incident("Authorization has been denied for this request.");
        public static readonly Incident UNAUTHORIZED_OPERATION = new Incident("Attempted to perform an unauthorized operation.");

        /// <summary>
        /// Override object.Equals()
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var incident = (Incident)obj;
            return (IncidentId == incident.IncidentId &&
                    Message == incident.Message &&
                    ErrorCode == incident.ErrorCode);
        }

        /// <summary>
        /// Override object.GetHashCode(). Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (IncidentId ?? string.Empty).GetHashCode() +
                       (Message ?? string.Empty).GetHashCode() +
                       (ErrorCode ?? string.Empty).GetHashCode();
            }
        }
    }
}
