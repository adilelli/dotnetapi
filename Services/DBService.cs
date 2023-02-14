using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace MongoExample.Services;

public class DBService {

    private readonly IMongoCollection<Playlist> _playlistCollection;

    public DBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<Playlist>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Playlist>> GetAsync() {
        return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<List<Playlist>> GetAsyncbyid(string id) {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        return await _playlistCollection.Find(filter).ToListAsync();
    }

    public async Task CreateAsync(Playlist playlist) {
        await _playlistCollection.InsertOneAsync(playlist);
        return;
    }

    public async Task POSTFirstTimeLogin(Playlist playlist) {
        await _playlistCollection.InsertOneAsync(playlist);
        return;
    }

    public async Task PUTLogin(string id, string LoginTime) {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        UpdateDefinition<Playlist> update = Builders<Playlist>.Update.Set("LoginTime", LoginTime);
        await _playlistCollection.UpdateOneAsync(filter, update);
        return;
    }

//string id, int TotalScore, int PrevScore
    public async Task PUTTotalScore(string id, Playlist playlist) {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        UpdateDefinition<Playlist> update = Builders<Playlist>.Update.Set("TotalScore", playlist.TotalScore)
                                                                     .Set("PrevScore",playlist.PrevScore);
        await _playlistCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task PUTTokensReq(string id, /*int TokensRequested, string TxnHash,*/ Playlist playlist2) {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        UpdateDefinition<Playlist> update = Builders<Playlist>.Update.Set("TokensRequested", playlist2.TokensRequested)
                                                                     .Set("TxnHash",playlist2.TxnHash);
        await _playlistCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task PUTTokensCaimed(string id, /*int TokensClaimed, int lastTokensClaimedUpdated,*/ Playlist playlist) {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        UpdateDefinition<Playlist> update = Builders<Playlist>.Update.Set("TokensClaimed", playlist.TokensClaimed)
                                                                     .Set("lastTokensClaimedUpdated",playlist.lastTokensClaimedUpdated);
        await _playlistCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id) { 
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        await _playlistCollection.DeleteOneAsync(filter);
        return;
    }



}