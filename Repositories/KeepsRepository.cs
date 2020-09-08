using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories {
    public class KeepsRepository {
        private readonly IDbConnection _db;

        public KeepsRepository (IDbConnection db) {
            _db = db;
        }

        internal IEnumerable<Keep> Get () {
            string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
            return _db.Query<Keep> (sql);
        }

        internal Keep Create (Keep KeepData) {
            string sql = @"
           INSERT INTO keeps
           (name, description, userId, img, isPrivate, views, shares, keeps)
           VALUES
           (@Name, @Description, @UserId, @Img, @IsPrivate, @Views, @Shares, @Keeps);
           SELECT LAST_INSERT_ID()";
            KeepData.Id = _db.ExecuteScalar<int> (sql, KeepData);
            return KeepData;
        }
    }
}