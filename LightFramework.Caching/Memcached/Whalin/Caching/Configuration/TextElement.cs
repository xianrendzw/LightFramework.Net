using System.Configuration;

namespace Whalin.Caching.Configuration
{
	/// <summary>
	/// CDATA config element
	/// </summary>
	public sealed class TextElement : ConfigurationElement
	{
		public string Content { get; set; }

		protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
		{
			this.Content = reader.ReadElementContentAsString();
		}
	}
}
