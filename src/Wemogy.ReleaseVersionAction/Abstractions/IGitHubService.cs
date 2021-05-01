using System.Collections.Generic;
using System.Threading.Tasks;
using Wemogy.ReleaseVersionAction.Models;

namespace Wemogy.ReleaseVersionAction.Abstractions
{
	public interface IGitHubService
	{
		Task<List<Tag>> GetAllTagsAsync(string repository);
	}
}
