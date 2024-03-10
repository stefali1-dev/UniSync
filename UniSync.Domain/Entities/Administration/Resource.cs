using UniSync.Domain.Common; // Assuming the same structure for Result<T>
using System;

namespace UniSync.Domain.Entities.Administration
{
    public class Resource
    {
        public Guid ResourceId { get; private set; }
        public string ResourceType { get; private set; } // e.g., Photo, Document, Video, etc.
        public string CloudURL { get; private set; }

        // Constructor
        private Resource(Guid resourceId, string resourceType, string cloudURL)
        {
            ResourceId = resourceId;
            ResourceType = resourceType;
            CloudURL = cloudURL;
        }

        // Create method
        public static Result<Resource> Create(Guid resourceId, string resourceType, string cloudURL)
        {
            if (resourceId == Guid.Empty)
            {
                return Result<Resource>.Failure("ResourceId cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(resourceType))
            {
                return Result<Resource>.Failure("ResourceType cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(cloudURL))
            {
                return Result<Resource>.Failure("CloudURL cannot be empty or whitespace.");
            }

            var resource = new Resource(resourceId, resourceType, cloudURL);
            return Result<Resource>.Success(resource);
        }
    }
}
