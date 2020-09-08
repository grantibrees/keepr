using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }
    public VaultKeep Get(int id)
    {
      return _repo.GetById(id);
    }

    internal IEnumerable<VaultKeep> Get()
    {
      return _repo.Get();
    }

  }

}