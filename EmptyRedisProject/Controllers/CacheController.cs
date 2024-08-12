using EmptyRedisProject.Interfaces;
using EmptyRedisProject.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cache")]
public class CacheController : ControllerBase
{
    private readonly IRedisCacheService _redisCacheService;

    public CacheController(IRedisCacheService redisCacheService)
    {
        _redisCacheService = redisCacheService;
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        return Ok(await _redisCacheService.GetValueAsync(key));
    }

    [HttpPost]
    public async Task<IActionResult> Set([FromBody] RedisCacheRequestModel redisCacheRequestModel)
    {
        await _redisCacheService.SetValueAsync(redisCacheRequestModel.Key, redisCacheRequestModel.Value);
        return Ok();
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete(string key)
    {
        await _redisCacheService.Clear(key);
        return Ok();
    }
}