﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LintCoder.Shared.MongoDB
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
