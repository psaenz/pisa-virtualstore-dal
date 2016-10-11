using System.Reflection;
using System.IO;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Test.Factories.General
{
    class GeneralMediaFactory : BaseEntityFactory<GeneralMedia>
    {
        public override GeneralMedia CreateInstance()
        {
            return CreateInstance(10.50, Resource1.GeneralMedia_01_jpg, "testImage.jpg", GeneralMediaTypes.GetInstance().IMAGE, 20.50);
        }

        public GeneralMedia CreateInstance(double height, byte[] mediaData, string reference, GeneralMediaType mediaType, double width) {
            GeneralMedia media = new GeneralMedia();
            media.Height = height;
            media.MediaData = mediaData;
            media.MediaReference = reference;
            media.MediaType = mediaType;
            media.Width = width;
            return media;
        }
    }
}
