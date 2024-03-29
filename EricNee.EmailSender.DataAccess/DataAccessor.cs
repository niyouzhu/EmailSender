﻿using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.EmailSender.DataAccess
{
    public class DataAccessor
    {
        public DataAccessor() : this(DatabaseFactoryWarpper.CreateInstance())
        {

        }

        public DataAccessor(Database database)
        {
            Database = database;
        }

        public Database Database { get; }

        public IEnumerable<MailEntry> GetBacklogEntries()
        {
            var sql = "SELECT ID, SUBJECT, CREATEDTIME, BODY, ISHTML, [FROM], [TO], CC, BCC, Attachments FROM BACKLOG";
            return Database.ExecuteSqlStringAccessor<MailEntry>(sql);

        }
        public IEnumerable<MailEntry> GetRealTimeEntries()
        {
            var sql = "SELECT ID, SUBJECT, CREATEDTIME, BODY, ISHTML, [FROM], [TO], CC, BCC, Attachments FROM REALTIME";
            return Database.ExecuteSqlStringAccessor<MailEntry>(sql);

        }
        public IEnumerable<MailEntry> GetInProcessEntries()
        {
            var sql = "SELECT ID, SUBJECT, CREATEDTIME, BODY, ISHTML, [FROM], [TO], CC, BCC, Attachments FROM INPROCESS";
            return Database.ExecuteSqlStringAccessor<MailEntry>(sql);

        }

        public int RemoveFromBacklog(MailEntry entry)
        {
            var sql = $"DELETE FROM BACKLOG WHERE ID = @Id";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            return Database.ExecuteNonQuery(cmd);
        }

        public int RemoveFromInProcess(MailEntry entry)
        {
            var sql = $"DELETE FROM INPROCESS WHERE ID = @Id";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            return Database.ExecuteNonQuery(cmd);
        }


        public MailEntry AddToBacklog(MailEntry entry)
        {
            var sql = @"INSERT INTO BACKLOG ([Id]
           ,[Subject]
           ,[CreatedTime]
           ,[Body]
           ,[IsHtml]
           ,[From]
           ,[To]
           ,[CC]
           ,[BCC],[Attachments]) VALUES(@Id, @Subject, @CreatedTime, @Body, @IsHtml, @From, @To, @CC, @BCC,@Attachments)";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            Database.AddInParameter(cmd, "@Subject", System.Data.DbType.String, entry.Subject);
            var now = DateTime.Now;
            Database.AddInParameter(cmd, "@CreatedTime", System.Data.DbType.DateTime, now);
            Database.AddInParameter(cmd, "@Body", System.Data.DbType.String, entry.Body);
            Database.AddInParameter(cmd, "@IsHtml", System.Data.DbType.Boolean, entry.IsHtml);
            Database.AddInParameter(cmd, "@From", System.Data.DbType.String, entry.From);
            Database.AddInParameter(cmd, "@To", System.Data.DbType.String, entry.To);
            Database.AddInParameter(cmd, "@CC", System.Data.DbType.String, entry.CC);
            Database.AddInParameter(cmd, "@BCC", System.Data.DbType.String, entry.BCC);
            Database.AddInParameter(cmd, "@Attachments", System.Data.DbType.String, entry.Attachments);
            Database.ExecuteNonQuery(cmd);
            entry.CreatedTime = now;
            return entry;
        }
        public MailEntry AddToInProcess(MailEntry entry)
        {
            var sql = @"INSERT INTO INPROCESS (Id, Subject, CreatedTime, Body, IsHtml
           ,[From]
           ,[To]
           ,[CC]
           ,[BCC],[Attachments]) VALUES(@Id, @Subject, @CreatedTime, @Body, @IsHtml, @From, @To, @CC, @BCC,@Attachments)";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            Database.AddInParameter(cmd, "@Subject", System.Data.DbType.String, entry.Subject);
            var now = DateTime.Now;
            Database.AddInParameter(cmd, "@CreatedTime", System.Data.DbType.DateTime, now);
            Database.AddInParameter(cmd, "@Body", System.Data.DbType.String, entry.Body);
            Database.AddInParameter(cmd, "@IsHtml", System.Data.DbType.Boolean, entry.IsHtml);
            Database.AddInParameter(cmd, "@From", System.Data.DbType.String, entry.From);
            Database.AddInParameter(cmd, "@To", System.Data.DbType.String, entry.To);
            Database.AddInParameter(cmd, "@CC", System.Data.DbType.String, entry.CC);
            Database.AddInParameter(cmd, "@BCC", System.Data.DbType.String, entry.BCC);
            Database.AddInParameter(cmd, "@Attachments", System.Data.DbType.String, entry.Attachments);
            Database.ExecuteNonQuery(cmd);
            entry.CreatedTime = now;
            return entry;
        }

        public MailEntry AddToSuccess(MailEntry entry)
        {
            var sql = @"INSERT INTO SUCCESS (Id, Subject, CreatedTime, Body, IsHtml
           ,[From]
           ,[To]
           ,[CC]
           ,[BCC],[Attachments]) VALUES(@Id, @Subject, @CreatedTime, @Body, @IsHtml, @From, @To, @CC, @BCC,@Attachments)";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            Database.AddInParameter(cmd, "@Subject", System.Data.DbType.String, entry.Subject);
            var now = DateTime.Now;
            Database.AddInParameter(cmd, "@CreatedTime", System.Data.DbType.DateTime, now);
            Database.AddInParameter(cmd, "@Body", System.Data.DbType.String, entry.Body);
            Database.AddInParameter(cmd, "@IsHtml", System.Data.DbType.Boolean, entry.IsHtml);
            Database.AddInParameter(cmd, "@From", System.Data.DbType.String, entry.From);
            Database.AddInParameter(cmd, "@To", System.Data.DbType.String, entry.To);
            Database.AddInParameter(cmd, "@CC", System.Data.DbType.String, entry.CC);
            Database.AddInParameter(cmd, "@BCC", System.Data.DbType.String, entry.BCC);
            Database.AddInParameter(cmd, "@Attachments", System.Data.DbType.String, entry.Attachments);
            Database.ExecuteNonQuery(cmd);
            entry.CreatedTime = now;
            return entry;
        }


        public MailEntry AddToFailure(MailEntry entry)
        {
            var sql = @"INSERT INTO Failure (Id, Subject, CreatedTime, Body, IsHtml           
        ,[From]
           ,[To]
           ,[CC]
           ,[BCC],[Attachments]) VALUES(@Id, @Subject, @CreatedTime, @Body, @IsHtml, @From, @To, @CC, @BCC ,@Attachments)";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            Database.AddInParameter(cmd, "@Subject", System.Data.DbType.String, entry.Subject);
            var now = DateTime.Now;
            Database.AddInParameter(cmd, "@CreatedTime", System.Data.DbType.DateTime, now);
            Database.AddInParameter(cmd, "@Body", System.Data.DbType.String, entry.Body);
            Database.AddInParameter(cmd, "@IsHtml", System.Data.DbType.Boolean, entry.IsHtml);
            Database.AddInParameter(cmd, "@From", System.Data.DbType.String, entry.From);
            Database.AddInParameter(cmd, "@To", System.Data.DbType.String, entry.To);
            Database.AddInParameter(cmd, "@CC", System.Data.DbType.String, entry.CC);
            Database.AddInParameter(cmd, "@BCC", System.Data.DbType.String, entry.BCC);
            Database.AddInParameter(cmd, "@Attachments", System.Data.DbType.String, entry.Attachments);
            Database.ExecuteNonQuery(cmd);
            entry.CreatedTime = now;
            return entry;
        }

        public int RemoveFromRealTime(MailEntry entry)
        {
            var sql = $"DELETE FROM REALTIME WHERE ID = @Id";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            return Database.ExecuteNonQuery(cmd);
        }

        public MailEntry AddToRealTime(MailEntry entry)
        {
            var sql = @"INSERT INTO REALTIME (Id, Subject, CreatedTime, Body, IsHtml           
        ,[From]
           ,[To]
           ,[CC]
           ,[BCC], [Attachments]) VALUES(@Id, @Subject, @CreatedTime, @Body, @IsHtml, @From, @To, @CC, @BCC, @Attachments)";
            var cmd = Database.GetSqlStringCommand(sql);
            Database.AddInParameter(cmd, "@Id", System.Data.DbType.Guid, entry.Id);
            Database.AddInParameter(cmd, "@Subject", System.Data.DbType.String, entry.Subject);
            var now = DateTime.Now;
            Database.AddInParameter(cmd, "@CreatedTime", System.Data.DbType.DateTime, now);
            Database.AddInParameter(cmd, "@Body", System.Data.DbType.String, entry.Body);
            Database.AddInParameter(cmd, "@IsHtml", System.Data.DbType.Boolean, entry.IsHtml);
            Database.AddInParameter(cmd, "@From", System.Data.DbType.String, entry.From);
            Database.AddInParameter(cmd, "@To", System.Data.DbType.String, entry.To);
            Database.AddInParameter(cmd, "@CC", System.Data.DbType.String, entry.CC);
            Database.AddInParameter(cmd, "@BCC", System.Data.DbType.String, entry.BCC);
            Database.AddInParameter(cmd, "@Attachments", System.Data.DbType.String, entry.Attachments);
            Database.ExecuteNonQuery(cmd);
            entry.CreatedTime = now;
            return entry;
        }
    }
}
