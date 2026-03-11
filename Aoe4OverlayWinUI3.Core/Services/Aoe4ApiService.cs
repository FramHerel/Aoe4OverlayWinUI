using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Aoe4OverlayWinUI3.Core.Contracts.Services;
using Aoe4OverlayWinUI3.Core.Models;

namespace Aoe4OverlayWinUI3.Core.Services;

public class Aoe4ApiService:IAoe4ApiService
{
    private readonly HttpClient _httpClient;
    // API 基础路径
    private const string BaseUrl = "https://aoe4world.com/api/v0/";

    // 构造函数，注入 HttpClient
    public Aoe4ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    // 获取玩家信息
    public async Task<Player?> GetPlayerAsync(string query)
    {
        // 如果查询字符串为空，直接返回 null
        if (string.IsNullOrEmpty(query)) return null;
        try
        {

            if (long.TryParse(query, out _))
            {
                var playerById = await GetPlayerByIdAsync(query);

                if (playerById != null)
                {
                    return playerById;
                }
            }
            return await GetPlayerByNameAsync(query);
        }
        catch (Exception)
        {
            // 其他未知错误
            return null;
        }
    }

    // 通过 Name 获取玩家信息
    private async Task<Player?> GetPlayerByNameAsync(string query)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<PlayerSearchResponse>($"{BaseUrl}players/search?query={Uri.EscapeDataString(query)}");

            // 如果 response 不为空，且 players 列表里有数据，取第一个
            if (response?.Players != null && response.Players.Count > 0)
            {
                return response.Players[0];
            }

            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"GetPlayerByNameAsync Error: {ex.Message}");
            return null;
        }
    }

    // 通过 profile ID 获取玩家信息
    private async Task<Player?> GetPlayerByIdAsync(string query)
    {
        try
        {
            // 请求 profile ID 的 API
            using var response = await _httpClient.GetAsync($"players/{query}");

            // 如果是 404 返回 null，触发 GetPlayerByNameAsync
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            // 确保其他非成功状态码抛出异常
            response.EnsureSuccessStatusCode();

            // 解析 JSON
            return await response.Content.ReadFromJsonAsync<Player>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"GetPlayerById Error: {ex.Message}");
            return null;
        }
    }
}
