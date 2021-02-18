using System;
using CCC_API.Data.TestDataObjects;
using System.Collections.Generic;
using CCC_API.Utils.Assertion;
using CCC_Infrastructure.Utils;

namespace CCC_API.Data.Responses.Activities
{
    public class PublishActivitiesResponse
    {
        public List<PublishActivity> PublishActivities { get; set; }
        public List<string> AvailablePublishActivityTypes { get; set; }
        public List<string> AvailablePublishActivityPublicationStates { get; set; }
        public int UpperBound { get; set; }

        /// <summary>
        /// Finds activity by predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public PublishActivity SelectActivity(Predicate<PublishActivity> predicate) => PublishActivities.Find(predicate);

        /// <summary>
        /// Tries to find activity by predicate or throws error if not found.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public PublishActivity GetActivity(Predicate<PublishActivity> predicate)
        {
            var acc = SelectActivity(predicate);
            if (acc == null)
                throw new Exception(Err.Msg("Activity not found by given predicate"));
            return acc;
        }
    }
}
