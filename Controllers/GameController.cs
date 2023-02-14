using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers; 

[Controller]
[Route("api/[controller]")]
public class GameController: Controller {
    
    private readonly DBService _mongoDBService;

    public GameController(DBService DBService) {
        _mongoDBService = DBService;
    }

    [HttpGet]
    public async Task<List<Playlist>> Get() {
        return await _mongoDBService.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<List<Playlist>> GetUserbyId(string id) {
        return await _mongoDBService.GetAsyncbyid(id);
    }

    [HttpPost]
    public async Task<IActionResult> PostFirstTimeLogin([FromBody] Playlist playlist) { 
        await _mongoDBService.CreateAsync(playlist);
        return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
    }

    [HttpPut("login/{id}")]
    public async Task<IActionResult> PutLogin(string id, [FromBody] string LoginTime,[FromBody] Playlist playlist) {
       await _mongoDBService.PUTLogin(id, LoginTime);
       return NoContent();
    }

    [HttpPatch("totalscore/{id}")]
    public async Task<IActionResult> PutTotalScore(string id, [FromBody] Playlist playlist) {
       await _mongoDBService.PUTTotalScore(id, playlist);
       return NoContent();
    }

    [HttpPatch("tokensrequired/{id}")]
    public async Task<IActionResult> PutTokensReq(string id,/* [FromBody] int TokensRequested ,[FromBody] string TxnHash, */[FromBody] Playlist playlist) {
       await _mongoDBService.PUTTokensReq(id, playlist);
       return NoContent();
    }

    [HttpPatch("tokensclaimed/{id}")]
    public async Task<IActionResult> PutTokensClaimed(string id, /*[FromBody] int TokensClaimed ,[FromBody] string lastTokensClaimedUpdated, */[FromBody] Playlist playlist) {
       await _mongoDBService.PUTTokensCaimed(id, playlist);
       return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }

}
