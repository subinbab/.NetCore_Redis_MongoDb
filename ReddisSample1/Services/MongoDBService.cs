using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ReddisSample1.Models;

namespace ReddisSample1.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Playlist> _playlistCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            // Retrieve the MongoDB connection string from environment variables
            string mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB", EnvironmentVariableTarget.Process);

            if (string.IsNullOrWhiteSpace(mongoConnectionString))
            {
                Console.WriteLine("MongoDB connection string is null or empty. Please check your environment variables.");
                return;
            }
            else
            {
                Console.WriteLine(mongoConnectionString);
            }

            // Use the connection string to create a MongoClient
            MongoClient client = new MongoClient(mongoConnectionString);

            // Now you can use 'client' in your MongoDB-related code
            // For example: var database = client.GetDatabase("your-database-name");
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<Playlist>(mongoDBSettings.Value.CollectionName);
        }
        public async Task<List<Playlist>> GetAsync() {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();

        }
        public async Task CreateAsync(Playlist playlist) {
            await _playlistCollection.InsertOneAsync(playlist);
            return;
        }
        public async Task AddToPlaylistAsync(string id, string movieId) {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("movieIds", movieId);
            await _playlistCollection.UpdateOneAsync(filter, update);
            return;
        }
        public async Task DeleteAsync(string id) {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            await _playlistCollection.DeleteOneAsync(filter);
            return;
        }

    }
}
