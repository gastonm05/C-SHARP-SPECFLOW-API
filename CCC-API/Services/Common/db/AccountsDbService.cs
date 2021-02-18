using CCC_API.Services.Common.Support;
using CCC_Infrastructure.Utils;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CCC_API.Services.Common.db
{

    public class AccountsDbService : BaseDbService
    {
        public AccountsDbService(int companyId)
            : base(new CompanyService().GetConnectionToCompanyDb(companyId)) { }

        /// <summary>
        /// Get Activation code for provided userAccountId
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <returns>int</returns>
        public int getActivationCode(int userAccountId)
        {
            string sqlStatement =
                $@"SELECT up.parametervalue 
                   FROM [dbo].[userparameter] up 
                   WHERE up.userAccountId = '{userAccountId}' AND up.parametername = 'MSATOKEN'";
            var cmd = new SqlCommand(sqlStatement, Connection);
            var reader = cmd.ExecuteReader();

            int activationCode = 0;
            while (reader.Read())
            {
                activationCode = Convert.ToInt32(reader["ParameterValue"]);
            }
            return activationCode;
        }
        /// <summary>
        /// This method create a DB connection to reset MSATokenResent userparameter based on the given values.
        /// </summary>
        /// <param name="userAccountId"></param>    
        /// <param name="value"></param>      
        /// <param name="parameterName"></param>      
        public void ResetMSATokenInfo(int userAccountId, string value, string parameterName)
        {
            string sqlStatement = $"UPDATE [dbo].[userparameter] SET parametervalue = '{value}' WHERE useraccountid='{userAccountId}' and parametername = '{parameterName}'";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("updateResetMSATokenResent " + e.Message);
            }
        }
        /// <summary>
        /// This method create a DB connection to reset userAccount IsLocked field based on the given values.
        /// </summary>
        /// <param name="userAccountId"></param>  
        /// <param name="value"></param>      
        public void ResetUserAccountIsLocked(int userAccountId, string value)
        {
            string sqlStatement = $"UPDATE [dbo].[useraccount] SET IsLocked = '{value}' WHERE useraccountid='{userAccountId}'";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("updateResetMSATokenResent " + e.Message);
            }
        }
        /// <summary>
        /// This method create a DB connection to reset userAccount FailedAttempts field based on the given values.
        /// </summary>
        /// <param name="userAccountId"></param>        
        /// <param name="value"></param>      
        public void ResetUserFailedAttempts(int userAccountId, string value)
        {
            string sqlStatement = $"UPDATE [dbo].[useraccount] SET FailedAttempts = '{value}' WHERE useraccountid='{userAccountId}'";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("updateResetMSATokenResent " + e.Message);
            }
        }

        /// <summary>
        /// Get Latest UserAccountResetPasswordid for provided userAccountId
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <returns>int</returns>
        public string GetUserAccountResetPasswordid(int userAccountId)
        {
            string sqlStatement =
                   $@"SELECT A.UserAccountResetPasswordid
                   FROM [dbo].[UserAccountResetPassword ] A 
				   INNER JOIN (SELECT 	useraccountid, MAX(expirationDateUTC) AS MaxExpirationDate FROM UserAccountResetPassword
								WHERE useraccountid = '{userAccountId}' GROUP BY useraccountid) B	
                   ON A.useraccountid = B.useraccountid AND A.expirationDateUTC = B.MaxExpirationDate";
            var cmd = new SqlCommand(sqlStatement, Connection);
            var reader = cmd.ExecuteReader();

            string userAccountResetPasswordid = "";
            while (reader.Read())
            {
                userAccountResetPasswordid = Convert.ToString(reader["UserAccountResetPasswordid"]);
            }
            return userAccountResetPasswordid;
        }

        /// <summary>
        /// Corrupts the user cache.
        /// </summary>
        /// <param name="userAccountId">The user account id.</param>
        /// <param name="field">The field in the database to corrupt.</param>
        /// <param name="value">The value to put in the field.</param>
        /// <exception cref="Exception">
        /// CorruptUserCache failed: " + e.Message
        /// </exception>
        public void CorruptUserCache(int userAccountId, string field, string value)
        {
            var regex = new Regex("[^A-Za-z0-9']");
            var newvalue = regex.Replace(value, string.Empty);
            if (newvalue != value)
            {
                throw new Exception(Err.Msg($"SQL illegal for field '{field}' and value '{value}'"));
            }
            string sqlStatement = $"UPDATE [dbo].[usercache] SET {field} = {value} WHERE useraccountid='{userAccountId}'";
            try
            {
                var cmd = new SqlCommand(sqlStatement, Connection);
                var update = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(Err.Msg("CorruptUserCache failed."), e);
            }
        }

        /// <summary>
        /// Get LCID current user parameter for provided userAccountId
        /// </summary>
        /// <param name="userAccountId"></param>
        /// <returns>int</returns>
        public int GetLCID(int userAccountId)
        {
            string sqlStatement =
                $@"SELECT up.parametervalue 
                   FROM [dbo].[userparameter] up 
                   WHERE up.userAccountId = '{userAccountId}' AND up.parametername = 'LCID'";
            var cmd = new SqlCommand(sqlStatement, Connection);
            var reader = cmd.ExecuteReader();

            int lcid = 0;
            while (reader.Read())
            {
                lcid = Convert.ToInt32(reader["ParameterValue"]);
            }
            return lcid;
        }        
    }
}