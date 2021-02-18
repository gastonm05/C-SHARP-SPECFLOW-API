using System;
using System.Data.SqlClient;
using CCC_API.Data.PostData.Analytics;
using CCC_API.Data.TestDataObjects.Activities;
using CCC_API.Services.Common.db;
using CCC_API.Services.Common.Support;

namespace CCC_API.Services.Activities.DB
{
    public class DbDistributionPRWebService : BaseDbService
    {
        public DbDistributionPRWebService(int companyId)
            : base(new CompanyService().GetConnectionToCompanyDb(companyId))
        {}

        /// <summary>
        /// This method create a DB connection to update the release status based on the given values.
        /// </summary>
        /// <param name="distributionID"></param>
        /// <param name="desiredReleaseStatus"></param>
        public void SetPRWebReleaseStatusExecution(int distributionID, PRWebReleaseStatus desiredReleaseStatus)
        {
            string sqlStatement = $"UPDATE [dbo].[DistributionPRWeb] SET PRWebReleaseStatusId={(int)desiredReleaseStatus} WHERE DistributionID={distributionID}";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("updatePublishActivityStatus failure " + e.Message);
            }
        }

        /// <summary>
        /// This method clean all the created changes in DB to delete a a distribution
        /// </summary>
        /// <param name="distributionId"></param>
        /// <param name="prwebPressReleaseId"></param>
        public void DeleteDistribution(int distributionId, int prwebPressReleaseId)
        {
            ExeuteDeleteDistribution($"delete from [dbo].[DistributionAttachment]  where distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[DistributionEmail] where distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[DistributionExportLabel]  where distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[DistributionFax]  where distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[DistributionPRNWire]  where distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[DistributionActionQueue]  where distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[DistributionPRWeb] where  distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[Distribution] where  distributionid={distributionId}");
            ExeuteDeleteDistribution($"delete from [dbo].[PublishActivity] where  entityid={distributionId}");

            PRWebAdminService prwebAdminService = new PRWebAdminService();
            var prwebAdminConn = prwebAdminService.GetConnectionToPRWebAdminDb();

            ExecuteAdminDeleteDistribution($"delete from [dbo].[PRWebAdminDistribution] where PRid={prwebPressReleaseId}", prwebAdminConn);
            ExecuteAdminDeleteDistribution($"delete from [dbo].[PRWebAdminDistributionHistory] where PRid={prwebPressReleaseId}", prwebAdminConn);

            prwebAdminService.TearDown();
        }

        /// <summary>
        /// This method execute the delete process in the Admin PRWeb DB
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <param name="prwebPressReleaseId"></param>
        /// <param name="prwebAdminConn"></param>
        public void ExecuteAdminDeleteDistribution(string sqlStatement, SqlConnection prwebAdminConn)
        {
            try
            {
                var cmd = new SqlCommand(sqlStatement, prwebAdminConn);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("Admin delete Distribution failure " + e.Message);
            }
        }

        /// <summary>
        /// This method execute the delete process to clean the distribution
        /// </summary>
        /// <param name="sqlStatement"></param>
        /// <param name="distributionId"></param>
        public void ExeuteDeleteDistribution(string sqlStatement)
        {
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("deleteDistribution failure " + e.Message);
            }
        }

        /// <summary>
        /// This method update the expiration date of a subscription.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="date"></param>
        public void updatePRWebSubscriptionExpirationDate(int companyId, string date)
        {
            string sqlStatement = $"UPDATE [dbo].[prwebsubscription] SET ExpirationDate='{date}' WHERE companyid={companyId}";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("The update of the expiration date fail" + e.Message);
            }
        }

        /// <summary>
        /// This method returns the IDL id added to the DB.
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public string GetIdlDistributionPrweb(int distributionId)
        {
            string sqlStatement = $"SELECT PRWebIndustryOutletCategoryIDs FROM DistributionPRWEb WHERE DistributionId={distributionId}";
            try { 
                var cmd = new SqlCommand(sqlStatement, Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader["PRWebIndustryOutletCategoryIDs"].ToString();
                }
            }catch (Exception e)
            {
                Console.WriteLine("Get IDL of distribution prweb transaction fail " + e.Message);
            }
            return null;
        }

        /// <summary>
        /// This method deletes counts from zzDistributionPRWebByDay_{companyId} base on releaseId
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="releaseId"></param>
        public void DeleteCountsForRelease(int companyId, int releaseId)
        {
            string sqlStatement = $"DELETE FROM zzDistributionPRWebByDay_{companyId} WHERE ReleaseID={releaseId} ";

        try {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();

            } catch (Exception e)	{
                Console.WriteLine($"The delete statement on zzDistributionPRWebByDay_{companyId} fail" + e.Message);
            }
        }

        /// <summary>
        /// This method insert given values in zzDistributionPRWebByDay_{companyId} DB
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="releaseId"></param>
        /// <param name="releaseDate"></param>
        /// <param name="activityType"></param>
        /// <param name="hits"></param>
        public void InsertCountsForRelease(int companyId, int releaseId, string releaseDate, PRWebActivityType activityType, int hits)
        {
            int activity = (int)activityType;
            string sqlStatement = $"INSERT INTO zzDistributionPRWebByDay_{companyId} (ReleaseId, ActivityTypeId, ActivityDate, Hits) VALUES ({releaseId}, {activity}, '{releaseDate}', {hits})";

            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine($"The insert statement on zzDistributionPRWebByDay_{companyId} fail" + e.Message);
            }
        }

        /// <summary>
        /// This method update the PublicationState in the PublishActivity DB
        /// </summary>
        /// <param name="publicationStatus"></param>
        /// <param name="entityId"></param>
        public void UpdatePublishActivityStatus(PublicationsStatus publicationStatus, int entityId)
        {
            string sqlStatement = $"UPDATE [dbo].[PublishActivity] SET PublicationState={(int)publicationStatus} WHERE EntityId={entityId}";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("The update of the updatePublishActivityStatus fail" + e.Message);
            }
        }

        public int GetDistributionAddOnsQuantityUsed(int distributionId, int addonSubscriptionId)
        {
            int quantity = -1;
            string sqlStatement = $"SELECT Quantity FROM PRWebAddOnReleaseQuantity WHERE DistributionId = {distributionId} AND AddOnSubscriptionId = {addonSubscriptionId}";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    quantity = Int32.Parse(reader["Quantity"].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Get Distribution AddOns QuantityUsed failed with: " + e.Message);
            }

            return quantity;
        }

        public void UpdateDistributionAddonsQuantityUsed(int distributionId, int addonSubscriptionId, int quantityUsed)
        {
            string sqlStatement = $"UPDATE PRWebAddOnReleaseQuantity SET Quantity = {quantityUsed} WHERE DistributionID = {distributionId} AND AddOnSubscriptionID = {addonSubscriptionId}";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("The update of the PRWebAddOnReleaseQuantity failed with: " + e.Message);
            }
        }

        /// <summary>
        /// This method returns the DistributionPRWeb PressContactPhoneExtension from the db
        /// </summary>
        /// <param name="distributionId"></param>
        /// <returns></returns>
        public string GetPressContactPhoneExtension(int distributionId)
        {
            string sqlStatement = $"SELECT PressContactPhoneExtension FROM DistributionPRWEb WHERE DistributionId={distributionId}";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader["PressContactPhoneExtension"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Get PressContactPhoneExtension of distribution prweb transaction fail " + e.Message);
            }
            return null;
        }
    }
}
