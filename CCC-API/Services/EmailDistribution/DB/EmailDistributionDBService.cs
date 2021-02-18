using CCC_API.Data.Responses.Email;
using CCC_API.Data.Responses.Media;
using CCC_API.Data.Responses.Media.Contact;
using CCC_API.Data.Responses.Media.Outlet;
using CCC_API.Services.Common.db;
using CCC_API.Services.Common.Support;
using CCC_Infrastructure.Utils;
using Dapper;
using System;
using System.Data.SqlClient;

namespace CCC_API.Services.EmailDistribution.DB
{
    /// <summary>
    /// Email distribution related db interaction service.
    /// </summary>
    public class EmailDistributionDbService : BaseDbService
    {
        public EmailDistributionDbService(int companyId) 
            : base(new CompanyService().GetConnectionToCompanyDb(companyId)){}

        public EmailDistributionDbService(SqlConnection connection) : base(connection) {}

        public EmailDistributionDbService(string companyName) 
            : base(new CompanyService().GetConnectionToCompanyDbByName(companyName)) {}


        /// <summary>
        /// Performs common checkings and returns sql recient type string.
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GenerateRecipientTypeId(EmailDistDetails dist, IMediaListItem item)
        {
            if (dist == null) throw new ArgumentException(Err.Msg("Wrong null distribution parameter"));
            if (dist.DistributionId <= 0) throw new ArgumentException(Err.Msg("Wrong distribution id parameter"));
            if (item == null) throw new ArgumentException(Err.Msg("Specify correct recipient"));

            var payload = $"{item.Id}";
            if (item is ContactsItem) return "MediaContactID = " + payload;
            if (item is OutletsItem) return "MediaOutletID  = " + payload;

            throw new NotImplementedException(Err.Msg($"Please add implementation for {item.GetType()}"));
        }

        /// <summary>
        /// Inserts open for a recipients.
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="item"></param>
        /// <returns>true</returns> if succesfull
        public bool OpenEmail(EmailDistDetails dist, IMediaListItem item, int opened)
        {
            var payload = GenerateRecipientTypeId(dist, item);
            var sqlCommand =
                $@"
                BEGIN
	            -- variables
	            DECLARE @DistID int,               
	                    @ActivityID int,          @DistActionID int, 
	                    @ActTypeName varchar(50), @ActionName varchar(50), @campaigns varchar(50),
                        @OpenedCount int;
	            SET @DistID         = {dist.DistributionId};
                SET @OpenedCount    = {opened};
	            SET @ActTypeName    = 'E-mail Campaign'
	            SET @ActionName     = 'Action=Open/Read Campaign'
                SET @campaigns       = 'Open/Read Campaign'
	            EXEC @ActivityID    = [dbo].[UniqueID_GetNextID] 'Activity', 'ActivityID'
	            SET @DistActionID   = (SELECT DistributionActionID FROM [dbo].[DistributionAction] 
			 	                       WHERE DistributionID = @DistID AND   Parameters = @ActionName)
	            -- Insert new distribution action
	            -- Connect distribution to the action
	            INSERT INTO [dbo].[Activity](
		            ActivityID, Subject, DistributionActionID, ActivityTypeID, ActivityDate, Amount, Duration, Version,  
		            MediaContactID, MediaOutletID, MediaContactSortName, MediaOutletSortName, DataGroupID, 
		            act.CompanyID, CreationDate, LastModifiedDate, 
		            IncludeInAnalytics, RemindViaEmail, LastIndexedDate, DistributionID, ReIndex, 
		            HasAttachment, AttachmentFileTypeID, IsManager)
		            -- From where -- copy rows data
		            SELECT TOP 1
			            @ActivityID AS ActivityID, 		
			            @campaigns AS Subject,
			            @DistActionID AS DistributionActionID,
			            t.ActivityTypeID, 
			            ActivityDate, Amount, Duration, act.Version, MediaContactID, MediaOutletID, 
			            MediaContactSortName, MediaOutletSortName, DataGroupID, 
			            act.CompanyID, 
			            CreationDate, LastModifiedDate, IncludeInAnalytics, 
			            RemindViaEmail, LastIndexedDate, DistributionID, 		 
			            ReIndex, HasAttachment, AttachmentFileTypeID, IsManager
		            FROM [dbo].[Activity] act
		            LEFT JOIN [dbo].[ActivityType] t 
		            ON    act.CompanyID = t.CompanyID
		            WHERE act.DistributionID = @DistID
                    AND   t.Name = @ActTypeName
                    ----------- Here comes contact, outlet, individual etc...
		            AND   act.{payload}		  

                    UPDATE [dbo].[DistributionEmailStatistics]
                    SET OpenedCount = @OpenedCount
                    WHERE DistributionId = @DistID          
                END                
                ";

            var cmd = new SqlCommand(sqlCommand, Connection);
            var update = cmd.ExecuteNonQuery();
            return update >= 2;
        }

