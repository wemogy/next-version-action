using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wemogy.ReleaseVersionAction.Abstractions;
using Wemogy.ReleaseVersionAction.Models;

namespace Wemogy.ReleaseVersionAction.Services
{
	public class GitHubService : IGitHubService
	{
		readonly HttpClient _httpClient;

		public GitHubService(string username, string token)
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://api.github.com");
			_httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("wemogy-action", "1"));
			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{token}")));
		}

		public async Task<List<Tag>> GetAllTagsAsync(string repository)
		{
			var url = $"/repos/{repository}/releases";
			var response = await _httpClient.GetAsync(url);
			var json = await response.Content.ReadAsStringAsync();
			var tags = JsonSerializer.Deserialize<List<Tag>>(json);
			return tags;
		}
	}
}
