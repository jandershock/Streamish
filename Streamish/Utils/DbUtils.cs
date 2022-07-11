using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Streamish.Models;

namespace Streamish.Utils
{
    /// <summary>
    ///  A set of useful function for interacting with ADO.NET
    /// </summary>
    public static class DbUtils
    {
        /// <summary>
        ///  Get a string from a data reader object and gracefully handle NULL values
        /// </summary>
        /// <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        /// <param name="column">The name of the column from the result set refereed to by the reader.</param>
        /// <returns>The value of the given column or null.</returns>
        public static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetString(ordinal);
        }

        /// <summary>
        ///  Get an int from a data reader object.
        ///  This method assumes the value is not NULL.
        /// </summary>
        /// <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        /// <param name="column">The name of the column from the result set refereed to by the reader.</param>
        /// <returns>The value of the given column.</returns>
        public static int GetInt(SqlDataReader reader, string column)
        {
            return reader.GetInt32(reader.GetOrdinal(column));
        }

        /// <summary>
        ///  Get a DateTime from a data reader object.
        ///  This method assumes the value is not NULL.
        /// </summary>
        /// <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        /// <param name="column">The name of the column from the result set refereed to by the reader.</param>
        /// <returns>The value of the given column.</returns>
        public static DateTime GetDateTime(SqlDataReader reader, string column)
        {
            return reader.GetDateTime(reader.GetOrdinal(column));
        }

        /// <summary>
        ///  Get an int? (nullable int) from a data reader object and gracefully handle NULL values
        /// </summary>
        /// <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        /// <param name="column">The name of the column from the result set refereed to by the reader.</param>
        /// <returns>The value of the given column or null.</returns>
        public static int? GetNullableInt(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetInt32(ordinal);
        }

        /// <summary>
        ///  Get a DateTime? (nullable DateTime) from a data reader object and gracefully handle NULL values
        /// </summary>
        /// <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        /// <param name="column">The name of the column from the result set refereed to by the reader.</param>
        /// <returns>The value of the given column or null.</returns>
        public static DateTime? GetNullableDateTime(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetDateTime(ordinal);
        }

        /// <summary>
        ///  Determine if the value a given column is NULL
        /// </summary>
        /// <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        /// <param name="column">The name of the column from the result set refereed to by the reader.</param>
        /// <returns>true if "column" is NULL in the database otherwise false.</returns>
        public static bool IsDbNull(SqlDataReader reader, string column)
        {
            return reader.IsDBNull(reader.GetOrdinal(column));
        }

        /// <summary>
        ///  Determine if the value a given column is not NULL
        /// </summary>
        /// <param name="reader">A SqlDataReader that has not exhausted it's result set.</param>
        /// <param name="column">The name of the column from the result set refereed to by the reader.</param>
        /// <returns>true if "column" is not NULL in the database otherwise false.</returns>
        public static bool IsNotDbNull(SqlDataReader reader, string column)
        {
            return !IsDbNull(reader, column);
        }

        /// <summary>
        ///  Add a parameter to the given SqlCommand object and gracefully handle null values.
        /// </summary>
        /// <param name="cmd">The command to which to add the parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter. May be null.</param>
        public static void AddParameter(SqlCommand cmd, string name, object value)
        {
            if (value == null)
            {
                cmd.Parameters.AddWithValue(name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(name, value);
            }
        }

        public static UserProfile ExtractUserProfile(SqlDataReader reader, string idAlias, string nameAlias, string emailAlias, string imageUrlAlias, string dateCreatedAlias)
        {
            return new UserProfile()
            {
                Id = DbUtils.GetInt(reader, idAlias),
                Name = DbUtils.GetString(reader, nameAlias),
                Email = DbUtils.GetString(reader, emailAlias),
                ImageUrl = DbUtils.GetString(reader, imageUrlAlias),
                DateCreated = DbUtils.GetDateTime(reader, dateCreatedAlias)
            };
        }

        public static Video ExtractVideo(SqlDataReader reader, string idAlias, string titleAlias, string descriptionAlias, string urlAlias, string dateCreatedAlias, string userProfileIdAlias)
        {
            return new Video()
            {
                Id = DbUtils.GetInt(reader, idAlias),
                Title = DbUtils.GetString(reader, titleAlias),
                Description = DbUtils.GetString(reader, descriptionAlias),
                Url = DbUtils.GetString(reader, urlAlias),
                DateCreated = DbUtils.GetDateTime(reader, dateCreatedAlias),
                UserProfileId = DbUtils.GetInt(reader, userProfileIdAlias)
            };
        }

        public static Comment ExtractComment(SqlDataReader reader, string idAlias, string messageAlias, string vIdAlias, string userProfileIdAlias)
        {
            return new Comment()
            {
                Id = DbUtils.GetInt(reader, idAlias),
                Message = DbUtils.GetString(reader, messageAlias),
                VideoId = DbUtils.GetInt(reader, vIdAlias),
                UserProfileId = DbUtils.GetInt(reader, userProfileIdAlias)
            };
        }
    }
}