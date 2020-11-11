using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    [Serializable]
    public class PhotoGalleryException : Exception
    {
        public string Property { get; protected set; }

        public PhotoGalleryException(string message, string prop) : base(message) { Property = prop; }

        public PhotoGalleryException(string message): base(message) { }

        protected PhotoGalleryException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
           base(serializationInfo, streamingContext)
        { }
    }
}
