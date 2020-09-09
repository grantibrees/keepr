using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Http;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }

    public Vault Get(int id)
    {
      return _repo.Get(id);
    }

    public IEnumerable<Vault> GetByUserId(string userId)
    {
      return _repo.GetByUserId(userId);
    }

    public Vault GetById(int id, string userId)
    {
      var found = Get(id);
      if (found.UserId != userId)
      {
        throw new Exception("Problem, this vault belongs to a different user");
      }
      return _repo.GetById(id, userId);
    }

    public string Delete(int id, string userId)
    {
      Vault found = Get(id);
      if (found.UserId != userId)
      {
        throw new Exception("Problem, this vault belongs to a different user");
      }
      if (_repo.Delete(id, userId))
      {
        return "Successfully Deleted";
      }
      throw new Exception("Problem, dev check your code (vaultservice delete)");
    }

  }

}