using System;
using Core.Entities.Identity;

namespace Core.Contract.Repository
{
	public interface ITokenService
	{
		string CreateToken(AppUser user);
	}
}

