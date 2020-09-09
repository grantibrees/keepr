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
    // NOTE Gets the collection
    public IEnumerable<VaultKeep> Get()
    {
      return _repo.Get();
    }

    // NOTE Calls GetbyID. I'm not sure why this works, but putting _repo.GetById(id); doesn't work if you put it in the method above. ¯\_(ツ)_/¯
    public VaultKeep Get(int id)
    {
      return _repo.GetById(id);
    }

    // NOTE Gets by Id. If no ID, problem
    private VaultKeep GetById(int id)
    {
      VaultKeep found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }



    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }


    public VaultKeep Delete(string userId, int id)
    {

      VaultKeep found = GetById(id);
      if (found.UserId != userId)
      {
        throw new Exception("Cannot delete, users don't match.");
      }
      if (_repo.Delete(id, userId))
      {
        return found;
      }
      throw new Exception("Dev, check your code (VaultKeep Delete)");
    }

    // NOTE Getting the collection but putting the View Model class on it for passing down
    public IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
    {
      return _repo.GetKeepsByVaultId(vaultId, userId);
    }


  }

}