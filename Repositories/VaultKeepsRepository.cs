using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    // NOTE Get all relationships
    public IEnumerable<VaultKeep> Get()
    {
      string sql = "SELECT * FROM vaultkeeps";
      return _db.Query<VaultKeep>(sql);
    }

    // NOTE Get a single vk relationship
    public VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }


    // NOTE Get all vk relationships by a user
    public IEnumerable<VaultKeepViewModel> GetByUserId(string userId)
    {
      string sql = @"
      SELECT 
      keeps.*,
      vaultkeeps.id as vaultKeepId
      FROM vaultkeeps
      INNER JOIN keeps ON keeps.id = vaultkeeps.keepId
      WHERE userId = @UserId;";
      return _db.Query<VaultKeepViewModel>(sql, new { userId });
    }

    public VaultKeep Create(VaultKeep VaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (userId, vaultId, keepId)
      VALUES
      (@UserId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();";
      VaultKeepData.Id = _db.ExecuteScalar<int>(sql, VaultKeepData);
      return VaultKeepData;
    }


    public bool Delete(int id, string userId)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id and userId = @userId LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }

    public IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"
      SELECT 
        keeps.*,
        vaultkeeps.id as vaultKeepId
        FROM vaultkeeps
        INNER JOIN keeps ON keeps.id = vaultkeeps.keepId 
        WHERE (vaultId = @vaultId AND vaultkeeps.userId = @userId) ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId, userId });
    }


  }

}