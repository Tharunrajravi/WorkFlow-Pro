using System;
using System.Runtime.Serialization;

namespace WorkflowPro.Common
{
    /// <summary>
    /// Base application exception for all domain-specific errors in Workflow Pro.
    /// </summary>
    [Serializable]
    public class BaseApplicationException : Exception
    {
        public BaseApplicationException() { }
        public BaseApplicationException(string message) : base(message) { }
        public BaseApplicationException(string message, Exception innerException) : base(message, innerException) { }
        protected BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Exception thrown when business rules or data validation checks fail.
    /// </summary>
    [Serializable]
    public class ValidationException : BaseApplicationException
    {
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Exception thrown when a requested entity cannot be located in the database.
    /// </summary>
    [Serializable]
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Exception thrown when file upload, size check, format validation, or disk storage fails.
    /// </summary>
    [Serializable]
    public class FileUploadException : BaseApplicationException
    {
        public FileUploadException() { }
        public FileUploadException(string message) : base(message) { }
        public FileUploadException(string message, Exception innerException) : base(message, innerException) { }
        protected FileUploadException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Exception thrown when authentication or authorization operations fail.
    /// </summary>
    [Serializable]
    public class AuthenticationException : BaseApplicationException
    {
        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, Exception innerException) : base(message, innerException) { }
        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Exception thrown when a database or persistence error occurs in the repository layer.
    /// </summary>
    [Serializable]
    public class RepositoryException : BaseApplicationException
    {
        public RepositoryException() { }
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(string message, Exception innerException) : base(message, innerException) { }
        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}