        /// <summary>
        /// Inserts email recipient click for a link.
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="item"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public bool ClickEmailLink(EmailDistDetails dist, IMediaListItem item, Link link, int clicked)
        {
            if (string.IsNullOrEmpty(link?.Name)) throw new ArgumentException(Err.Msg("Wrong link parameter"));
            var payload = GenerateRecipientTypeId(dist, item);
            var sqlCommand = $@"
                BEGIN
	                -- variables
	                DECLARE @DistID int, 
	                        @ActivityID int,          @DistActionID int, 
	                        @ActTypeName varchar(50), @ActionName varchar(550),
                            @ClickedCount int;
	                SET @DistID = {dist.DistributionId};
                    SET @ClickedCount = {clicked};
	                SET @ActTypeName  = 'E-mail Campaign'
	                SET @ActionName   = '{link.Name}'
	                EXEC @ActivityID  = [dbo].[UniqueID_GetNextID] 'Activity', 'ActivityID'
	                SET @DistActionID = (SELECT DistributionActionID FROM [dbo].[DistributionAction] 
			 	                         WHERE DistributionID = @DistID 
			 	                         AND Name = @ActionName)
	                -- Insert new distribution action
	                -- Connect distribution to the action
	                INSERT INTO [dbo].[Activity](
		                ActivityID, Subject, DistributionActionID, ActivityTypeID, ActivityDate, Amount, Duration, Version,  
		                MediaContactID, MediaOutletID, MediaContactSortName, MediaOutletSortName, DataGroupID, 
		                act.CompanyID, CreationDate, LastModifiedDate, 
		                IncludeInAnalytics, RemindViaEmail, LastIndexedDate, DistributionID, ReIndex, 
		                HasAttachment, AttachmentFileTypeID, IsManager)
		                -- From where -- copy rows data
		                SELECT TOP 1
			                @ActivityID AS ActivityID, 		
			                @ActionName AS Subject,
			                @DistActionID AS DistributionActionID,
			                t.ActivityTypeID, 
			                ActivityDate, Amount, Duration, act.Version, MediaContactID, MediaOutletID, 
			                MediaContactSortName, MediaOutletSortName, DataGroupID, 
			                act.CompanyID, 
			                CreationDate, LastModifiedDate, IncludeInAnalytics, 
			                RemindViaEmail, LastIndexedDate, DistributionID, 		 
			                ReIndex, HasAttachment, AttachmentFileTypeID, IsManager
		                FROM [dbo].[Activity] act
		                LEFT JOIN [dbo].[ActivityType] t 
		                ON    act.CompanyID = t.CompanyID
		                WHERE act.DistributionID = @DistID
                        AND   t.Name = @ActTypeName
                        ----------- Here comes contact, outlet, individual etc...
		                AND   act.{payload}		
                        
                        UPDATE [dbo].[DistributionEmailStatistics]
                            SET ClickThroughCount = @ClickedCount
                            WHERE DistributionId = @DistID    
                
                END
            ";
            var cmd = new SqlCommand(sqlCommand, Connection);
            var update = cmd.ExecuteNonQuery();
            return update >= 2;
        }

        /// <summary>
        /// Retrieves distribution email table object by distribution id.
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns>DistributionEmail</returns>
        public DistributionEmail GetDistributionEmail(int distributionId)
        {
            var sqlStatement = $"SELECT * FROM DistributionEmail WHERE DistributionId={distributionId}";
            var distributionEmails = Connection.Query<DistributionEmail>(sqlStatement);
            return distributionEmails.FirstOrError("No distribution found by id " + distributionId);
        }

        /// <summary>
        /// Retrieves Distribution table by specified table field value. 
        /// </summary>
        /// <param name="uniqueField">table field</param>
        /// <param name="value">table value</param>
        /// <returns></returns>
        public Distribution GetDistributionByField(string uniqueField, string value)
        {
            var sqlStatement = $"SELECT * FROM Distribution WHERE {uniqueField}='{value}'";
            var distributions = Connection.Query<Distribution>(sqlStatement);
            return distributions.FirstOrError($"No distribution found by {uniqueField}={value}");
        }
    }
}
