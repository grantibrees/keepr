using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }

    internal Keep Create(Keep KeepData)
    {
      string sql = @"
           INSERT INTO keeps
           (name, description, userId, img, isPrivate, views, shares, keeps)
           VALUES
           (@Name, @Description, @UserId, @Img, @IsPrivate, @Views, @Shares, @Keeps);
           SELECT LAST_INSERT_ID()";
      KeepData.Id = _db.ExecuteScalar<int>(sql, KeepData);
      return KeepData;
    }
    internal Keep GetById(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal bool Delete(int id, string userId)
    {
      string sql = "DELETE FROM keeps WHERE id = @id AND userId = @userId LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }

    internal bool Edit(Keep editKeep, string userId)
    {
      editKeep.UserId = userId;
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img,
      views = @Views,
      shares = @Shares,
      keeps = @Keeps,
      isPrivate = @IsPrivate
      WHERE id = @Id
      AND userId = @UserId";
      int affectedRows = _db.Execute(sql, editKeep);
      return affectedRows == 1;
    }

    internal bool IncrementKeptCount(Keep editKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
      keeps = @Keeps
      WHERE id = @Id";
      int affectedRows = _db.Execute(sql, editKeep);
      return affectedRows == 1;
    }

    internal bool IncrementViewCount(Keep editKeep)
    {
      string sql = @"
      UPDATE keeps
      SET
      views= @Views
      WHERE id = @Id";
      int affectedRows = _db.Execute(sql, editKeep);
      return affectedRows == 1;
    }
    internal IEnumerable<Keep> GetUsersKeeps(string userId)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @userId";
      return _db.Query<Keep>(sql, new { userId });
    }

  }
}