using CCC_API.Data;
using CCC_API.Data.PostData.Media.Contact;
using CCC_API.Data.Responses.Email;
using CCC_API.Data.Responses.Media;
using CCC_API.Services.Media.Contact;
using CCC_API.Services.Media.Outlet;
using CCC_Infrastructure.Utils;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using Zukini;

namespace CCC_API.Services.Media
{
    public class EntityListService : AuthApiService
    {
        public const string EntityListUri = "entitylist";
        public const string EntityListFilterUri = "entitylist/filter";

        public enum EntityListTypes { Contact, Outlet };

        public EntityListService(string sessionKey) : base(sessionKey){}

        /// <summary>
        /// Gets entity lists by specified filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>List</returns>
        public List<EntityList> GetMediaLists(BaseEntityListFilterBody filter)
        {
            return FilterMediaLists(filter)?.Results;
        }

        /// <summary>
        ///  Gets entity lists by specified filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public EntityListFilter FilterMediaLists(BaseEntityListFilterBody filter)
        {
            return Request().Post()
                .ToEndPoint(EntityListFilterUri).Data(filter)
                .ExecCheck<EntityListFilter>();
        }
        
        /// <summary>
        /// Retrieves avaiable contact lists for email distribution.
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<BaseMedia> GetAvaliableMediaContactsLists()
        {
            return GetMediaLists(
                new BaseEntityListFilterBody
                {
                    EntityType = "MediaContact",
                    IncludePublicAndShared = true,
                    ExcludeBounceBackLists = true,
                    FilterType = "SystemAndUser",
                    UnlimitedResults = true
                })
                .Select(it => new BaseMedia { Id = it.Id, Name = it.Name, active = true, selected = true });
        }

        /// <summary>
        /// Retrieves avaiable outlet lists for email distribution.
        /// </summary>
        /// <returns>IEnumerable</returns>
        public IEnumerable<BaseMedia> GetAvaliableMediaOutletsLists()
        {
            return GetMediaLists(
                new BaseEntityListFilterBody
                {
                    EntityType = "MediaOutlet",
                    IncludePublicAndShared = true,
                    ExcludeBounceBackLists = true,
                    FilterType = "User",
                    UnlimitedResults = true
                })
                .Select(it => new BaseMedia { Id = it.Id, Name = it.Name, active = true, selected = true });
        }

        /// <summary>
        /// Deletes list by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IRestResponse DeleteList(int id) =>
            Request().Delete().ToEndPoint(EntityListUri + "/" + id).Exec();

        /// <summary>
        /// Deletes an entity list by name
        /// </summary>
        /// <param name="name">Name of the list to delete</param>
        public IRestResponse DeleteEntityListByName(string name, EntityListTypes listType)
        {
            int id;
            if (listType.Equals(EntityListTypes.Contact))
            {
                id = GetAvaliableMediaContactsLists().FirstOrError(l => l.Name.Equals(name), $"Name was not '{name}'").Id;
            }
            else
            {
                id = GetAvaliableMediaOutletsLists().FirstOrError(l => l.Name.Equals(name), $"Name was not '{name}'").Id;
            }
            return DeleteList(id);
        }

        /// <summary>
        /// Extacts recipients from a distribution.
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="bucket"></param>
        /// <returns></returns>
        public List<IMediaListItem> GetEmailDistributionRecipientListItems(EmailDist dist, PropertyBucket bucket = null)
        {
            var contactsService = new ContactsService(SessionKey);
            var outletsService  = new OutletsService(SessionKey);

            var contacts = dist.MediaContacts
                .SelectMany(
                    l => contactsService.FindContacts(ContactsService.ContactsSearchCriteria.List_Name, l.Name).Items)
                .ToList();
            bucket?.Remember(ContactsService.ContactsEndpoint, contacts, true);

            var outlets = dist.MediaOutlets
                .SelectMany(
                    l => outletsService.FindOutlets(OutletsService.OutletSearchCriteria.OutletListName, l.Name).Items)
                .ToList();

            bucket?.Remember(OutletsService.OutletsEndpoint, outlets, true);
            var total = new List<IMediaListItem>()
                .Concat(contacts)
                .Concat(outlets)
                .ToList();

            return total;
        }
        /// <summary>
        /// this method add - update - delete a note into a media list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public IRestResponse<EntityList> PatchNotes(int id, string note)
        {
            var patchBody = new PatchData[] { new PatchData("update", "/Supplement/Notes", note) };
            return Request().Patch().ToEndPoint($"{EntityListUri}/{id}").Data(patchBody).Exec<EntityList>();
        }
    }
}
