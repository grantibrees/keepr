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

    internal IEnumerable<Vault> GetByUserId(string userId)
    {
      return _repo.GetByUserId(userId);
    }

  }

}